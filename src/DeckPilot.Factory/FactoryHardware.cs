using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.IO.Ports;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using DeckPilot.Core;

namespace DeckPilot.Factory;

internal sealed record FactoryDevice(string Port, string Name, string Id, string Location, bool Bootloader)
{
    public override string ToString() => $"{Name}  ·  {Port}";
}

internal sealed record FirmwarePackage(string Folder, FirmwareManifest Manifest) : IDisposable
{
    public void Dispose() { try { Directory.Delete(Folder, true); } catch { } }
}

internal static class FactoryHardware
{
    private const string ResourceName = "DeckPilot.Factory.FirmwareBundle.zip";
    public static FirmwareManifest? BundleInfo()
    {
        using var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(ResourceName);
        if (resource == null) return null;
        using var archive = new ZipArchive(resource, ZipArchiveMode.Read);
        var hex = archive.GetEntry("DeckPilot.hex") ?? throw new InvalidDataException("Image firmware manquante.");
        var manifest = archive.GetEntry("manifest.json") ?? throw new InvalidDataException("Manifeste firmware manquant.");
        if (hex.Length > 128 * 1024 || manifest.Length > 4096) throw new InvalidDataException("Archive firmware invalide.");
        using var bytes = new MemoryStream(); using (var stream = hex.Open()) stream.CopyTo(bytes);
        using var reader = new StreamReader(manifest.Open());
        return FirmwareManifest.Read(reader.ReadToEnd(), bytes.ToArray());
    }

    public static FirmwarePackage Prepare()
    {
        using var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(ResourceName)
            ?? throw new IOException("Le firmware DeckPilot n’est pas inclus dans cet installateur.");
        string folder = Path.Combine(Path.GetTempPath(), "DeckPilotFactory", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(folder);
        try
        {
            using var archive = new ZipArchive(resource, ZipArchiveMode.Read);
            long total = 0;
            foreach (var entry in archive.Entries)
            {
                total += entry.Length;
                string target = Path.GetFullPath(Path.Combine(folder, entry.FullName));
                if (!target.StartsWith(folder + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase) || total > 150 * 1024 * 1024)
                    throw new InvalidDataException("Archive firmware invalide.");
                if (entry.Name.Length == 0) { Directory.CreateDirectory(target); continue; }
                Directory.CreateDirectory(Path.GetDirectoryName(target)!);
                entry.ExtractToFile(target, false);
            }
            byte[] image = File.ReadAllBytes(Path.Combine(folder, "DeckPilot.hex"));
            var manifest = FirmwareManifest.Read(File.ReadAllText(Path.Combine(folder, "manifest.json")), image);
            if (!File.Exists(Path.Combine(folder, "tools", "bin", "avrdude.exe")) || !File.Exists(Path.Combine(folder, "tools", "etc", "avrdude.conf")))
                throw new InvalidDataException("Outil de téléversement manquant.");
            return new(folder, manifest);
        }
        catch { try { Directory.Delete(folder, true); } catch { } throw; }
    }

    public static IReadOnlyList<FactoryDevice> Discover()
    {
        var result = new List<FactoryDevice>();
        using var search = new ManagementObjectSearcher("SELECT DeviceID, Name, PNPDeviceID FROM Win32_SerialPort");
        search.Options.Timeout = TimeSpan.FromSeconds(4);
        using var objects = search.Get();
        foreach (ManagementObject item in objects)
        {
            using (item)
            {
                string id = item["PNPDeviceID"] as string ?? "";
                bool Leonardo = HasId(id, "8036") || HasId(id, "0036");
                if (!Leonardo) continue;
                string port = item["DeviceID"] as string ?? "";
                if (!System.Text.RegularExpressions.Regex.IsMatch(port, @"^COM[0-9]{1,4}$")) continue;
                result.Add(new(port, item["Name"] as string ?? "Arduino Leonardo", id, PhysicalLocation(id), HasId(id, "0036")));
            }
        }
        return result.OrderBy(d => d.Port, StringComparer.OrdinalIgnoreCase).ToArray();
    }

    private static bool HasId(string id, string pid) => id.StartsWith($@"USB\VID_2341&PID_{pid}", StringComparison.OrdinalIgnoreCase) || id.StartsWith($@"USB\VID_2A03&PID_{pid}", StringComparison.OrdinalIgnoreCase);

    private static string PhysicalLocation(string id)
    {
        if (CM_Locate_DevNodeW(out uint node, id, 0) != 0) return "";
        for (int depth = 0; depth < 8; depth++)
        {
            var deviceId = new StringBuilder(512);
            if (CM_Get_Device_IDW(node, deviceId, deviceId.Capacity, 0) != 0) return "";
            string current = deviceId.ToString();
            if (current.StartsWith(@"USB\VID_", StringComparison.OrdinalIgnoreCase) && !current.Contains("&MI_", StringComparison.OrdinalIgnoreCase))
            {
                byte[] buffer = new byte[4096]; uint size = (uint)buffer.Length;
                if (CM_Get_DevNode_Registry_PropertyW(node, 0xE, out _, buffer, ref size, 0) != 0 || CM_Get_Parent(out uint parent, node, 0) != 0) return "";
                var parentId = new StringBuilder(512);
                if (CM_Get_Device_IDW(parent, parentId, parentId.Capacity, 0) != 0) return "";
                string connection = Encoding.Unicode.GetString(buffer, 0, (int)size).TrimEnd('\0');
                return string.IsNullOrWhiteSpace(connection) ? "" : parentId + "|" + connection;
            }
            if (CM_Get_Parent(out uint next, node, 0) != 0) return "";
            node = next;
        }
        return "";
    }

    public static async Task FlashAsync(FactoryDevice selected, IProgress<string> progress, CancellationToken cancellation)
    {
        if (string.IsNullOrWhiteSpace(selected.Location)) throw new IOException("Impossible de confirmer l’emplacement USB physique. Aucun flash ne sera lancé.");
        var device = Discover().SingleOrDefault(d => d.Id == selected.Id && d.Port == selected.Port && d.Location == selected.Location)
            ?? throw new IOException("La carte sélectionnée a changé ou a été débranchée. Relance la détection.");
        using var package = Prepare();
        FactoryDevice boot;
        if (device.Bootloader) boot = device;
        else
        {
            progress.Report("Redémarrage vers le bootloader Caterina…");
            using (var port = new SerialPort(device.Port, 1200) { DtrEnable = true }) { port.Open(); port.DtrEnable = false; }
            boot = await WaitForAsync(device.Location, true, TimeSpan.FromSeconds(20), cancellation);
        }
        progress.Report($"Écriture vérifiée sur {boot.Port}… Ne débranche pas la carte.");
        string exe = Path.Combine(package.Folder, "tools", "bin", "avrdude.exe");
        var start = new ProcessStartInfo(exe) { UseShellExecute = false, CreateNoWindow = true, RedirectStandardError = true, RedirectStandardOutput = true, WorkingDirectory = Path.GetDirectoryName(exe)! };
        foreach (string arg in new[] { "-C" + Path.Combine(package.Folder, "tools", "etc", "avrdude.conf"), "-p", "atmega32u4", "-c", "avr109", "-P", boot.Port, "-b", "57600", "-D", "-U", "flash:w:" + Path.Combine(package.Folder, "DeckPilot.hex") + ":i" }) start.ArgumentList.Add(arg);
        using var process = Process.Start(start) ?? throw new IOException("AVRDUDE n’a pas pu démarrer.");
        Task<string> stdout = process.StandardOutput.ReadToEndAsync(), stderr = process.StandardError.ReadToEndAsync();
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cancellation); timeout.CancelAfter(TimeSpan.FromMinutes(2));
        try { await process.WaitForExitAsync(timeout.Token); }
        catch (OperationCanceledException) { if (!process.HasExited) process.Kill(true); await process.WaitForExitAsync(); throw new IOException("Téléversement interrompu ou délai dépassé. Le firmware peut être incomplet ; rebranche la carte et relance l’installation."); }
        string log = await stdout + "\n" + await stderr;
        string logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DeckPilotFactory", "last-flash.log");
        Directory.CreateDirectory(Path.GetDirectoryName(logPath)!); await File.WriteAllTextAsync(logPath, log, cancellation);
        if (process.ExitCode != 0) throw new IOException($"AVRDUDE a signalé une erreur ({process.ExitCode}). Journal : {logPath}");
        progress.Report("Écriture vérifiée. Attente du redémarrage…");
        var runtime = await WaitForAsync(device.Location, false, TimeSpan.FromSeconds(25), cancellation);
        using var transport = new FactoryTransport(runtime.Port);
        var client = new BoardProtocolClient(transport);
        var identity = await Task.Run(() => client.Identify(cancellation), cancellation);
        if (identity.Version != package.Manifest.Version) throw new IOException("La carte répond, mais avec une version firmware inattendue.");
        progress.Report($"Firmware {identity.Version} installé, relu et vérifié.");
    }

    private static async Task<FactoryDevice> WaitForAsync(string location, bool boot, TimeSpan duration, CancellationToken token)
    {
        var watch = Stopwatch.StartNew();
        while (watch.Elapsed < duration)
        {
            token.ThrowIfCancellationRequested();
            var matches = Discover().Where(d => d.Location == location && d.Bootloader == boot).ToArray();
            if (matches.Length > 1) throw new IOException("Plusieurs ports correspondent à ce périphérique USB ; aucune carte n’a été choisie.");
            if (matches.Length == 1) return matches[0];
            await Task.Delay(250, token);
        }
        throw new IOException(boot ? "Bootloader Caterina introuvable sur ce port USB." : "La carte ne s’est pas reconnectée après le flash.");
    }

    [DllImport("cfgmgr32.dll", CharSet = CharSet.Unicode)] private static extern uint CM_Locate_DevNodeW(out uint node, string id, uint flags);
    [DllImport("cfgmgr32.dll")] private static extern uint CM_Get_Parent(out uint parent, uint node, uint flags);
    [DllImport("cfgmgr32.dll", CharSet = CharSet.Unicode)] private static extern uint CM_Get_Device_IDW(uint node, StringBuilder id, int length, uint flags);
    [DllImport("cfgmgr32.dll", CharSet = CharSet.Unicode)] private static extern uint CM_Get_DevNode_Registry_PropertyW(uint node, uint property, out uint type, byte[] buffer, ref uint length, uint flags);
}

internal sealed class FactoryTransport : IBoardTransport
{
    private readonly SerialPort port;
    private readonly StringBuilder line = new();
    public FactoryTransport(string name)
    {
        port = new SerialPort(name, 115200) { DtrEnable = true, ReadTimeout = 100, WriteTimeout = 500, NewLine = "\n" };
        port.Open(); port.DiscardInBuffer();
    }
    public void WriteLine(string value) => port.WriteLine(value);
    public string ReadLine()
    {
        long until = Environment.TickCount64 + 100;
        while (Environment.TickCount64 < until)
        {
            int value = port.ReadByte();
            if (value == '\n') { string result = line.ToString(); line.Clear(); return result; }
            if (value == '\r') continue;
            if (value < 32 || value > 126 || line.Length >= 127) throw new IOException("Données série invalides.");
            line.Append((char)value);
        }
        throw new TimeoutException();
    }
    public void Dispose() => port.Dispose();
}
