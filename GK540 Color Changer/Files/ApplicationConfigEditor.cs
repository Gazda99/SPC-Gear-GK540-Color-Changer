using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace GK540_Color_Changer.Files {
public class ApplicationConfigEditor {
    private const string FileName = "app_data.ini";
    private const string DataSection = "DATA";
    private const string SelectedColorModeKey = "selected_color_mode";
    private const string SelectedProfileKey = "selected_profile";
    private const string SelectedModeKey = "selected_mode";
    private const string CommonColorKey = "common_color";
    private static string CustomColorKey(int n) => $"custom_color_{n}";

    private static readonly string ConfigLocation = $"{FilesPath.AppDataPath}\\{FileName}";
    private readonly Ini _ini;

    private int _selectedColorMode;
    private int _selectedProfile;
    private int _selectedMode;
    private Color _commonColor;
    private Color[] _customColors;


    public int SelectedProfile {
        get { return _selectedProfile; }
        set {
            _selectedProfile = value;
            WriteToConfig(SelectedProfileKey, _selectedProfile.ToString(), DataSection);
        }
    }

    public int SelectedMode {
        get { return _selectedMode; }
        set {
            _selectedMode = value;
            WriteToConfig(SelectedModeKey, _selectedMode.ToString(), DataSection);
        }
    }

    public int SelectedColorMode {
        get { return _selectedColorMode; }
        set {
            _selectedColorMode = value;
            WriteToConfig(SelectedColorModeKey, _selectedColorMode.ToString(), DataSection);
        }
    }

    public Color CommonColor {
        get { return _commonColor; }
        set {
            _commonColor = value;
            WriteToConfig(CommonColorKey, ColorHelper.ColorToString(_commonColor), DataSection);
        }
    }

    public Color[] GetCustomColors() {
        Color[] copyArray = new Color[ConfigConstants.CustomColorCount];
        Array.Copy(_customColors, copyArray, ConfigConstants.CustomColorCount);
        return copyArray;
    }

    public int[] GetCustomColorsOle() {
        return _customColors.Select(ColorTranslator.ToOle).ToArray();
    }

    public void SetCustomColor(int n, Color c) {
        if (n is < 0 or >= ConfigConstants.CustomColorCount) return;
        _customColors[n] = c;
        WriteToConfig(CustomColorKey(n), ColorHelper.ColorToString(c), DataSection);
    }

    public void SetCustomColors(Color[] colors) {
        if (colors.Length > ConfigConstants.CustomColorCount) return;

        for (int i = 0; i < colors.Length; i++) {
            if (ColorHelper.IsSameColor(colors[i], _customColors[i])) continue;
            _customColors[i] = colors[i];
            WriteToConfig(CustomColorKey(i), ColorHelper.ColorToString(_customColors[i]), DataSection);
        }
    }

    public void SetCustomColorsOle(int[] colors) {
        SetCustomColors(colors.Select(ColorTranslator.FromOle).ToArray());
    }

    public ApplicationConfigEditor() {
        _customColors = new Color[ConfigConstants.CustomColorCount];
        _ini = new Ini(ConfigLocation);

        if (!CheckIfFIleExists(ConfigLocation))
            CreateDefaultFile();
    }


    public void Read() {
        try {
            Int32.TryParse(_ini.Read(SelectedColorModeKey, DataSection), out _selectedColorMode);
            Int32.TryParse(_ini.Read(SelectedProfileKey, DataSection), out _selectedProfile);
            Int32.TryParse(_ini.Read(SelectedModeKey, DataSection), out _selectedMode);
            _commonColor = ColorHelper.StringToColor(_ini.Read(CommonColorKey, DataSection));

            for (int i = 0; i < ConfigConstants.CustomColorCount; i++)
                _customColors[i] = ColorHelper.StringToColor(_ini.Read(CustomColorKey(i), DataSection));
        }
        catch (Exception) {
            SetDefaultValues();
        }
    }

    private void WriteToConfig(string key, string value, string section) {
        _ini.Write(key, value, section);
    }

    private static void CreateDefaultFile() {
        if (!Directory.Exists(FilesPath.AppDataPath))
            Directory.CreateDirectory(FilesPath.AppDataPath);
        File.Create(ConfigLocation);
    }

    private void SetDefaultValues() {
        SelectedColorMode = 0;
        SelectedProfile = 0;
        SelectedMode = 0;
        CommonColor = Color.White;

        for (int i = 0; i < ConfigConstants.CustomColorCount; i++)
            SetCustomColor(i, ColorTranslator.FromOle(ConfigConstants.CustomColorDefaultOLEVal));
    }

    private static bool CheckIfFIleExists(string path) {
        return File.Exists(path);
    }
}
}