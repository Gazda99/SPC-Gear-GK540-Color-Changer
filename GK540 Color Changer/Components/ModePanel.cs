using System;
using System.Drawing;
using System.Windows.Forms;
using GK540_Color_Changer.Files;
using GK540_Color_Changer.Languages;

namespace GK540_Color_Changer.Components {
/// <summary>
/// Panel with radio buttons for chosing the mode
/// </summary>
public class ModePanel : Panel {
    private readonly RadioButton[] _radioButtons;
    public event EventHandler ModeChanged;
    public event EventHandler<NameChangedEventArgs> NameChanged;

    private int _selectedMode;

    public event MouseEventHandler MouseDownRadioButtons {
        add {
            for (int i = 0; i < _radioButtons.Length; i++) {
                lock (_radioButtons[i]) {
                    _radioButtons[i].MouseDown += value;
                }
            }
        }
        remove {
            for (int i = 0; i < _radioButtons.Length; i++) {
                lock (_radioButtons[i]) {
                    _radioButtons[i].MouseDown -= value;
                }
            }
        }
    }

    public int SelectedMode {
        get { return _selectedMode; }
        set {
            _selectedMode = value;
            _radioButtons[_selectedMode].Checked = true;
            ModeName = _radioButtons[_selectedMode].Text;
            OnModeChanged();
        }
    }

    public string ModeName;


    private void SetModeName(int n, string newName) {
        string oldName = _radioButtons[n].Text;
        _radioButtons[n].Text = newName;
        ModeName = newName;
        OnNameChanged(n, oldName, newName);
    }

    private readonly int _radioButtonWidth;
    private readonly int _radioButtonHeight;


    public ModePanel(string[] names, int selectedMode, int radioButtonWidth, int radioButtonHeight) {
        _selectedMode = selectedMode;
        _radioButtonWidth = radioButtonWidth;
        _radioButtonHeight = radioButtonHeight;
        _radioButtons = new RadioButton[ConfigConstants.ModeCount];
        CreateRadio();
        UpdateNames(names);
        ModeName = _radioButtons[_selectedMode].Text;
    }

    public void UpdateNames(string[] newNames) {
        if (newNames.Length != _radioButtons.Length) return;

        for (int i = 0; i < _radioButtons.Length; i++)
            _radioButtons[i].Text = newNames[i];
    }

    private void OnModeChanged() {
        ModeChanged?.Invoke(this, EventArgs.Empty);
    }

    private void OnNameChanged(int n, string oldName, string newName) {
        NameChangedEventArgs eventArgs = new NameChangedEventArgs()
            {N = n, OldName = oldName, NewName = newName, Successful = true};
        NameChanged?.Invoke(this, eventArgs);
    }

    private void CannotChangeName(string newName) {
        NameChangedEventArgs eventArgs = new NameChangedEventArgs() {NewName = newName, Successful = false};
        NameChanged?.Invoke(this, eventArgs);
    }


    private void CreateRadio() {
        int modeNumber = 0;
        int x = 0;
        int y = 5;

        Iterate();

        _radioButtons[_selectedMode].Checked = true;

        void Iterate() {
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 4; j++) {
                    if (modeNumber >= ConfigConstants.ModeCount)
                        return;

                    RadioButtonWithInt rb = new RadioButtonWithInt {
                        ForeColor = Constants.WhiteTextColor,
                        Location = new Point(x, y),
                        Size = new Size(_radioButtonWidth, _radioButtonHeight),
                        Padding = new Padding(2, 0, 0, 0),
                        FlatStyle = FlatStyle.Flat,
                        Value = modeNumber,
                        HighlightColor = Constants.DarkGrey
                    };

                    rb.MouseDown += RadioButtonOnClick;
                    _radioButtons[modeNumber] = rb;
                    CustomToolTip.AddTooltip(rb, Locals.GetString("tltRadioButton"));
                    this.Controls.Add(rb);

                    modeNumber++;
                    y += _radioButtonHeight + 10;
                }

                y = 5;
                x += _radioButtonWidth + 10;
            }
        }
    }

    private void RadioButtonOnClick(object sender, MouseEventArgs e) {
        if (sender is not RadioButtonWithInt rb) return;
        switch (e.Button) {
            case MouseButtons.Left:
                this.SelectedMode = rb.Value;
                break;
            case MouseButtons.Right:
                RightClick();
                break;
            default:
                return;
        }

        void RightClick() {
            CustomBox inputBox = new CustomBox(Locals.GetString("changeModeName"), CustomBoxType.InputBox,
                true, false) {Location = new Point(Cursor.Position.X, Cursor.Position.Y - 50)};

            if (inputBox.ShowDialog() != DialogResult.OK) return;
            
            if (String.IsNullOrEmpty(inputBox.Result))
                CannotChangeName(inputBox.Result);
            else
                SetModeName(rb.Value, inputBox.Result);
        }
    }
}
}