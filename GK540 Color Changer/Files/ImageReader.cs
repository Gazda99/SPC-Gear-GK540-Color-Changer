using System;
using System.Drawing;
using GK540_Color_Changer.Languages;

namespace GK540_Color_Changer.Files {
/// <summary>
/// Class used for loading the images needed for application
/// </summary>
public static class ImageReader {
    /// <summary>
    /// Reads an image from system to app
    /// </summary>
    /// <param name="path">Path to file - relative to .exe</param>
    /// <returns>Image read</returns>
    /// <exception cref="ImageNotLoadedException">Image could not be loaded</exception>
    public static Image Read(string path) {
        Image img;
        try {
            using Bitmap bmpTemp = new Bitmap(path);
            img = new Bitmap(bmpTemp);
        }
        catch (Exception) {
            throw new ImageNotLoadedException($"{Locals.GetString("fileNotLoadedExMsg")} {path}");
        }

        return img;
    }

    /// <summary>
    /// Reads an icon from system to app
    /// </summary>
    /// <param name="path">Path to file - relative to .exe</param>
    /// <returns>Icon read</returns>
    /// <exception cref="ImageNotLoadedException">Icon could not be loaded</exception>
    public static Icon ReadIcon(string path) {
        Icon icon;
        try {
            icon = new Icon(FilesPath.IconImage);
        }
        catch (Exception) {
            throw new ImageNotLoadedException($"{Locals.GetString("fileNotLoadedExMsg")} {path}");
        }

        return icon;
    }
}
}