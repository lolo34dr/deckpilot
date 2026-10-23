namespace DeckPilot.Core;

public sealed record GestureInvocation(int Index, ButtonAction Action);
public sealed class PressGestures
{
    private sealed class Press(int index, ButtonAction action, long at)
    {
        public int Index = index;
        public ButtonAction Action = action;
        public long At = at;
        public bool LongSent;
    }
    private readonly Dictionary<int, Press> down = [];
    public bool HasPending => down.Values.Any(p => !p.LongSent);
    public GestureInvocation? Begin(int key, int index, ButtonAction action, long at)
    {
        if (down.ContainsKey(key)) return null;
        if (!action.LongPressEnabled) return action.Kind == ActionKind.Passthrough ? null : new(index, action.CopyBasic());
        if (!action.IsValid()) throw new InvalidDataException("Configuration d’appui long invalide.");
        down.Add(key, new(index, action.Copy(), at)); return null;
    }
    public GestureInvocation? Release(int key, long at)
    {
        if (!down.Remove(key, out var press) || press.LongSent) return null;
        return new(press.Index, (at - press.At >= press.Action.LongPressMilliseconds ? press.Action.LongPressAction! : press.Action).CopyBasic());
    }
    public IReadOnlyList<GestureInvocation> Tick(long at)
    {
        var invocations = new List<GestureInvocation>();
        foreach (var p in down.Values)
            if (!p.LongSent && at - p.At >= p.Action.LongPressMilliseconds)
            { p.LongSent = true; invocations.Add(new(p.Index, p.Action.LongPressAction!.CopyBasic())); }
        return invocations;
    }
    public void CancelIndex(int index)
    {
        foreach (int key in down.Where(p => p.Value.Index == index).Select(p => p.Key).ToArray()) down.Remove(key);
    }
    public void CancelAll() => down.Clear();
}
