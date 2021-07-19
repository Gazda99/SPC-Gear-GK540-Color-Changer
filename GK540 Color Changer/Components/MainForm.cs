using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using GK540_Color_Changer.Files;
using GK540_Color_Changer.Keys;
using GK540_Color_Changer.Languages;

namespace GK540_Color_Changer.Components {
/// <summary>
/// Main form of application
/// </summary>
public class MainForm : Form {
    private const int WM_NCLBUTTONDOWN = 0xA1;
    private const int HTCAPTION = 0x2;
    private const int FormWidth = 1200;
    private const int FormHeight = 600;

    private readonly NotifyIcon _notifyIcon = new NotifyIcon();
    private readonly KeyboardConfigEditor _keyboardConfigEditor = new KeyboardConfigEditor();
    private readonly ApplicationConfigEditor _appConfigEditor = new ApplicationConfigEditor();
    private readonly CustomColorDialog _colorDialog = new CustomColorDialog();

    private NavBar _navBar;
    private KeyboardPictureBox _keyboardPictureBox;
    private ModePanel _modePanel;
    private ColorPickerPanel _commonColorPicker;


    private readonly History _history = new History();
    private RefColor _commonColor;

    
    [DllImport("User32.dll")]
    private static extern bool ReleaseCapture();

    [DllImport("User32.dll")]
    private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

    public MainForm() {
        ReadConfigs();
        InitForm();
        Icon icon;
        try {
            icon = ImageReader.ReadIcon(FilesPath.IconImage);
        }
        catch (ImageNotLoadedException e) {
            FileNotLoadedError.InvokeImageNotLoaded(e.Message);
            this.Load += (object _, EventArgs _) => { this.Close(); };
            return;
        }


        this.Icon = icon;

        InitNotifyIcon(icon);
        InitNavBar();
        InitColorDialog();
        InitPictureBox();
        InitOtherControls();
        InitHistory();
    }

    protected override CreateParams CreateParams {
        get {
            CreateParams cp = base.CreateParams;
            cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED
            return cp;
        }
    }

    private void ReadConfigs() {
        _appConfigEditor.Read();

        //reads gk540 config, if files does not exists create new ones using default scheme in app directory, if those
        //files too does not exists, exits app
        bool check = _keyboardConfigEditor.ReadAll();

        if (!check) {
            string errorMessage = $"{Locals.GetString("cannotLoadConfig")}";
            CustomBox errorBox = new CustomBox(errorMessage, CustomBoxType.ErrorBox, false, true);
            errorBox.ShowDialog();
            bool check2 = _keyboardConfigEditor.DefaultConfig();
            if (!check2) {
                FileNotLoadedError.InvokeConfigNotLoaded(FilesPath.DefaultConfigPath);
                return;
            }

            check = _keyboardConfigEditor.ReadAll();
            if (!check) {
                FileNotLoadedError.InvokeConfigNotLoaded(FilesPath.ConfigPath);
                return;
            }
        }


        _keyboardConfigEditor.CurrentProfile = _appConfigEditor.SelectedProfile;
        _keyboardConfigEditor.CurrentMode = _appConfigEditor.SelectedMode;
        _commonColor = _appConfigEditor.CommonColor;

        _colorDialog.CustomColors = _appConfigEditor.GetCustomColors().Select(ColorTranslator.ToOle).ToArray();
    }

    #region Init

    private void InitNotifyIcon(Icon icon) {
        _notifyIcon.Icon = icon;
        _notifyIcon.Text = Locals.GetString("notifyIcon");
        _notifyIcon.MouseDown += NormalWindow;
        _notifyIcon.Visible = false;
    }

    private void InitForm() {
        this.FormBorderStyle = FormBorderStyle.None;
        this.Size = new Size(FormWidth, FormHeight);
        this.BackColor = Constants.Black;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.ShowInTaskbar = true;
        this.Text = Locals.GetString("mainFormTitle");
    }

    private void InitNavBar() {
        _navBar = new NavBar(this.Size.Width, 500);
        _navBar.DragWindow += DraggableForm;
        _navBar.ExitPictureBoxMouseDown += CloseWindow;
        _navBar.MinimizePictureBoxMouseDown += MinimizeWindow;
        this.Controls.Add(_navBar);
    }

    private void InitColorDialog() {
        _colorDialog.CustomColorsChanged += (object _, EventArgs _) => UpdateColorDialogCustomColors();
    }


    private void InitPictureBox() {
        _keyboardPictureBox = new KeyboardPictureBox(
            _colorDialog,
            _commonColor,
            _keyboardConfigEditor.GetColorScheme(),
            (KeyboardPictureBox.OnClickBehaviour) _appConfigEditor.SelectedColorMode);

        _keyboardPictureBox.Location = new Point(
            LayoutHelpers.CenterCoordinate(this.Size.Width, _keyboardPictureBox.Size.Width),
            LayoutHelpers.GetYOffset(_navBar, 10)
        );
        _keyboardPictureBox.ColorChanged += KeyboardPictureBoxOnColorChanged;

        _keyboardPictureBox.GotColor += (object sender, ColorChangedEventArgs e) => {
            if (sender is not KeyboardPictureBox) return;
            SetCommonColor(e.NewColor);
        };

        this.Controls.Add(_keyboardPictureBox);
    }

    private void InitOtherControls() {
        const int horizontalSpace = 10;
        const int verticalSpace = 20;
        int height = 25;

        //init combobox with painting modes
        CustomComboBox cbxMode = new CustomComboBox {
            ForeColor = Constants.WhiteTextColor,
            BackColor = Constants.Black,
            BorderColor = Constants.DarkGrey,
            HighlightColor = Constants.DarkGrey,
            ItemHeight = 25,
            Size = new Size(175, height),
            Location = new Point(20, RowY(1)),
        };

        cbxMode.Items.AddRange(LocalsArrays.GetStrings("paintingModes"));
        cbxMode.SelectedIndex = _appConfigEditor.SelectedColorMode;
        CustomToolTip.AddTooltip(cbxMode, Locals.GetString("tltPaintingMode"));
        cbxMode.SelectedIndexChanged += CbxModeOnSelectedIndexChanged;
        this.Controls.Add(cbxMode);

        height = cbxMode.Height;

        //init color picker label
        Label colorPickerLabel = new Label {
            TextAlign = ContentAlignment.MiddleRight,
            Size = new Size(135, height),
            Text = Locals.GetString("lbColorPicker"),
            ForeColor = Constants.WhiteTextColor,
            Location = new Point(SetXLocation(cbxMode), RowY(1))
        };
        this.Controls.Add(colorPickerLabel);

        //init color picker
        _commonColorPicker = new ColorPickerPanel(Constants.DarkGrey, 3) {
            Size = new Size(height, height),
            Location = new Point(SetXLocation(colorPickerLabel), RowY(1)),
            BackColor = _commonColor.Color
        };
        CustomToolTip.AddTooltip(_commonColorPicker, Locals.GetString("tltChangeColor"));
        _commonColorPicker.MouseDown += ClickCommonColorPicker;
        this.Controls.Add(_commonColorPicker);

        //init bg color changer button
        Button btChangeBg = new CustomButton() {
            Text = Locals.GetString("btBackgroundChange"),
            Location = new Point(SetXLocation(cbxMode), RowY(2)),
            Size = new Size(175, height),
        };
        btChangeBg.Click += ButtonChangeBgOnClick;
        this.Controls.Add(btChangeBg);


        //init combobox with profiles
        CustomComboBox cbxProfiles = new CustomComboBox {
            ForeColor = Constants.WhiteTextColor,
            BackColor = Constants.Black,
            BorderColor = Constants.DarkGrey,
            HighlightColor = Constants.DarkGrey,
            ItemHeight = 25,
            Size = new Size(175, height),
            Location = new Point(SetXLocation(_commonColorPicker), RowY(1)),
        };

        CustomToolTip.AddTooltip(cbxProfiles, Locals.GetString("tltChooseProfile"));
        cbxProfiles.Items.AddRange(LocalsArrays.GetStrings("profiles"));
        cbxProfiles.SelectedIndex = _appConfigEditor.SelectedProfile;
        cbxProfiles.SelectedIndexChanged += CbxProfilesOnSelectedIndexChanged;
        this.Controls.Add(cbxProfiles);

        //init mode panel
        _modePanel = new ModePanel(_keyboardConfigEditor.GetModeNames(), _appConfigEditor.SelectedMode, 190, 25) {
            Size = new Size(this.Size.Width - cbxProfiles.Location.X - cbxProfiles.Width - horizontalSpace - 20,
                this.Size.Height - RowY(1) - 10),
            Location = new Point(SetXLocation(cbxProfiles), RowY(1)),
        };

        _modePanel.ModeChanged += (object sender, EventArgs _) => {
            if (sender is not ModePanel mp) return;
            ChangeCurrentMode(mp.SelectedMode);
        };
        _modePanel.NameChanged += ModePanelOnNameChanged;
        this.Controls.Add(_modePanel);

        //init button save colors
        Button btSave = new CustomButton {
            Size = new Size(175, height),
            Location = new Point(SetXLocation(_commonColorPicker), RowY(2)),
            Text = Locals.GetString("btSave")
        };
        btSave.Click += ButtonSaveOnClick;
        this.Controls.Add(btSave);

        //init button reset colors
        Button btReset = new CustomButton {
            Size = new Size(175, height),
            Location = new Point(SetXLocation(_commonColorPicker), RowY(3)),
            Text = Locals.GetString("btReset")
        };
        btReset.Click += ButtonResetOnClick;
        this.Controls.Add(btReset);

        int RowY(int rowNumber) {
            if (rowNumber == 1) return LayoutHelpers.GetYOffset(_keyboardPictureBox, 10);
            return LayoutHelpers.GetYOffset(_keyboardPictureBox, 10) + ((rowNumber - 1) * (height + verticalSpace));
        }

        int SetXLocation(Control precedingControl) {
            return precedingControl.Location.X + precedingControl.Size.Width + horizontalSpace;
        }
    }

    /// <summary>
    /// This should be called when all other controls are already added to Form. 
    /// </summary>
    private void InitHistory() {
        this.MouseDown += MouseDownGoBackHistory;
        foreach (Control c in this.Controls)
            c.MouseDown += MouseDownGoBackHistory;
        _modePanel.MouseDownRadioButtons += MouseDownGoBackHistory;
    }

    #endregion

    #region EventHandlers

    private void KeyboardPictureBoxOnColorChanged(object sender, ColorChangedEventArgs e) {
        _keyboardConfigEditor.UpdateKey(e.KeyNumber, e.NewColor);
        _history.Add(e.KeyNumber, e.OldColor);
    }


    private void CbxModeOnSelectedIndexChanged(object sender, EventArgs e) {
        if (sender is not CustomComboBox cbx) return;
        this._keyboardPictureBox.ChangeOnClickAction((KeyboardPictureBox.OnClickBehaviour) cbx.SelectedIndex);
        _appConfigEditor.SelectedColorMode = cbx.SelectedIndex;
    }

    private void CbxProfilesOnSelectedIndexChanged(object sender, EventArgs e) {
        if (sender is not CustomComboBox cbx) return;
        ChangeCurrentProfile(cbx.SelectedIndex);
        _appConfigEditor.SelectedProfile = cbx.SelectedIndex;
    }

    private void ModePanelOnNameChanged(object sender, NameChangedEventArgs e) {
        if (e.Successful) {
            _keyboardConfigEditor.ChangeModeName(e.N, e.NewName);

            string msg =
                $"{Locals.GetString("changedNameOf")} \"{e.OldName}\" {Locals.GetString("swTo")} \"{e.NewName}\"";
            _navBar.ShowTextMessage(msg);
        }
        else {
            string errorMsg = $"{Locals.GetString("cannotChangeName")} \"{e.NewName}\"";
            CustomBox error = new CustomBox(errorMsg, CustomBoxType.ErrorBox, false, true);
            error.ShowDialog();
        }
    }

    private void ButtonSaveOnClick(object sender, EventArgs e) {
        if (sender is not Button b) return;
        b.Enabled = false;
        SaveKeys();
        _history.Clear();
        b.Enabled = true;
    }

    private void MouseDownGoBackHistory(object sender, MouseEventArgs e) {
        if (e.Button != MouseButtons.XButton1) return;

        HistoryEvent historyEvent = _history.GetLastEvent();
        if (historyEvent is null) return;

        if (historyEvent.IsSingle) {
            _keyboardPictureBox.DrawKey(historyEvent.KeyNumber, historyEvent.OldColor);
            _keyboardConfigEditor.UpdateKey(historyEvent.KeyNumber, historyEvent.OldColor);
        }
        else {
            foreach (HistoryEvent he in historyEvent.BackgroundChange) {
                _keyboardPictureBox.DrawKey(he.KeyNumber, he.OldColor);
                _keyboardConfigEditor.UpdateKey(he.KeyNumber, he.OldColor);
            }
        }
    }

    private void ButtonResetOnClick(object sender, EventArgs e) {
        if (_keyboardConfigEditor.Reset())
            DrawKeys();
        _history.Clear();
    }

    private void ClickCommonColorPicker(object sender, MouseEventArgs e) {
        switch (e.Button) {
            case MouseButtons.Left: {
                _colorDialog.Color = _commonColor.Color;

                if (_colorDialog.ShowDialog(Locals.GetString("colorPickerDialog"), Cursor.Position) != DialogResult.OK)
                    return;

                _appConfigEditor.SetCustomColors(_colorDialog.CustomColors.Select(ColorTranslator.FromOle).ToArray());
                SetCommonColor(_colorDialog.Color);
                break;
            }
            case MouseButtons.Right: {
                string msg =
                    $"{Locals.GetString("currentColor")} {ColorHelper.ColorToStringWithLabel(_commonColor.Color)}";
                _navBar.ShowTextMessage(msg);
                break;
            }
            default:
                return;
        }
    }


    private void ButtonChangeBgOnClick(object sender, EventArgs e) {
        HistoryEvent[] bgChanges = new HistoryEvent[_keyboardPictureBox.KeyDict.Count];

        int i = 0;
        foreach ((int keyNumber, Key k) in _keyboardPictureBox.KeyDict) {
            bgChanges[i] = new HistoryEvent(keyNumber, k.Color);
            i++;
        }

        _keyboardPictureBox.ChangeBackgroundColorAndDraw(_commonColor.Color);
        _keyboardConfigEditor.UpdateKeys(_keyboardPictureBox.KeyDict);

        _history.Add(new HistoryEvent(bgChanges));
    }

    private void DraggableForm(object sender, MouseEventArgs e) {
        if (e.Button != MouseButtons.Left) return;
        ReleaseCapture();
        _ = SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
    }

    private void MinimizeWindow(object sender, MouseEventArgs e) {
        if (e.Button != MouseButtons.Left) return;

        this.Visible = false;
        _notifyIcon.Visible = true;
    }

    private void NormalWindow(object sender, MouseEventArgs e) {
        if (e.Button != MouseButtons.Left) return;

        this.Visible = true;
        _notifyIcon.Visible = false;
    }

    private void CloseWindow(object sender, MouseEventArgs e) {
        if (e.Button != MouseButtons.Left) return;

        CustomBox questionBox = new CustomBox(
            Locals.GetString("saveBeforeExit"), CustomBoxType.QuestionBox, false, false) {
            StartPosition = FormStartPosition.CenterParent
        };

        if (questionBox.ShowDialog() == DialogResult.OK)
            SaveKeys();

        _notifyIcon.Dispose();
        this.Close();
        Application.Exit();
    }

    #endregion

    private void DrawKeys() {
        _keyboardPictureBox.DrawKeys(_keyboardConfigEditor.GetColorScheme());
    }

    private void SaveKeys() {
        Cursor.Current = Cursors.WaitCursor;
        _keyboardConfigEditor.SaveKeys();

        string msg =
            $"{Locals.GetString("savedModeMessage_1")} {_keyboardConfigEditor.CurrentMode} \"{_modePanel.ModeName}\" {Locals.GetString("savedModeMessage_2")} {_keyboardConfigEditor.CurrentProfile}";
        _navBar.ShowTextMessage(msg);
        Cursor.Current = default;
    }

    private void UpdateColorDialogCustomColors() {
        _appConfigEditor.SetCustomColorsOle(_colorDialog.CustomColors);
    }

    private void SetCommonColor(Color c) {
        if (ColorHelper.IsSameColor(c, _commonColor.Color)) return;
        _commonColor.Color = c;
        _appConfigEditor.CommonColor = c;
        _commonColorPicker.BackColor = c;

        string message = $"{Locals.GetString("changedColorMessage")} {ColorHelper.ColorToStringWithLabel(c)}";
        _navBar.ShowTextMessage(message);
    }

    private void ChangeCurrentProfile(int n) {
        if (n < 0) return;
        _keyboardConfigEditor.CurrentProfile = n;
        _modePanel.SelectedMode = _keyboardConfigEditor.CurrentMode;
        _modePanel.UpdateNames(_keyboardConfigEditor.GetModeNames());
        DrawKeys();
        _history.Clear();
    }

    private void ChangeCurrentMode(int n) {
        if (n < 0) return;
        _keyboardConfigEditor.CurrentMode = n;
        _appConfigEditor.SelectedMode = n;

        DrawKeys();
        _history.Clear();
    }
}
}