using System;

namespace GK540_Color_Changer.Files {
/// <summary>
/// Provides path to files
/// </summary>
public static class FilesPath {
    private const string ResourcesLocation = "Resources\\";
    private const string DefaultConfigLocation = "DefaultConfigs\\";

    private static string CreateResourcesPath(string file) {
        return $"{ResourcesLocation}{file}";
    }

    public static readonly string AppDataPath =
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GK540 Color Changer";


    public static string ConfigPath =>
        $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Gaming Keyboard";

    public static string DefaultConfigPath =>
        $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\{DefaultConfigLocation}";


    public static string IconImage => CreateResourcesPath("keyboard_color.ico");
    public static string GK540KeyboardImage => CreateResourcesPath("gk540.png");
    public static string ExitButtonImage => CreateResourcesPath("exit_button_circle.png");
    public static string MinimizeButtonImage => CreateResourcesPath("minimize_button_square.png");

    public static string ConfigFile(int n) => $"{ConfigPath}\\UserDengColor_{n}.userd";
    public static string DefaultConfigFile(int n) => $"{DefaultConfigLocation}\\UserDengColor_{n}.userd";
}
}