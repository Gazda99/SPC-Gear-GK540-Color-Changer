using System.Drawing;

namespace GK540_Color_Changer {
public class HistoryEvent {
    public readonly int KeyNumber;
    public readonly Color OldColor;
    public readonly HistoryEvent[] BackgroundChange;
    public readonly bool IsSingle;


    public HistoryEvent(HistoryEvent[] backgroundChanges) {
        IsSingle = false;
        BackgroundChange = backgroundChanges;
    }

    public HistoryEvent(int keyNumber, Color oldColor) {
        KeyNumber = keyNumber;
        OldColor = oldColor;
        IsSingle = true;
    }
}
}