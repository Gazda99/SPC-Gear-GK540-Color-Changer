using System;
using System.Drawing;
using System.Threading.Tasks;

namespace GK540_Color_Changer.Files {
public class Mode {
    private static string ModeSection(int n) => $"Mode{n}";


    private readonly string _modeSection;
    private readonly Ini _ini;

    /// <summary>
    /// Selected Tool
    /// </summary>
    public int Tool { get; set; }

    /// <summary>
    /// Base color
    /// </summary>
    public Color JColor { get; set; }

    /// <summary>
    /// Selected Color
    /// </summary>
    public Color Color { get; set; }


    private Color[] _colorsTmp;
    public Color[] ColorsData { get; set; }


    private bool _hasChanged;

    public Mode(int modeNumber, Ini ini) {
        _ini = ini;

        ColorsData = new Color[Constants.Gk540ConfigKeysCount];
        _colorsTmp = new Color[Constants.Gk540ConfigKeysCount];

        _modeSection = ModeSection(modeNumber);

        ReadConfig();
    }
    
    private void ReadConfig() {
        Tool = Int32.Parse(_ini.Read("tool", _modeSection));
        JColor = ColorHelper.StringToColor(_ini.Read("jcolor", _modeSection));
        Color = ColorHelper.StringToColor(_ini.Read("color", _modeSection));

        for (int i = 0; i < Constants.Gk540ConfigKeysCount; i++) {
            string read = _ini.Read(i.ToString(), _modeSection);
            Color c = ColorHelper.StringToColor(read);
            ColorsData[i] = c;
            _colorsTmp[i] = c;
        }
    }

    public void UpdateKey(int keyNumber, Color newColor) {
        _colorsTmp[keyNumber] = newColor;
        _hasChanged = true;
    }
    

    public bool Reset() {
        if (!_hasChanged) return false;
        Array.Copy(ColorsData, _colorsTmp, _colorsTmp.Length);
        _hasChanged = false;
        return true;
    }
    

    public void SaveKeys() {
        if (!_hasChanged) return;

        Parallel.For(0, Constants.Gk540ConfigKeysCount - 1, i => {
            if (ColorHelper.IsSameColor(ColorsData[i], _colorsTmp[i])) return;

            ColorsData[i] = _colorsTmp[i];
            string value = $"{ColorHelper.ColorToString(ColorsData[i])}";

            if (ColorHelper.IsSameColor(ColorsData[i], Color.Black))
                value += ",1";
            else
                value += ",0";

            _ini.Write(i.ToString(), value, _modeSection);
        });

        _hasChanged = false;
    }
}
}