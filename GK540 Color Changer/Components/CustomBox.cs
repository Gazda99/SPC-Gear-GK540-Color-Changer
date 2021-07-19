using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using GK540_Color_Changer.Languages;

namespace GK540_Color_Changer.Components {
public class CustomBox : Form {
    private const int WM_NCLBUTTONDOWN = 0xA1;
    private const int HTCAPTION = 0x2;

    private readonly bool _isDraggable;

    private readonly Label _label = new Label();
    private Button _buttonOk;
    private Button _buttonCancel;

    private TextBox _textBox;

    public string Result => _textBox is null ? String.Empty : _textBox.Text;

    [DllImport("user32.dll")]
    private static extern bool ReleaseCapture();

    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

    public sealed override Color BackColor {
        get { return base.BackColor; }
        set { base.BackColor = value; }
    }

    public CustomBox(string message, CustomBoxType type, bool isDraggable = false, bool isCentered = true) {
        this._isDraggable = isDraggable;
        this.FormBorderStyle = FormBorderStyle.None;
        this.BackColor = Constants.Black;
        this.Size = new Size(250, 115);
        this.StartPosition = isCentered ? FormStartPosition.CenterScreen : FormStartPosition.Manual;

        this.ShowInTaskbar = false;
        this.Paint += DrawBorder;

        if (_isDraggable)
            this.MouseDown += DraggableForm;


        switch (type) {
            case CustomBoxType.ErrorBox:
                InitError();
                break;
            case CustomBoxType.QuestionBox:
                InitQuestion();
                break;
            case CustomBoxType.InputBox:
                InitInput();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        Init(message);
    }


    private void Init(string message) {
        _label.ForeColor = Constants.WhiteTextColor;
        _label.Location = new Point(
            LayoutHelpers.CenterCoordinate(this.Size.Width, _label.Size.Width), 10);
        _label.Text = message;
        _label.TextAlign = ContentAlignment.MiddleCenter;
        if (_isDraggable) _label.MouseDown += DraggableForm;

        this.Controls.Add(_label);
    }

    private void InitError() {
        _buttonOk = new CustomButton();
        this.TopMost = true;
        this.Size = new Size(this.Size.Width, 170);
        _label.Size = new Size(this.Size.Width - 20, (int) (this.Size.Height * 0.72));
        SetButtonSize(_buttonOk);
        _buttonOk.Text = Locals.GetString("btOK");
        _buttonOk.Location = new Point(
            LayoutHelpers.CenterCoordinate(this.Size.Width, _buttonOk.Size.Width), (int) (this.Size.Height * 0.8));
        this.Controls.Add(_buttonOk);
        _buttonOk.Click += (object _, EventArgs _) => ButtonAcceptClick();
    }


    private void InitQuestion() {
        _label.Size = new Size(this.Size.Width - 20, 60);
        InitAcceptCancel(Locals.GetString("btYes"), Locals.GetString("btNo"));
    }

    private void InitAcceptCancel(string acceptText, string cancelText) {
        _buttonOk = new CustomButton();
        _buttonCancel = new CustomButton();
        const int y = 80;
        _buttonOk.Text = acceptText;
        _buttonOk.Location = new Point(50, y);
        _buttonOk.Click += (object _, EventArgs _) => ButtonAcceptClick();
        SetButtonSize(_buttonOk);
        this.Controls.Add(_buttonOk);

        _buttonCancel.Text = cancelText;
        _buttonCancel.Location = new Point(135, y);
        _buttonCancel.Click += (object _, EventArgs _) => ButtonCancelClick();
        SetButtonSize(_buttonCancel);
        this.Controls.Add(_buttonCancel);
    }

    private void InitInput() {
        _label.Size = new Size(this.Size.Width - 20, 25);
        _textBox = new TextBox {
            Size = new Size(this.Size.Width - 20, 25),
            BackColor = Constants.DarkGrey,
            ForeColor = Constants.WhiteTextColor,
            MaxLength = 22,
            TextAlign = HorizontalAlignment.Center
        };
        _textBox.Location = new Point(
            LayoutHelpers.CenterCoordinate(this.Size.Width, _textBox.Size.Width),
            45);
        _textBox.KeyPress += InputOnEnter;
        _textBox.ContextMenuStrip = new CustomContextMenuStrip(_textBox);
        _textBox.Visible = true;
        this.Controls.Add(_textBox);

        InitAcceptCancel(Locals.GetString("btAccept"), Locals.GetString("btCancel"));
    }

    private static void SetButtonSize(Button bt) {
        bt.Size = new Size(75, 27);
    }


    private void DrawBorder(object sender, PaintEventArgs e) {
        using Pen p = new Pen(Constants.DarkGrey, 8);
        e.Graphics.DrawRectangle(p, this.DisplayRectangle);
    }

    private void DraggableForm(object sender, MouseEventArgs e) {
        if (e.Button != MouseButtons.Left) return;
        ReleaseCapture();
        SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
    }


    private void InputOnEnter(object sender, KeyPressEventArgs e) {
        if (e.KeyChar != (char) System.Windows.Forms.Keys.Enter) return;
        e.Handled = true;
        ButtonAcceptClick();
    }

    private void ButtonAcceptClick() {
        this.DialogResult = DialogResult.OK;
        this.Close();
    }

    private void ButtonCancelClick() {
        this.DialogResult = DialogResult.Cancel;
        this.Close();
    }
}

public enum CustomBoxType {
    ErrorBox,
    QuestionBox,
    InputBox,
}
}