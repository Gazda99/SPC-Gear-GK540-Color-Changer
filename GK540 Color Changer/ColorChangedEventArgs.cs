using System;
using System.Drawing;

namespace GK540_Color_Changer {
public class ColorChangedEventArgs : EventArgs {
    public int KeyNumber { get; init; }
    public Color OldColor { get; init; }
    public Color NewColor { get; init; }
}
}