using System;
using System.Drawing;
using System.Linq;

namespace GK540_Color_Changer {
/// <summary>
/// Provides methods related to Color
/// </summary>
public static class ColorHelper {
    /// <summary>
    /// Check if two colors have same RGB values
    /// </summary>
    /// <param name="c1">Color 1</param>
    /// <param name="c2">Color 2</param>
    public static bool IsSameColor(Color c1, Color c2) {
        return c1.R == c2.R && c1.G == c2.G && c1.B == c2.B;
    }

    /// <summary>
    /// Converts the string to Color struct
    /// </summary>
    /// <param name="value">String to convert</param>
    /// <returns>Color from string</returns>
    public static Color StringToColor(string value) {
        byte[] byteColorValues;
        try {
            byteColorValues = value.Split(',').Select(Byte.Parse).ToArray();
        }
        catch (Exception) {
            return Color.Black;
        }

        Color c = Color.FromArgb(byteColorValues[0], byteColorValues[1], byteColorValues[2]);
        return c;
    }



    /// <summary>
    /// Converts the Color struct to a string
    /// </summary>
    /// <param name="c">Color to convert</param>
    /// <returns>String from Color</returns>
    public static string ColorToString(Color c) {
        return $"{c.R},{c.G},{c.B}";
    }

    /// <summary>
    /// Converts the Color struct to a string with additional R= G= B= labels
    /// </summary>
    /// <param name="c">Color to convert</param>
    /// <returns>String from Color</returns>
    public static string ColorToStringWithLabel(Color c) {
        return $"R={c.R} G={c.G} B={c.B}";
    }
}
}