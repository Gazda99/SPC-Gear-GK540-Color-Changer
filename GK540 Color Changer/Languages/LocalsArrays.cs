using System;
using GK540_Color_Changer.Files;

namespace GK540_Color_Changer.Languages {
public static class LocalsArrays {
    private static readonly string[] _paintingModesEN = new[] {"Paint each key individually", "Use selected color"};
    private static readonly string[] _paintingModesPL = new[] {"Maluj każdy klawisz osobno", "Użyj wspólnego koloru"};

    private static string[] PaintingModes() {
        return Locals.CurrentLang switch {
            Langs.EN => _paintingModesEN,
            Langs.PL => _paintingModesPL,
            _ => _paintingModesEN
        };
    }

    private static string[] Profiles() {
        string[] result = new string[ConfigConstants.ProfilesCount];
        for (int i = 0; i < ConfigConstants.ProfilesCount; i++)
            result[i] = $"{Locals.GetString("profile")} {i + 1}";

        return result;
    }

    private static readonly string[] _configLocationsEN = new string[] {"Default files location", "App folder location"};
    private static readonly string[] _configLocationsPL = new string[] {"Domyślna lokalizacja", "Folder aplikacji"};

    private static string[] ConfigLocation() {
        return Locals.CurrentLang switch {
            Langs.EN => _configLocationsEN,
            Langs.PL => _configLocationsPL,
            _ => _configLocationsEN
        };
    }

    public static string[] GetStrings(string name) {
        return name switch {
            "paintingModes" => PaintingModes(),
            "profiles" => Profiles(),
            "configLocations" => ConfigLocation(),
            _ => Array.Empty<string>()
        };
    }
}
}