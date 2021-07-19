using System.Windows.Forms;

namespace GK540_Color_Changer.Components {
public sealed class CustomButton : Button {
    private const int BorderSize = 2;

    public CustomButton() {
        this.BackColor = Constants.Black;
        this.ForeColor = Constants.WhiteTextColor;
        this.FlatStyle = FlatStyle.Flat;
        FlatButtonAppearance flatAppearance = this.FlatAppearance;
        flatAppearance.BorderSize = BorderSize;
        flatAppearance.BorderColor = Constants.DarkGrey;
        flatAppearance.MouseOverBackColor = Constants.DarkGrey;
    }
}
}