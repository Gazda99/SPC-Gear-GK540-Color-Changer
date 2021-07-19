using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace GK540_Color_Changer.Files {
/// <summary>
/// Class for reading from and writing to an ini-like file 
/// </summary>
public class Ini {
    private readonly string _path;

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString,
        string lpFileName);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault,
        StringBuilder lpReturnedString, uint nSize, string lpFileName);

    public Ini(string iniPath) {
        _path = new FileInfo(iniPath).FullName;
    }

    /// <summary>
    /// Reads ini entry
    /// </summary>
    /// <param name="key">Key</param>
    /// <param name="section">Section</param>
    /// <returns>Value read</returns>
    public string Read(string key, string section) {
        StringBuilder retVal = new StringBuilder(255);
        GetPrivateProfileString(section, key, "", retVal, 255, _path);
        return retVal.ToString();
    }


    /// <summary>
    /// Writes ini entry
    /// </summary>
    /// <param name="key">Key</param>
    /// <param name="value">Value</param>
    /// <param name="section">Section</param>
    public void Write(string key, string value, string section) {
        WritePrivateProfileString(section, key, value, _path);
    }
}
}