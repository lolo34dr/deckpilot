using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DeckPilot.Core;

namespace DeckPilot.Windows;

public sealed record FirmwareDevice(string Port, string Name, string DeviceId, string Location, bool Bootloader)
{
    public override string ToString() => $"{Name} · {Port}";
}
public sealed record PreparedFirmware(string Directory, FirmwareManifest Manifest) : IDisposable
{
    public void Dispose() { try { System.IO.Directory.Delete(Directory, true); } catch (IOException) { } catch (UnauthorizedAccessException) { } }
}

public static class FirmwareService
{
    public static bool BundleAvailable => Assembly.GetExecutingAssembly().GetManifestResourceInfo("DeckPilot.FirmwareBundle.zip") != null;
    public static PreparedFirmware Prepare()
    {
        using var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("DeckPilot.FirmwareBundle.zip")
            ?? throw new IOException("Ce build ne contient pas le firmware. Utilisez une version publiée de DeckPilot.");
        string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DeckPilot", "Firmware", Guid.NewGuid().ToString("N"));
        System.IO.Directory.CreateDirectory(directory);
        try
        {
            using var archive = new ZipArchive(resource, ZipArchiveMode.Read);
            long total = 0;
            foreach (var entry in archive.Entries)
            {
                total += entry.Length;
                string target = Path.GetFullPath(Path.Combine(directory, entry.FullName));
                if (!target.StartsWith(directory + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase) || total > 150 * 1024 * 1024)
                    throw new InvalidDataException("Archive firmware invalide.");
                if (entry.Name.Length == 0) { System.IO.Directory.CreateDirectory(target); continue; }
                System.IO.Directory.CreateDirectory(Path.GetDirectoryName(target)!);
                entry.ExtractToFile(target, false);
            }
            byte[] image = File.ReadAllBytes(Path.Combine(directory, "DeckPilot.hex"));
            var manifest = FirmwareManifest.Read(File.ReadAllText(Path.Combine(directory, "manifest.json")), image);
            if (!File.Exists(Path.Combine(directory, "tools", "bin", "avrdude.exe")) || !File.Exists(Path.Combine(directory, "tools", "etc", "avrdude.conf")))
                throw new InvalidDataException("Outil de téléversement absent.");
            return new(directory, manifest);
        }
        catch { try { System.IO.Directory.Delete(directory, true); } catch (IOException) { } throw; }
    }
    public static List<FirmwareDevice> Discover()
    {
        var result = new List<FirmwareDevice>();
        using var search = new ManagementObjectSearcher("SELECT DeviceID, Name, PNPDeviceID FROM Win32_SerialPort");
        using var objects = search.Get();
        foreach (ManagementObject item in objects)
        {
            using (item)
            {
                string id = (string?)item["PNPDeviceID"] ?? "";
                bool runtime = KnownId(id, "8036"), boot = KnownId(id, "0036");
                if (!runtime && !boot) continue;
                string port = (string?)item["DeviceID"] ?? "";
                if (!System.Text.RegularExpressions.Regex.IsMatch(port, @"^COM[0-9]{1,4}$")) continue;
                result.Add(new(port, (string?)item["Name"] ?? "Arduino Leonardo", id, PhysicalLocation(id), boot));
            }
        }
        return result.OrderBy(d => d.Port).ToList();
    }
    private static bool KnownId(string id, string pid) =>
        id.StartsWith($@"USB\VID_2341&PID_{pid}", StringComparison.OrdinalIgnoreCase) ||
        id.StartsWith($@"USB\VID_2A03&PID_{pid}", StringComparison.OrdinalIgnoreCase);

    // Match the physical USB connection across runtime/composite and Caterina identities.
    // Never guess a new COM port just because it appeared during a flash.
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
                return parentId + "|" + Encoding.Unicode.GetString(buffer, 0, (int)size).TrimEnd('\0');
            }
            if (CM_Get_Parent(out uint next, node, 0) != 0) return "";
            node = next;
        }
        return "";
    }
    public static FirmwareIdentity? Identify(FirmwareDevice device)
    {
        if (device.Bootloader) return null;
        try
        {
            using var port = new SerialPort(device.Port, 115200) { DtrEnable = true, ReadTimeout = 1000, WriteTimeout = 1000, NewLine = "\n" };
            port.Open(); port.DiscardInBuffer(); port.WriteLine("DECKPILOT?");
            return FirmwareIdentity.Parse(port.ReadLine());
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or TimeoutException or InvalidOperationException) { return null; }
    }
    public static async Task FlashAsync(FirmwareDevice selected, PreparedFirmware package, IProgress<string> progress)
    {
        var current = await Task.Run(Discover);
        var device = current.SingleOrDefault(d => d.DeviceId == selected.DeviceId && d.Port == selected.Port && d.Location == selected.Location && !d.Bootloader)
            ?? throw new IOException("La carte sélectionnée a été débranchée ou a changé. Relancez la détection.");
        if (string.IsNullOrWhiteSpace(device.Location)) throw new IOException("Impossible de vérifier la connexion USB physique. Aucune écriture n’a été lancée.");
        var identity = await Task.Run(() => Identify(device));
        if (identity != null && Version.Parse(identity.Version) > Version.Parse(package.Manifest.Version))
            throw new IOException("La carte contient un firmware plus récent. Mettez DeckPilot à jour.");
        // Revalidate the exact bytes immediately before beginning any hardware operation.
        FirmwareManifest.Read(File.ReadAllText(Path.Combine(package.Directory, "manifest.json")), File.ReadAllBytes(Path.Combine(package.Directory, "DeckPilot.hex")));
        progress.Report("Redémarrage de la carte… Ne débranchez pas le câble USB.");
        await Task.Run(() =>
        {
            using var port = new SerialPort(device.Port, 1200) { DtrEnable = true };
            port.Open(); port.DtrEnable = false;
        });
        var boot = await WaitForDeviceAsync(device.Location, true, TimeSpan.FromSeconds(20));
        progress.Report($"Écriture et vérification sur {boot.Port}… Ne débranchez pas la carte.");
        string executable = Path.Combine(package.Directory, "tools", "bin", "avrdude.exe");
        var start = new ProcessStartInfo(executable) { UseShellExecute = false, CreateNoWindow = true, RedirectStandardError = true, RedirectStandardOutput = true, WorkingDirectory = Path.GetDirectoryName(executable)! };
        // -V is deliberately absent: AVRDUDE must verify the flash. Never touch fuses or bootloader.
        foreach (string argument in new[] { "-C" + Path.Combine(package.Directory, "tools", "etc", "avrdude.conf"), "-p", "atmega32u4", "-c", "avr109", "-P", boot.Port, "-b", "57600", "-D", "-U", "flash:w:" + Path.Combine(package.Directory, "DeckPilot.hex") + ":i" })
            start.ArgumentList.Add(argument);
        using var process = Process.Start(start) ?? throw new IOException("AVRDUDE n’a pas pu démarrer.");
        Task<string> stdout = process.StandardOutput.ReadToEndAsync(), stderr = process.StandardError.ReadToEndAsync();
        using var timeout = new CancellationTokenSource(TimeSpan.FromMinutes(2));
        try { await process.WaitForExitAsync(timeout.Token); }
        catch (OperationCanceledException)
        {
            if (!process.HasExited) process.Kill(true);
            await process.WaitForExitAsync();
            throw new IOException("Le téléversement a dépassé le délai. Le firmware peut être incomplet ; consultez les instructions de récupération.");
        }
        string log = await stdout + "\n" + await stderr;
        string logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DeckPilot", "Firmware", "last-flash.log");
        await File.WriteAllTextAsync(logPath, log);
        if (process.ExitCode != 0) throw new IOException($"AVRDUDE a signalé une erreur ({process.ExitCode}). Le firmware peut être incomplet. Journal : {logPath}");
        progress.Report("Écriture vérifiée. Attente du redémarrage de DeckPilot…");
        var runtime = await WaitForDeviceAsync(device.Location, false, TimeSpan.FromSeconds(25));
        for (int attempt = 0; attempt < 6; attempt++)
        {
            var health = await Task.Run(() => Identify(runtime));
            if (health?.Version == package.Manifest.Version) { progress.Report($"Firmware {health.Version} installé et identifié. Vos profils et votre calibrage sont conservés."); return; }
            await Task.Delay(500);
        }
        throw new IOException("L’écriture a été vérifiée, mais le firmware n’a pas répondu avec la version attendue. Fermez les autres outils série puis relancez la détection.");
    }
    private static async Task<FirmwareDevice> WaitForDeviceAsync(string location, bool bootloader, TimeSpan timeout)
    {
        var timer = Stopwatch.StartNew();
        while (timer.Elapsed < timeout)
        {
            var devices = await Task.Run(Discover);
            var matching = devices.Where(d => d.Location == location && d.Bootloader == bootloader).ToList();
            if (matching.Count > 1) throw new IOException("Plusieurs ports correspondent à la carte. Aucune carte ne sera choisie automatiquement.");
            if (matching.Count == 1) return matching[0];
            await Task.Delay(200);
        }
        throw new IOException(bootloader ? "Le bootloader Caterina n’a pas été détecté sur cette connexion USB. Aucune écriture n’a été lancée." : "La carte n’est pas réapparue après le flash. Consultez les instructions de récupération.");
    }
    [DllImport("cfgmgr32.dll", CharSet = CharSet.Unicode)] private static extern uint CM_Locate_DevNodeW(out uint node, string id, uint flags);
    [DllImport("cfgmgr32.dll")] private static extern uint CM_Get_Parent(out uint parent, uint node, uint flags);
    [DllImport("cfgmgr32.dll", CharSet = CharSet.Unicode)] private static extern uint CM_Get_Device_IDW(uint node, StringBuilder id, int length, uint flags);
    [DllImport("cfgmgr32.dll", CharSet = CharSet.Unicode)] private static extern uint CM_Get_DevNode_Registry_PropertyW(uint node, uint property, out uint type, byte[] buffer, ref uint length, uint flags);
}
