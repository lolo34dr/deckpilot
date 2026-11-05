namespace DeckPilot.Core;

public static class VirtualCableEndpoints
{
    public static bool IsCableDevice(string? adapterName) =>
        string.Equals(adapterName, "VB-Audio Virtual Cable", StringComparison.OrdinalIgnoreCase);

    public static bool IsPlaybackEndpoint(string? name) =>
        name?.StartsWith("CABLE Input", StringComparison.OrdinalIgnoreCase) == true;

    public static bool IsRecordingEndpoint(string? name) =>
        name?.StartsWith("CABLE Output", StringComparison.OrdinalIgnoreCase) == true;
}
