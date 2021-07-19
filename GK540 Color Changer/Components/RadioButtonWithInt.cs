using System;
using System.Drawing;
using System.Windows.Forms;

namespace GK540_Color_Changer.Components {
/// <summary>
/// Radio button with additional field for int
/// </summary>
public class RadioButtonWithInt : RadioButton {
    public int Value { get; set; }
    public Color HighlightColor { get; set; }
    private bool _drawBorder;

    public RadioButtonWithInt() {
        this.Paint += Draw;
        this.MouseEnter += HoverOn;
        this.MouseLeave += HoverOff;
    }


    private void HoverOn(object sender, EventArgs e) {
        _drawBorder = true;
        this.Refresh();
    }

    private void HoverOff(object sender, EventArgs e) {
        _drawBorder = false;
        this.Refresh();
    }

    /// Draws dashed border
    private void Draw(object sender, PaintEventArgs e) {
        if (!_drawBorder) return;
        using Pen p = new Pen(HighlightColor, 2) {DashPattern = new float[] {1.0f, 2.0f, 3.0f, 2.0f}};
        e.Graphics.DrawRectangle(p, this.DisplayRectangle);
    }
}
}