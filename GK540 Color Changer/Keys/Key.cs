using System.Drawing;

namespace GK540_Color_Changer.Keys {
public class Key {
    public readonly string Name;
    public readonly int KeyNumber;

    public Color Color = Color.Black;
    public Rectangle Rectangle;
    
    public Key(int keyNumber, string name, int x, int y, int width, int height) {
        KeyNumber = keyNumber;
        Name = name;
        Rectangle = new Rectangle(x, y, width, height);
    }

    public Key(int keyNumber, string name, Rectangle rect) {
        KeyNumber = keyNumber;
        Name = name;
        Rectangle = rect;
    }
}
}