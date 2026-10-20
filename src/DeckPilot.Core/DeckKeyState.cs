namespace DeckPilot.Core;

// A decision is made once per physical press and kept until release.
public sealed class DeckKeyState
{
    private readonly HashSet<int> down = [];
    private readonly HashSet<int> swallowed = [];
    public bool Process(int key, bool isDown, Func<int, bool> onFirstPress)
    {
        if (!Calibration.IsDeckKey(key)) return false;
        if (isDown)
        {
            if (down.Add(key) && onFirstPress(key)) swallowed.Add(key);
            return swallowed.Contains(key);
        }
        down.Remove(key);
        return swallowed.Remove(key);
    }
}
