using System.Windows.Forms;

namespace GK540_Color_Changer.Components {
public class CustomToolTip : ToolTip {
    private CustomToolTip() {
       this.OwnerDraw = true;
       this.Draw += ToolTipDraw;
    }

    /// <summary>
    /// Creates the tooltip and assigns it to Control passed as parameter
    /// </summary>
    /// <param name="c">Control to which tooltip will be assigned</param>
    /// <param name="tooltipMessage">Message showed in tooltip</param>
    public static void AddTooltip(Control c, string tooltipMessage) {
        CustomToolTip toolTip = new CustomToolTip() {
            AutoPopDelay = 5000,
            InitialDelay = 750,
            ReshowDelay = 500,
            ShowAlways = true,
            BackColor = Constants.DarkGrey,
            ForeColor = Constants.WhiteTextColor
        };
        toolTip.SetToolTip(c, tooltipMessage);
    }


    private static void ToolTipDraw(object sender, DrawToolTipEventArgs e) {
        e.DrawBackground();
        e.DrawBorder();
        e.DrawText();
    }
}
}