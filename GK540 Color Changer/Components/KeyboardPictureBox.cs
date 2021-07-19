using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GK540_Color_Changer.Files;
using GK540_Color_Changer.Keys;
using GK540_Color_Changer.Languages;

namespace GK540_Color_Changer.Components {
public class KeyboardPictureBox : PictureBox {
    private readonly string _keyboardImagePath = FilesPath.GK540KeyboardImage;

    public event EventHandler<ColorChangedEventArgs> ColorChanged;
    public event EventHandler<ColorChangedEventArgs> GotColor;


    public Dictionary<int, Key> KeyDict { get; init; }

    private Bitmap _bitmap;

    private OnClickBehaviour _currentOnClickBehaviour;

    private readonly CustomColorDialog _colorDialog;
    private readonly RefColor _commonColor;

    public KeyboardPictureBox(CustomColorDialog colorDialog, RefColor commonColor,
        Color[] colorScheme, OnClickBehaviour behaviour = OnClickBehaviour.EachKeySeparated) {
        InitImage();
        _colorDialog = colorDialog;
        _commonColor = commonColor;
        _currentOnClickBehaviour = behaviour;

        KeyData kf = new KeyData();
        KeyDict = kf.GetGK540KeyDataDict();

        this.MouseDown += OnClickChangeColor;
        this.MouseDown += GetColor;
        if (_currentOnClickBehaviour == OnClickBehaviour.OneColorForAll)
            this.MouseMove += OnClickChangeColor;

        DrawKeys(colorScheme);
    }

    ~KeyboardPictureBox() {
        _bitmap.Dispose();
    }

    private void InitImage() {
        try {
            using Image baseImage = ImageReader.Read(_keyboardImagePath);
            this.Size = new Size(baseImage.Width, baseImage.Height);
            _bitmap = new Bitmap(baseImage);
        }
        catch (ImageNotLoadedException e) {
            FileNotLoadedError.InvokeImageNotLoaded(e.Message);
        }
    }

    private void GetColor(object sender, MouseEventArgs e) {
        if (e.Button != MouseButtons.Right) return;

        foreach ((int _, Key k) in KeyDict) {
            if (!k.Rectangle.Contains(e.Location)) continue;
            ColorChangedEventArgs eventArgs = new ColorChangedEventArgs() {NewColor = k.Color};
            GotColor?.Invoke(this, eventArgs);
            return;
        }
    }

    private void OnClickChangeColor(object sender, MouseEventArgs e) {
        if (e.Button != MouseButtons.Left) return;

        foreach ((int _, Key k) in KeyDict) {
            if (!k.Rectangle.Contains(e.Location)) continue;
            Color oldColor = k.Color;
            RefColor mc;
            switch (_currentOnClickBehaviour) {
                case OnClickBehaviour.EachKeySeparated:
                    mc = OnClickEachKeySeparated(k);
                    break;
                case OnClickBehaviour.OneColorForAll:
                    mc = OnClickOneColorForAll(k);
                    break;
                default:
                    return;
            }

            if (mc is not null)
                OnColorChanged(k.KeyNumber, oldColor, mc.Color);

            return;
        }
    }

    private void OnColorChanged(int keyNumber, Color oldColor, Color newColor) {
        ColorChangedEventArgs eventArgs = new ColorChangedEventArgs()
            {KeyNumber = keyNumber, OldColor = oldColor, NewColor = newColor};
        ColorChanged?.Invoke(this, eventArgs);
    }
    
    private RefColor OnClickEachKeySeparated(Key k) {
        string dialogTitle = $"{Locals.GetString("selectedKey")} {k.Name}";

        if (_colorDialog.ShowDialog(dialogTitle, Cursor.Position) != DialogResult.OK) return null;

        if (ColorHelper.IsSameColor(_colorDialog.Color, k.Color)) return null;

        k.Color = _colorDialog.Color;
        DrawKey(k);
        return k.Color;
    }

    private RefColor OnClickOneColorForAll(Key k) {
        if (ColorHelper.IsSameColor(_commonColor.Color, k.Color)) return null;
        k.Color = _commonColor.Color;
        DrawKey(k);
        return _commonColor;
    }


    public void DrawKeys(Color[] colors) {
        foreach ((int _, Key k) in KeyDict)
            k.Color = colors[k.KeyNumber];
        DrawKeys(KeyDict);
    }

    public void ChangeBackgroundColorAndDraw(Color c) {
        foreach ((int _, Key k) in KeyDict)
            k.Color = c;
        DrawKeys(KeyDict);
    }

    private void DrawKeys(Dictionary<int, Key> keyDict) {
        using Graphics g = Graphics.FromImage(_bitmap);
        using Pen p = new Pen(keyDict.First().Value.Color, 2);

        foreach ((_, Key k) in keyDict) {
            p.Color = k.Color;
            g.DrawRectangle(p, k.Rectangle);
        }

        this.Image = _bitmap;
    }


    private void DrawKey(Key k) {
        using Graphics g = Graphics.FromImage(_bitmap);
        using Pen p = new Pen(k.Color, 2);
        g.DrawRectangle(p, k.Rectangle);
        this.Image = _bitmap;
    }

    public void DrawKey(int keyNumber, Color c) {
        Key k = KeyDict[keyNumber];
        k.Color = c;
        DrawKey(k);
    }

    public void ChangeOnClickAction(OnClickBehaviour behaviour) {
        _currentOnClickBehaviour = behaviour;
        switch (_currentOnClickBehaviour) {
            case OnClickBehaviour.EachKeySeparated:
                this.MouseMove -= OnClickChangeColor;
                break;
            case OnClickBehaviour.OneColorForAll:
                this.MouseMove += OnClickChangeColor;
                break;
        }
    }

    public enum OnClickBehaviour {
        EachKeySeparated,
        OneColorForAll
    }
}
}