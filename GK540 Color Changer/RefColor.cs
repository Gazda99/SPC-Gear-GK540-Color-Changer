using System.Drawing;

namespace GK540_Color_Changer {
public class RefColor {
    public Color Color { get; set; }

    public RefColor() { }

    public RefColor(Color c) {
        Color = c;
    }

    public static implicit operator Color(RefColor refColor) {
        return refColor.Color;
    }

    public static implicit operator RefColor(Color color) {
        return new RefColor(color);
    }
}
}