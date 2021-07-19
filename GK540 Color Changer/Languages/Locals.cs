using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace GK540_Color_Changer.Languages {
/// <summary>
/// Class used for setting the app language, and providing the method for getting a localized strings
/// </summary>
public static class Locals {
    private static ResourceManager _rm =
        new ResourceManager("GK540_Color_Changer.Local.en_local", GetAssembly());


    public static Langs CurrentLang { get; private set; }

    static Locals() {
        CurrentLang = Langs.EN;
    }


    /// <summary>
    /// Sets the language of app to what the system has
    /// </summary>
    public static void SetLanguage() {
        CultureInfo ci = CultureInfo.CurrentUICulture;
        SetLanguage(ci.ThreeLetterWindowsLanguageName);
    }

    /// <summary>
    /// Sets the language of app, using provided three letter windows language name 
    /// </summary>
    /// <param name="threeLetterWindowsLanguageName">Three-letter code for the language as defined in the Windows API.</param>
    public static void SetLanguage(string threeLetterWindowsLanguageName) {
        switch (threeLetterWindowsLanguageName) {
            case "PLK":
                SetLanguage(Langs.PL);
                break;
            default:
                SetLanguage(Langs.EN);
                break;
        }
    }

    /// <summary>
    /// Sets the language of app
    /// </summary>
    /// <param name="lang">Language enum</param>
    public static void SetLanguage(Langs lang) {
        string thisNamespace = typeof(Locals).Namespace;
        try {
            _rm = lang switch {
                Langs.EN => new ResourceManager($"{thisNamespace}.en_local", GetAssembly()),
                Langs.PL => new ResourceManager($"{thisNamespace}.pl_local", GetAssembly()),
                _ => new ResourceManager($"{thisNamespace}.en_local", GetAssembly())
            };
            CurrentLang = lang;
        }
        catch (Exception) {
            _rm = new ResourceManager($"{thisNamespace}.en_local", GetAssembly());
            CurrentLang = Langs.EN;
        }
    }

    /// <summary>
    /// Gets the string in the language now selected
    /// </summary>
    /// <param name="name">Name of desired string resource</param>
    /// <returns>Localized string</returns>
    public static string GetString(string name) {
        return _rm.GetString(name);
    }

    private static Assembly GetAssembly() {
        return Assembly.GetExecutingAssembly();
    }
}

/// <summary>
/// Enum representing Language codes
/// </summary>
public enum Langs {
    EN,
    PL
}
}