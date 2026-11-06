namespace DeckPilot.Core;

public enum ProfileOverlayPosition { TopLeft, TopRight, BottomLeft, BottomRight, Custom }

public sealed class ProfileOverlaySettings
{
    public bool Enabled { get; set; } = true;
    public ProfileOverlayPosition Position { get; set; } = ProfileOverlayPosition.TopRight;
    public double XPercent { get; set; } = 75;
    public double YPercent { get; set; } = 5;
    public bool IsValid() => Enum.IsDefined(Position) && double.IsFinite(XPercent) && XPercent is >= 0 and <= 100
        && double.IsFinite(YPercent) && YPercent is >= 0 and <= 100;
}

public static class ProfileOverlayLayout
{
    public static (int X, int Y) Place(int areaX, int areaY, int areaWidth, int areaHeight, int windowWidth, int windowHeight, int margin, ProfileOverlaySettings settings)
    {
        if (settings is null || areaWidth <= 0 || areaHeight <= 0 || windowWidth <= 0 || windowHeight <= 0 || margin < 0 || !settings.IsValid())
            throw new ArgumentOutOfRangeException(nameof(settings), "Position de notification invalide.");

        int spaceX = Math.Max(0, areaWidth - windowWidth);
        int spaceY = Math.Max(0, areaHeight - windowHeight);
        int insetX = Math.Min(margin, spaceX / 2);
        int insetY = Math.Min(margin, spaceY / 2);
        double x = settings.Position switch
        {
            ProfileOverlayPosition.TopLeft or ProfileOverlayPosition.BottomLeft => 0,
            ProfileOverlayPosition.TopRight or ProfileOverlayPosition.BottomRight => 1,
            _ => settings.XPercent / 100d
        };
        double y = settings.Position switch
        {
            ProfileOverlayPosition.TopLeft or ProfileOverlayPosition.TopRight => 0,
            ProfileOverlayPosition.BottomLeft or ProfileOverlayPosition.BottomRight => 1,
            _ => settings.YPercent / 100d
        };
        return (areaX + insetX + (int)Math.Round((spaceX - 2 * insetX) * x),
            areaY + insetY + (int)Math.Round((spaceY - 2 * insetY) * y));
    }
}
