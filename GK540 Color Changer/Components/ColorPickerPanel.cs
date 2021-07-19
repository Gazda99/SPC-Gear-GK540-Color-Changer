using System.Drawing;
using System.Windows.Forms;

namespace GK540_Color_Changer.Components {
/// <summary>
/// Used for picking the common color
/// </summary>
public class ColorPickerPanel : Panel {
    public RefColor HighlightColor { get; set; } = Color.Black;
    public int BorderWidth { get; set; } = 1;

    public ColorPickerPanel() { }

    public ColorPickerPanel(RefColor highlightColor) {
        HighlightColor = highlightColor;
    }

    public ColorPickerPanel(RefColor highlightColor, int width) : this(highlightColor) {
        BorderWidth = width;
    }


    protected override void OnPaint(PaintEventArgs e) {
        using SolidBrush brush = new SolidBrush(this.BackColor);
        e.Graphics.FillRectangle(brush, this.ClientRectangle);

        if (HighlightColor is null) return;

        using Pen p = new Pen(HighlightColor.Color, BorderWidth);
        e.Graphics.DrawRectangle(p, 0, 0, this.ClientSize.Width - 1, this.ClientSize.Height - 1);
    }
}
}