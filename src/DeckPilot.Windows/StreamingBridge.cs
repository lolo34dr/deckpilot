using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using DeckPilot.Core;

namespace DeckPilot.Windows;

internal static class StreamingSecretProtection
{
    public static string Protect(string secret)
    {
        if (string.IsNullOrEmpty(secret)) return "";
        byte[] protectedBytes = ProtectedData.Protect(Encoding.UTF8.GetBytes(secret), null, DataProtectionScope.CurrentUser);
        return Convert.ToBase64String(protectedBytes);
    }

    public static string Unprotect(string protectedSecret)
    {
        if (string.IsNullOrEmpty(protectedSecret)) return "";
        try { return Encoding.UTF8.GetString(ProtectedData.Unprotect(Convert.FromBase64String(protectedSecret), null, DataProtectionScope.CurrentUser)); }
        catch (Exception ex) when (ex is CryptographicException or FormatException) { return ""; }
    }
}

internal static class StreamingBridge
{
    private static readonly TimeSpan RequestTimeout = TimeSpan.FromSeconds(8);

    public static async Task<IReadOnlyList<string>> GetScenesAsync(string provider, StreamingConnections settings, CancellationToken cancellationToken)
    {
        if (provider == "streamlabs")
        {
            using var client = await StreamlabsSession.ConnectAsync(settings, cancellationToken);
            var result = await client.RequestAsync("ScenesService", "getScenes", [], cancellationToken);
            return result?.AsArray().Select(scene => scene?["name"]?.GetValue<string>() ?? "").Where(name => name.Length > 0).ToArray() ?? [];
        }
        using var obs = await ObsSession.ConnectAsync(settings, cancellationToken);
        var data = await obs.RequestAsync("GetSceneList", null, cancellationToken);
        return data["scenes"]?.AsArray().Select(scene => scene?["sceneName"]?.GetValue<string>() ?? "").Where(name => name.Length > 0).ToArray() ?? [];
    }

    public static async Task ExecuteAsync(ButtonAction action, StreamingConnections settings, CancellationToken cancellationToken)
    {
        StreamingCommand? command = StreamingActionCatalog.Find(action.ApplicationName, action.Target);
        if (command == null) throw new InvalidOperationException("Choisissez une commande OBS ou Streamlabs.");
        if (command.NeedsTarget && string.IsNullOrWhiteSpace(action.Arguments))
            throw new InvalidOperationException("Choisissez la scène ou la source à contrôler.");

        if (action.ApplicationName == "streamlabs")
        {
            using var client = await StreamlabsSession.ConnectAsync(settings, cancellationToken);
            await ExecuteStreamlabsAsync(client, action, cancellationToken);
            return;
        }
        using var obs = await ObsSession.ConnectAsync(settings, cancellationToken);
        await ExecuteObsAsync(obs, action, cancellationToken);
    }

    private static async Task ExecuteObsAsync(ObsSession obs, ButtonAction action, CancellationToken token)
    {
        string target = action.Arguments.Trim();
        switch (action.Target)
        {
            case "scene": await obs.RequestAsync("SetCurrentProgramScene", new { sceneName = target }, token); break;
            case "stream-start": await EnsureObsOutputAsync(obs, "GetStreamStatus", "outputActive", "StartStream", true, token); break;
            case "stream-stop": await EnsureObsOutputAsync(obs, "GetStreamStatus", "outputActive", "StopStream", false, token); break;
            case "record-start": await EnsureObsOutputAsync(obs, "GetRecordStatus", "outputActive", "StartRecord", true, token); break;
            case "record-stop": await EnsureObsOutputAsync(obs, "GetRecordStatus", "outputActive", "StopRecord", false, token); break;
            case "record-pause":
                var recordStatus = await obs.RequestAsync("GetRecordStatus", null, token);
                if (recordStatus["outputActive"]?.GetValue<bool>() == true && recordStatus["outputPaused"]?.GetValue<bool>() != true) await obs.RequestAsync("PauseRecord", null, token);
                break;
            case "record-resume":
                var pausedStatus = await obs.RequestAsync("GetRecordStatus", null, token);
                if (pausedStatus["outputActive"]?.GetValue<bool>() == true && pausedStatus["outputPaused"]?.GetValue<bool>() == true) await obs.RequestAsync("ResumeRecord", null, token);
                break;
            case "mic-toggle":
            case "desktop-audio-toggle": await ToggleObsInputAsync(obs, target, token); break;
            case "source-toggle": await ToggleObsSceneSourceAsync(obs, target, token); break;
            case "replay-start": await EnsureObsOutputAsync(obs, "GetReplayBufferStatus", "outputActive", "StartReplayBuffer", true, token); break;
            case "replay-stop": await EnsureObsOutputAsync(obs, "GetReplayBufferStatus", "outputActive", "StopReplayBuffer", false, token); break;
            case "replay-save": await obs.RequestAsync("SaveReplayBuffer", null, token); break;
            case "studio-toggle":
                var studio = await obs.RequestAsync("GetStudioModeEnabled", null, token);
                await obs.RequestAsync("SetStudioModeEnabled", new { studioModeEnabled = studio["studioModeEnabled"]?.GetValue<bool>() != true }, token);
                break;
            case "preview-transition": await obs.RequestAsync("TriggerStudioModeTransition", null, token); break;
            case "virtualcam-start": await EnsureObsOutputAsync(obs, "GetVirtualCamStatus", "outputActive", "StartVirtualCam", true, token); break;
            case "virtualcam-stop": await EnsureObsOutputAsync(obs, "GetVirtualCamStatus", "outputActive", "StopVirtualCam", false, token); break;
            default: throw new InvalidOperationException("Commande OBS inconnue.");
        }
    }

    private static async Task EnsureObsOutputAsync(ObsSession obs, string statusRequest, string activeField, string command, bool desired, CancellationToken token)
    {
        var status = await obs.RequestAsync(statusRequest, null, token);
        if (status[activeField]?.GetValue<bool>() != desired) await obs.RequestAsync(command, null, token);
    }

    private static async Task ToggleObsInputAsync(ObsSession obs, string inputName, CancellationToken token)
    {
        var status = await obs.RequestAsync("GetInputMute", new { inputName }, token);
        bool muted = status["inputMuted"]?.GetValue<bool>() == true;
        await obs.RequestAsync("SetInputMute", new { inputName, inputMuted = !muted }, token);
    }

    private static async Task ToggleObsSceneSourceAsync(ObsSession obs, string sourceName, CancellationToken token)
    {
        var current = await obs.RequestAsync("GetCurrentProgramScene", null, token);
        string sceneName = current["sceneName"]?.GetValue<string>() ?? throw new IOException("OBS n’a pas renvoyé la scène active.");
        var items = await obs.RequestAsync("GetSceneItemList", new { sceneName }, token);
        var item = items["sceneItems"]?.AsArray().FirstOrDefault(row => string.Equals(row?["sourceName"]?.GetValue<string>(), sourceName, StringComparison.OrdinalIgnoreCase));
        if (item == null) throw new IOException($"Source « {sourceName} » introuvable dans la scène « {sceneName} ».");
        int itemId = item["sceneItemId"]?.GetValue<int>() ?? throw new IOException("OBS n’a pas renvoyé l’identifiant de cette source.");
        var state = await obs.RequestAsync("GetSceneItemEnabled", new { sceneName, sceneItemId = itemId }, token);
        bool enabled = state["sceneItemEnabled"]?.GetValue<bool>() == true;
        await obs.RequestAsync("SetSceneItemEnabled", new { sceneName, sceneItemId = itemId, sceneItemEnabled = !enabled }, token);
    }

    private static async Task ExecuteStreamlabsAsync(StreamlabsSession client, ButtonAction action, CancellationToken token)
    {
        string target = action.Arguments.Trim();
        switch (action.Target)
        {
            case "scene":
                var scenes = await client.RequestAsync("ScenesService", "getScenes", [], token);
                var scene = scenes?.AsArray().FirstOrDefault(row => string.Equals(row?["name"]?.GetValue<string>(), target, StringComparison.OrdinalIgnoreCase));
                string id = scene?["id"]?.GetValue<string>() ?? throw new IOException($"Scène Streamlabs « {target} » introuvable.");
                await client.RequestAsync("ScenesService", "makeSceneActive", [JsonValue.Create(id)], token);
                break;
            case "stream-start":
            case "stream-stop": await EnsureStreamlabsOutputAsync(client, "streamingStatus", action.Target == "stream-start" ? "live" : "offline", "toggleStreaming", token); break;
            case "record-start":
            case "record-stop": await EnsureStreamlabsOutputAsync(client, "recordingStatus", action.Target == "record-start" ? "recording" : "offline", "toggleRecording", token); break;
            case "mic-toggle":
            case "desktop-audio-toggle": await ToggleStreamlabsAudioAsync(client, target, token); break;
            case "source-toggle": await ToggleStreamlabsSceneItemAsync(client, target, token); break;
            case "replay-start": await client.RequestAsync("StreamingService", "startReplayBuffer", [], token); break;
            case "replay-stop": await client.RequestAsync("StreamingService", "stopReplayBuffer", [], token); break;
            case "replay-save": await client.RequestAsync("StreamingService", "saveReplay", [], token); break;
            default: throw new InvalidOperationException("Commande Streamlabs inconnue.");
        }
    }

    private static async Task EnsureStreamlabsOutputAsync(StreamlabsSession client, string statusName, string desired, string toggleMethod, CancellationToken token)
    {
        var model = await client.RequestAsync("StreamingService", "getModel", [], token);
        string status = model?[statusName]?.GetValue<string>() ?? "offline";
        bool transitional = statusName == "streamingStatus"
            ? status is "starting" or "reconnecting" or "ending"
            : status is "starting" or "start" or "stopping" or "writing";
        if (transitional) return;
        bool active = statusName == "streamingStatus" ? status == "live" : status == "recording";
        if ((desired == "live") != active) await client.RequestAsync("StreamingService", toggleMethod, [], token);
    }

    private static async Task ToggleStreamlabsAudioAsync(StreamlabsSession client, string sourceName, CancellationToken token)
    {
        var sources = await client.RequestAsync("AudioService", "getSourcesForCurrentScene", [], token);
        var source = sources?.AsArray().FirstOrDefault(row => string.Equals(row?["name"]?.GetValue<string>(), sourceName, StringComparison.OrdinalIgnoreCase));
        string id = source?["sourceId"]?.GetValue<string>() ?? throw new IOException($"Source audio Streamlabs « {sourceName} » introuvable dans la scène active.");
        bool muted = source?["muted"]?.GetValue<bool>() == true;
        await client.RequestAsync("SourcesService", "setMuted", [JsonValue.Create(id), JsonValue.Create(!muted)], token);
    }

    private static async Task ToggleStreamlabsSceneItemAsync(StreamlabsSession client, string sourceName, CancellationToken token)
    {
        var activeScene = await client.RequestAsync("ScenesService", "activeScene", [], token);
        string resource = activeScene?["resourceId"]?.GetValue<string>() ?? throw new IOException("Streamlabs n’a pas renvoyé la scène active.");
        var items = await client.RequestAsync(resource, "getItems", [], token);
        var item = items?.AsArray().FirstOrDefault(row => string.Equals(row?["name"]?.GetValue<string>(), sourceName, StringComparison.OrdinalIgnoreCase));
        string itemResource = item?["resourceId"]?.GetValue<string>() ?? throw new IOException($"Source « {sourceName} » introuvable dans la scène active Streamlabs.");
        bool visible = item?["visible"]?.GetValue<bool>() == true;
        await client.RequestAsync(itemResource, "setVisibility", [JsonValue.Create(!visible)], token);
    }

    private static Uri SocketUri(string host, int port, string path)
    {
        if (Uri.CheckHostName(host) is not (UriHostNameType.Dns or UriHostNameType.IPv4 or UriHostNameType.IPv6))
            throw new InvalidOperationException("Saisissez une adresse de serveur valide.");
        return new UriBuilder("ws", host, port, path).Uri;
    }

    private static async Task<JsonNode> ReceiveJsonAsync(ClientWebSocket socket, CancellationToken cancellationToken)
    {
        using var buffer = new MemoryStream();
        byte[] chunk = new byte[8192];
        WebSocketReceiveResult result;
        do
        {
            result = await socket.ReceiveAsync(new ArraySegment<byte>(chunk), cancellationToken);
            if (result.MessageType == WebSocketMessageType.Close) throw new IOException("L’application de streaming a fermé la connexion.");
            buffer.Write(chunk, 0, result.Count);
            if (buffer.Length > 1024 * 1024) throw new IOException("La réponse du serveur de streaming est trop grande.");
        } while (!result.EndOfMessage);
        return JsonNode.Parse(buffer.ToArray()) ?? throw new IOException("Réponse vide du serveur de streaming.");
    }

    private sealed class ObsSession(ClientWebSocket socket) : IDisposable
    {
        private int nextId;
        public static async Task<ObsSession> ConnectAsync(StreamingConnections settings, CancellationToken token)
        {
            var socket = new ClientWebSocket();
            using var timeout = CancellationTokenSource.CreateLinkedTokenSource(token);
            timeout.CancelAfter(RequestTimeout);
            try
            {
                await socket.ConnectAsync(SocketUri(settings.ObsHost, settings.ObsPort, "/"), timeout.Token);
                var hello = await ReceiveJsonAsync(socket, timeout.Token);
                if (hello["op"]?.GetValue<int>() != 0) throw new IOException("OBS a renvoyé une réponse WebSocket inattendue.");
                JsonNode identify = new JsonObject { ["rpcVersion"] = 1 };
                var authentication = hello["d"]?["authentication"];
                if (authentication != null)
                {
                    string password = StreamingSecretProtection.Unprotect(settings.ObsPasswordProtected);
                    if (string.IsNullOrEmpty(password)) throw new InvalidOperationException("Saisissez le mot de passe OBS WebSocket dans la configuration de cette action.");
                    string salt = authentication["salt"]?.GetValue<string>() ?? "";
                    string challenge = authentication["challenge"]?.GetValue<string>() ?? "";
                    string secret = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password + salt)));
                    string response = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(secret + challenge)));
                    identify["authentication"] = response;
                }
                await SendJsonAsync(socket, new JsonObject { ["op"] = 1, ["d"] = identify }, timeout.Token);
                var identified = await ReceiveJsonAsync(socket, timeout.Token);
                if (identified["op"]?.GetValue<int>() != 2) throw new IOException("OBS a refusé l’identification. Vérifiez le mot de passe WebSocket.");
                return new ObsSession(socket);
            }
            catch (OperationCanceledException) when (!token.IsCancellationRequested)
            {
                socket.Dispose(); throw new TimeoutException("Délai de connexion dépassé. Vérifiez qu’OBS est ouvert et que son serveur WebSocket est activé.");
            }
            catch { socket.Dispose(); throw; }
        }

        public async Task<JsonObject> RequestAsync(string requestType, object? data, CancellationToken token)
        {
            using var timeout = CancellationTokenSource.CreateLinkedTokenSource(token);
            timeout.CancelAfter(RequestTimeout);
            string id = Interlocked.Increment(ref nextId).ToString(System.Globalization.CultureInfo.InvariantCulture);
            var requestData = JsonSerializer.SerializeToNode(data) as JsonObject ?? new JsonObject();
            await SendJsonAsync(socket, new JsonObject { ["op"] = 6, ["d"] = new JsonObject { ["requestType"] = requestType, ["requestId"] = id, ["requestData"] = requestData } }, timeout.Token);
            while (true)
            {
                var response = await ReceiveJsonAsync(socket, timeout.Token);
                if (response["op"]?.GetValue<int>() != 7 || response["d"]?["requestId"]?.GetValue<string>() != id) continue;
                var result = response["d"]?["requestStatus"];
                if (result?["result"]?.GetValue<bool>() != true)
                    throw new IOException(result?["comment"]?.GetValue<string>() ?? $"La commande OBS « {requestType} » a échoué.");
                return response["d"]?["responseData"] as JsonObject ?? new JsonObject();
            }
        }

        public void Dispose() { try { socket.Abort(); } catch { } socket.Dispose(); }
    }

    private sealed class StreamlabsSession(ClientWebSocket socket) : IDisposable
    {
        private int nextId;
        public static async Task<StreamlabsSession> ConnectAsync(StreamingConnections settings, CancellationToken token)
        {
            var socket = new ClientWebSocket();
            using var timeout = CancellationTokenSource.CreateLinkedTokenSource(token);
            timeout.CancelAfter(RequestTimeout);
            try
            {
                string serverId = Random.Shared.Next(0, 1000).ToString("000", System.Globalization.CultureInfo.InvariantCulture);
                string sessionId = Guid.NewGuid().ToString("N");
                string path = $"/api/{serverId}/{sessionId}/websocket";
                await socket.ConnectAsync(SocketUri(settings.StreamlabsHost, settings.StreamlabsPort, path), timeout.Token);
                var session = new StreamlabsSession(socket);
                string secret = StreamingSecretProtection.Unprotect(settings.StreamlabsTokenProtected);
                if (string.IsNullOrWhiteSpace(secret)) throw new InvalidOperationException("Saisissez le jeton Remote Control Streamlabs dans la configuration de cette action.");
                await session.ReadOpenFrameAsync(timeout.Token);
                await session.RequestAsync("TcpServerService", "auth", [JsonValue.Create(secret)], timeout.Token);
                return session;
            }
            catch (OperationCanceledException) when (!token.IsCancellationRequested)
            {
                socket.Dispose(); throw new TimeoutException("Délai de connexion dépassé. Vérifiez que Streamlabs Desktop est ouvert et que Remote Control est activé.");
            }
            catch { socket.Dispose(); throw; }
        }

        public async Task<JsonNode?> RequestAsync(string resource, string method, JsonArray args, CancellationToken token)
        {
            using var timeout = CancellationTokenSource.CreateLinkedTokenSource(token);
            timeout.CancelAfter(RequestTimeout);
            int id = Interlocked.Increment(ref nextId);
            string json = JsonSerializer.Serialize(new JsonObject
            {
                ["jsonrpc"] = "2.0", ["id"] = id, ["method"] = method,
                ["params"] = new JsonObject { ["resource"] = resource, ["args"] = args.DeepClone() }
            });
            string sockFrame = "a" + JsonSerializer.Serialize(new[] { json });
            await socket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(sockFrame)), WebSocketMessageType.Text, true, timeout.Token);
            while (true)
            {
                JsonNode? message = await ReceiveSockFrameAsync(timeout.Token);
                if (message is not JsonObject response || response["id"]?.GetValue<int>() != id) continue;
                if (response["error"] is JsonNode error) throw new IOException(error["message"]?.GetValue<string>() ?? $"La commande Streamlabs « {method} » a échoué.");
                return response["result"]?.DeepClone();
            }
        }

        private async Task ReadOpenFrameAsync(CancellationToken token)
        {
            while (true)
            {
                string frame = await ReceiveTextAsync(token);
                if (frame == "o") return;
                if (frame.StartsWith("c", StringComparison.Ordinal)) throw new IOException("Streamlabs a refusé la connexion Remote Control.");
            }
        }

        private async Task<JsonNode?> ReceiveSockFrameAsync(CancellationToken token)
        {
            while (true)
            {
                string frame = await ReceiveTextAsync(token);
                if (frame == "h" || frame == "o") continue;
                if (frame.StartsWith("c", StringComparison.Ordinal)) throw new IOException("Streamlabs a fermé la connexion Remote Control.");
                if (!frame.StartsWith("a", StringComparison.Ordinal)) continue;
                var messages = JsonNode.Parse(frame[1..])?.AsArray();
                foreach (var item in messages ?? [])
                {
                    string? value = item?.GetValue<string>();
                    if (value != null) return JsonNode.Parse(value);
                }
            }
        }

        private async Task<string> ReceiveTextAsync(CancellationToken token)
        {
            using var buffer = new MemoryStream();
            byte[] chunk = new byte[8192];
            WebSocketReceiveResult result;
            do
            {
                result = await socket.ReceiveAsync(new ArraySegment<byte>(chunk), token);
                if (result.MessageType == WebSocketMessageType.Close) throw new IOException("Streamlabs a fermé la connexion Remote Control.");
                buffer.Write(chunk, 0, result.Count);
                if (buffer.Length > 1024 * 1024) throw new IOException("La réponse Streamlabs est trop grande.");
            } while (!result.EndOfMessage);
            return Encoding.UTF8.GetString(buffer.ToArray());
        }

        public void Dispose() { try { socket.Abort(); } catch { } socket.Dispose(); }
    }

    private static Task SendJsonAsync(ClientWebSocket socket, JsonObject message, CancellationToken token) =>
        socket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(message.ToJsonString())), WebSocketMessageType.Text, true, token);
}
