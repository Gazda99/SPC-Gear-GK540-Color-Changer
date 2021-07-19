using System;

namespace GK540_Color_Changer {
public class NameChangedEventArgs : EventArgs {
    public int N { get; init; }
    public string OldName { get; init; }
    public string NewName { get; init; }

    public bool Successful { get; init; }
}
}