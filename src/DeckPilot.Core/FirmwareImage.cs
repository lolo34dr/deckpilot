using System.Globalization;
using System.Security.Cryptography;
using System.Text.Json;

namespace DeckPilot.Core;

public sealed record FirmwareIdentity(string Version)
{
    public const string Board = "promicro-32u4-16mhz";
    public static FirmwareIdentity? Parse(string line)
    {
        var parts = line.Trim().Split('|');
        return parts.Length == 4 && parts[0] == "DECKPILOT" && parts[1] == "1" && parts[2] == Board
            && System.Version.TryParse(parts[3], out var version) && version.Build >= 0 && version.Revision == -1
            ? new(parts[3]) : null;
    }
}

public sealed record FirmwareManifest(string Board, string Version, string Sha256)
{
    public static FirmwareManifest Read(string json, byte[] image)
    {
        var manifest = JsonSerializer.Deserialize<FirmwareManifest>(json) ?? throw new InvalidDataException("Manifeste firmware absent.");
        if (manifest.Board != FirmwareIdentity.Board || !System.Version.TryParse(manifest.Version, out var version) || version.Build < 0 || version.Revision != -1)
            throw new InvalidDataException("Ce firmware ne correspond pas à la carte prise en charge.");
        if (!string.Equals(Convert.ToHexString(SHA256.HashData(image)), manifest.Sha256, StringComparison.OrdinalIgnoreCase))
            throw new InvalidDataException("Le firmware est endommagé (SHA-256).");
        ValidateHex(System.Text.Encoding.ASCII.GetString(image));
        return manifest;
    }
    public static void ValidateHex(string text)
    {
        // Leonardo reserves the upper 4 KiB for Caterina. Never accept bootloader writes.
        const int maximum = 28672;
        var occupied = new HashSet<uint>();
        uint upper = 0;
        bool ended = false;
        foreach (string line in text.Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
        {
            if (ended || line.Length < 11 || line[0] != ':' || (line.Length & 1) == 0)
                throw new InvalidDataException("Format Intel HEX invalide.");
            byte[] record;
            try { record = Convert.FromHexString(line[1..]); }
            catch (FormatException) { throw new InvalidDataException("Format Intel HEX invalide."); }
            if (record.Length != record[0] + 5 || record.Sum(b => (int)b) % 256 != 0)
                throw new InvalidDataException("Somme de contrôle Intel HEX invalide.");
            int length = record[0], address = (record[1] << 8) | record[2];
            switch (record[3])
            {
                case 0:
                    uint start = upper + (uint)address;
                    if ((ulong)start + (uint)length > maximum) throw new InvalidDataException("Écriture hors de la mémoire programme autorisée.");
                    for (uint i = 0; i < length; i++)
                        if (!occupied.Add(start + i)) throw new InvalidDataException("Zones firmware superposées.");
                    break;
                case 1 when length == 0 && address == 0: ended = true; break;
                case 2 when length == 2 && address == 0: upper = (uint)((record[4] << 8) | record[5]) << 4; break;
                case 4 when length == 2 && address == 0: upper = (uint)((record[4] << 8) | record[5]) << 16; break;
                default: throw new InvalidDataException("Type d’enregistrement firmware non autorisé.");
            }
        }
        if (!ended || occupied.Count == 0 || !occupied.Contains(0)) throw new InvalidDataException("Firmware incomplet.");
    }
}
