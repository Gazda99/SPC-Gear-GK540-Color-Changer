using System;
using System.Drawing;

namespace GK540_Color_Changer.Files {
public class Config {
    private const string PresentModeSection = "PRESENTMODE";
    private const string ModeNameSection = "MODENAME";
    public string[] ModeNames { get; }
    private readonly Mode[] _modes;

    private int _currentMode;
    private readonly Ini _ini;

    public int CurrentMode {
        get { return _currentMode; }
        set {
            _currentMode = value;
            _modes[_currentMode].Reset();
            _ini.Write("mode", _currentMode.ToString(), PresentModeSection);
        }
    }

    public Config(string filePath) {
        ModeNames = new string[ConfigConstants.ModeCount];
        _modes = new Mode[ConfigConstants.ModeCount];

        _ini = new Ini(filePath);

        ReadConfig();
    }

    /// <summary>
    /// Reads the whole config, delegating the modes section to Mode class
    /// </summary>
    private void ReadConfig() {
        Int32.TryParse(_ini.Read("mode", PresentModeSection), out _currentMode);

        for (int i = 0; i < ConfigConstants.ModeCount; i++) {
            ModeNames[i] = _ini.Read(i.ToString(), ModeNameSection);
            _modes[i] = new Mode(i, _ini);
        }
    }

    public void UpdateKey(int modeNumber, int keyNumber, Color newColor) {
        _modes[modeNumber].UpdateKey(keyNumber, newColor);
    }

    public bool Reset() {
        return _modes[CurrentMode].Reset();
    }

    public void SaveKeys(int mode) {
        _modes[mode].SaveKeys();
    }

    public void SetModeName(int n, string newName) {
        ModeNames[n] = newName;
        _ini.Write(n.ToString(), newName, ModeNameSection);
    }

    public void SetPresentMode(int n) {
        _currentMode = n;
    }

    public Color[] GetColorScheme(int modeNumber) {
        return _modes[modeNumber].ColorsData;
    }
}
}