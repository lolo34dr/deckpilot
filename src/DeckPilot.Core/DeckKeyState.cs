namespace DeckPilot.Core;

// A decision is made once per physical press and kept until release.
public sealed class DeckKeyState
{
    private readonly HashSet<int> down = [];
    private readonly HashSet<int> swallowed = [];
    public bool Process(int key, bool isDown, Func<int, bool> onFirstPress, Action<int>? onRelease = null)
    {
        if (!Calibration.IsDeckKey(key)) return false;
        if (isDown)
        {
            if (down.Add(key) && onFirstPress(key)) swallowed.Add(key);
            return swallowed.Contains(key);
        }
        if (!down.Remove(key)) return false;
        bool suppress = swallowed.Remove(key);
        onRelease?.Invoke(key);
        return suppress;
    }
}
