using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using DeckPilot.Core;

internal static class FirmwareChecks
{
    public static int Run()
    {
        int count = 0;
        void Check(string name, Action action) { action(); count++; Console.WriteLine("PASS " + name); }
        void Expect(bool value) { if (!value) throw new Exception("Firmware assertion failed"); }
        void Reject(Action action)
        {
            try { action(); } catch (InvalidDataException) { return; }
            throw new Exception("Unsafe firmware accepted");
        }
        string Record(byte type, ushort address, params byte[] data)
        {
            byte[] record = [(byte)data.Length, (byte)(address >> 8), (byte)address, type, ..data, 0];
            record[^1] = unchecked((byte)-record.Sum(b => (int)b));
            return ":" + Convert.ToHexString(record) + "\n";
        }
        string eof = Record(1, 0), valid = Record(0, 0, 0x0c, 0x94, 0x10, 0) + eof;
        string Manifest(byte[] image, string board = FirmwareIdentity.Board, string version = "1.0.0") => JsonSerializer.Serialize(new { Board = board, Version = version, Sha256 = Convert.ToHexString(SHA256.HashData(image)) });
        Check("Firmware identity requires the DeckPilot protocol and exact board", () => {
            Expect(FirmwareIdentity.Parse("DECKPILOT|1|promicro-32u4-16mhz|1.0.0\r\n")?.Version == "1.0.0");
            foreach (string line in new[] { "Arduino Leonardo", "DECKPILOT|2|promicro-32u4-16mhz|1.0.0", "DECKPILOT|1|other|1.0.0", "DECKPILOT|1|promicro-32u4-16mhz|1.0", "DECKPILOT|1|promicro-32u4-16mhz|1.0.0|extra" }) Expect(FirmwareIdentity.Parse(line) == null);
        });
        Check("Valid application HEX and SHA256 manifest are accepted", () => {
            var bytes = Encoding.ASCII.GetBytes(valid);
            Expect(FirmwareManifest.Read(Manifest(bytes), bytes).Version == "1.0.0");
            FirmwareManifest.ValidateHex(Record(4, 0, 0, 0) + valid);
            FirmwareManifest.ValidateHex(Record(2, 0, 0, 0) + valid);
        });
        Check("Firmware hash corruption, wrong board and malformed version are rejected", () => {
            var bytes = Encoding.ASCII.GetBytes(valid);
            Reject(() => FirmwareManifest.Read(Manifest(bytes), Encoding.ASCII.GetBytes(valid + " ")));
            Reject(() => FirmwareManifest.Read(Manifest(bytes, "other"), bytes));
            Reject(() => FirmwareManifest.Read(Manifest(bytes, version: "1.0"), bytes));
        });
        Check("Bootloader, extended-address and boundary-crossing writes are rejected", () => {
            Reject(() => FirmwareManifest.ValidateHex(Record(0, 0x7000, 1, 2) + eof));
            Reject(() => FirmwareManifest.ValidateHex(Record(0, 0x6fff, 1, 2) + eof));
            Reject(() => FirmwareManifest.ValidateHex(Record(4, 0, 0xff, 0xff) + Record(0, 0xffff, 1, 2) + eof));
            Reject(() => FirmwareManifest.ValidateHex(Record(2, 0, 0x10, 0) + valid));
        });
        Check("Missing EOF, trailing writes, overlap and absent reset vector are rejected", () => {
            Reject(() => FirmwareManifest.ValidateHex(Record(0, 0, 1)));
            Reject(() => FirmwareManifest.ValidateHex(eof));
            Reject(() => FirmwareManifest.ValidateHex(valid + Record(0, 10, 1)));
            Reject(() => FirmwareManifest.ValidateHex(Record(0, 0, 1, 2) + Record(0, 1, 3) + eof));
            Reject(() => FirmwareManifest.ValidateHex(Record(0, 10, 1) + eof));
        });
        Check("Invalid HEX checksums, lengths and record types are rejected", () => {
            Reject(() => FirmwareManifest.ValidateHex(":010000000100\n" + eof));
            Reject(() => FirmwareManifest.ValidateHex(":GG\n"));
            Reject(() => FirmwareManifest.ValidateHex(Record(3, 0, 0, 0, 0, 0) + valid));
            Reject(() => FirmwareManifest.ValidateHex(":0200000001FD\n" + eof));
        });
        string compiled = Path.Combine("artifacts", "firmware-build", "DeckPilot.ino.hex");
        if (File.Exists(compiled)) Check("The compiled ATmega32U4 image stays below Caterina", () => FirmwareManifest.ValidateHex(File.ReadAllText(compiled)));
        string reset = Path.Combine("artifacts", "firmware-stage", "Blank.hex");
        if (File.Exists(reset)) Check("The reset image clears only the 28 KiB application area and leaves a safe reset loop", () => {
            string image = File.ReadAllText(reset);
            FirmwareManifest.ValidateHex(image);
            var bytes = new SortedDictionary<int, byte>();
            foreach (string line in image.Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
            {
                var record = Convert.FromHexString(line[1..]);
                if (record[3] != 0) continue;
                int address = (record[1] << 8) | record[2];
                for (int i = 0; i < record[0]; i++) bytes.Add(address + i, record[4 + i]);
            }
            Expect(bytes.Count == 28672 && bytes[0] == 0xFF && bytes[1] == 0xCF);
            Expect(bytes.All(pair => pair.Value == 0xFF || pair.Key == 1));
            Expect(bytes.Keys.Min() == 0 && bytes.Keys.Max() == 28671);
        });
        return count;
    }
}
