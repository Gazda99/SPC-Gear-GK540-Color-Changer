using System.Collections.Generic;
using System.Drawing;
using System.IO;
using GK540_Color_Changer.Keys;

namespace GK540_Color_Changer.Files {
public class KeyboardConfigEditor {
    private const int ConfigCount = 4;

    private readonly Config[] _configs;
    public int CurrentProfile { get; set; }

    public int CurrentMode {
        get { return _configs[CurrentProfile].CurrentMode; }
        set { _configs[CurrentProfile].CurrentMode = value; }
    }

    public KeyboardConfigEditor() {
        _configs = new Config[ConfigCount];
    }

    public bool ReadAll() {
        if (!CheckConfigExists())
            return false;

        for (int i = 0; i < ConfigCount; i++)
            _configs[i] = new Config(FilesPath.ConfigFile(i));

        return true;
    }

    public bool DefaultConfig() {
        for (int i = 0; i < ConfigCount; i++)
            if (!File.Exists(FilesPath.DefaultConfigFile(i)))
                return false;

        if (!Directory.Exists(FilesPath.ConfigPath))
            Directory.CreateDirectory(FilesPath.ConfigPath);


        for (int i = 0; i < ConfigCount; i++) {
            using StreamWriter sw = File.CreateText(FilesPath.ConfigFile(i));
            sw.Write(File.ReadAllText(FilesPath.DefaultConfigFile(i)));
        }

        return true;
    }

    public void UpdateKey(int keyNumber, Color newColor) {
        _configs[CurrentProfile].UpdateKey(CurrentMode, keyNumber, newColor);
    }

    public void UpdateKeys(Dictionary<int, Key> keyDict) {
        foreach ((int keyNumber, Key k) in keyDict) {
            _configs[CurrentProfile].UpdateKey(CurrentMode, keyNumber, k.Color);
        }
    }

    public bool Reset() {
        return _configs[CurrentProfile].Reset();
    }

    public void SaveKeys() {
        _configs[CurrentProfile].SaveKeys(CurrentMode);
    }

    public string[] GetModeNames() {
        return _configs[CurrentProfile].ModeNames;
    }

    public void ChangeModeName(int n, string newName) {
        _configs[CurrentProfile].SetModeName(n, newName);
    }

    public Color[] GetColorScheme() {
        return _configs[CurrentProfile].GetColorScheme(CurrentMode);
    }

    private static bool CheckConfigExists() {
        for (int i = 0; i < ConfigCount; i++) {
            if (File.Exists(FilesPath.ConfigFile(i))) continue;
            return false;
        }

        return true;
    }
}
}