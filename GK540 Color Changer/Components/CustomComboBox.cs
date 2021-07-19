using System.Drawing;
using System.Windows.Forms;

namespace GK540_Color_Changer.Components {
/// <summary>
/// Custom combobox with ability to change color to fit the app theme
/// </summary>
public class CustomComboBox : ComboBox {
    private const int WmPaint = 0xF;
    public Color BorderColor { get; set; } = Color.Black;
    public Color HighlightColor { get; set; } = Color.Blue;
    public StringAlignment TextAlign { get; set; } = StringAlignment.Center;
    public int TextYOffset { get; set; } = 0;

    public CustomComboBox() {
        this.FlatStyle = FlatStyle.Flat;
        this.DrawMode = DrawMode.OwnerDrawVariable;
        this.DropDownStyle = ComboBoxStyle.DropDownList;
        this.DrawItem += CustomComboBoxDrawItem;
        this.MeasureItem += CustomComboBoxMeasureItem;
    }

    protected override CreateParams CreateParams {
        get {
            CreateParams cp = base.CreateParams;
            cp.Style &= ~0x02000000; // Turn off WS_CLIPCHILDREN
            return cp;
        }
    }

    protected override void WndProc(ref Message m) {
        base.WndProc(ref m);
        if (m.Msg != WmPaint) return;
        using Graphics g = Graphics.FromHwnd(Handle);
        using Pen p = new Pen(BorderColor, 3);
        g.DrawRectangle(p, 0, 0, Width - 1, Height - 1);
    }

    private void CustomComboBoxDrawItem(object sender, DrawItemEventArgs e) {
        e.DrawBackground();
        if (e.Index < 0) return;

        using StringFormat sf = new StringFormat {
            LineAlignment = TextAlign,
            Alignment = TextAlign
        };

        using Brush brush = new SolidBrush(this.ForeColor);
        if ((e.State & DrawItemState.Selected) == DrawItemState.Selected) {
            using SolidBrush sBrush = new SolidBrush(HighlightColor);
            e.Graphics.FillRectangle(sBrush, e.Bounds);
        }


        e.Graphics.DrawString(this.Items[e.Index].ToString(), this.Font, brush,
            new RectangleF(e.Bounds.X, e.Bounds.Y + TextYOffset, e.Bounds.Width, e.Bounds.Height), sf);
    }

    private static void CustomComboBoxMeasureItem(object sender, MeasureItemEventArgs e) { }
}
}