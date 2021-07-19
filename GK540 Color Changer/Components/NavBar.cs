using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using GK540_Color_Changer.Files;
using GK540_Color_Changer.Languages;

namespace GK540_Color_Changer.Components {
/// <summary>
/// Navigation bar
/// </summary>
public class NavBar : Panel {
    private const int Space = 4;
    private const int NavBarHeight = 35;
    private const int TimerInterval = 2500;

    private PictureBox _exitPictureBox;
    private PictureBox _minimizePictureBox;
    private Label _messageLabel;
    private Timer _messageTimer;
    

    public event MouseEventHandler ExitPictureBoxMouseDown {
        add {
            lock (_exitPictureBox) {
                _exitPictureBox.MouseDown += value;
            }
        }
        remove {
            lock (_exitPictureBox) {
                _exitPictureBox.MouseDown -= value;
            }
        }
    }

    public event MouseEventHandler MinimizePictureBoxMouseDown {
        add {
            lock (_minimizePictureBox) {
                _minimizePictureBox.MouseDown += value;
            }
        }
        remove {
            lock (_minimizePictureBox) {
                _minimizePictureBox.MouseDown -= value;
            }
        }
    }

    public event MouseEventHandler DragWindow {
        add {
            //     lock (this) {
            this.MouseDown += value;
            _messageLabel.MouseDown += value;
            //  }
        }
        remove {
            // lock (this) {
            this.MouseDown -= value;
            _messageLabel.MouseDown -= value;
            // }
        }
    }


    public NavBar(int width,int msgLabelWidth) {
        this.Size = new Size(width, NavBarHeight);
        this.Location = new Point(0, 0);

        InitCustomButtons();
        InitMessageLabel(msgLabelWidth);
        InitTimer();
    }

    
    private void InitMessageLabel(int msgLabelWidth) {
        _messageLabel = new Label {
            Location = new Point(Space, 0),
            Size = new Size(msgLabelWidth, this.Size.Height),
            ForeColor = Constants.WhiteTextColor,
            BackColor = Color.Transparent,
            TextAlign = ContentAlignment.MiddleLeft
        };
        this.Controls.Add(_messageLabel);
    }

    private void InitTimer() {
        _messageTimer = new Timer() {Enabled = false, Interval = TimerInterval};
        _messageTimer.Tick += OnTimerTick;
    }


    private void InitCustomButtons() {
        //exit button
        Image exitImage;
        try {
            exitImage = ImageReader.Read(FilesPath.ExitButtonImage);
        }
        catch (ImageNotLoadedException e) {
            FileNotLoadedError.InvokeImageNotLoaded(e.Message);
            return;
        }

        _exitPictureBox = CreateCommonPictureBox(exitImage);
        _exitPictureBox.Location = new Point(this.Size.Width - _exitPictureBox.Size.Width - Space,
            LayoutHelpers.CenterCoordinate(this.Size.Height, _exitPictureBox.Size.Height));

        CustomToolTip.AddTooltip(_exitPictureBox, Locals.GetString("btExitApp"));

        this.Controls.Add(_exitPictureBox);

        //minimize button
        Image minimizeImage;
        try {
            minimizeImage = ImageReader.Read(FilesPath.MinimizeButtonImage);
        }
        catch (ImageNotLoadedException e) {
            FileNotLoadedError.InvokeImageNotLoaded(e.Message);
            return;
        }

        _minimizePictureBox = CreateCommonPictureBox(minimizeImage);
        _minimizePictureBox.Location = new Point(
            _exitPictureBox.Location.X - _minimizePictureBox.Size.Width - Space,
            LayoutHelpers.CenterCoordinate(this.Size.Height, _minimizePictureBox.Size.Height));

        CustomToolTip.AddTooltip(_minimizePictureBox, Locals.GetString("btMinimize"));

        this.Controls.Add(_minimizePictureBox);

        //common parameters for pictureboxes
        static PictureBox CreateCommonPictureBox(Image img) {
            return new PictureBox {
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = img,
                Size = new Size(25, 25)
            };
        }
    }

    private void OnTimerTick(object sender, EventArgs _) {
        if (sender is not Timer timer) return;
        _messageLabel.Text = "";
        timer.Stop();
    }

    protected override void OnPaintBackground(PaintEventArgs e) {
        LinearGradientBrush brush = new LinearGradientBrush(
            this.ClientRectangle,
            Constants.DarkGrey,
            Constants.Black,
            90F);
        e.Graphics.FillRectangle(brush, this.ClientRectangle);
    }


    public void ShowTextMessage(string message) {
        _messageTimer.Stop();
        _messageLabel.Text = message;
        _messageTimer.Start();
    }
}
}