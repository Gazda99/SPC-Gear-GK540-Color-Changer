using System.Windows.Forms;

namespace GK540_Color_Changer {
/// <summary>
/// Provides methods for arranging components
/// </summary>
public static class LayoutHelpers {
    /// <summary>
    /// Get the X or Y coordinate which is centered relative to a parent Control
    /// </summary>
    /// <param name="parentComponentSize">Parent width or height</param>
    /// <param name="componentSize">Centered component width or height</param>
    public static int CenterCoordinate(int parentComponentSize, int componentSize) {
        return (int) (parentComponentSize * 0.5 - componentSize * 0.5);
    }

    /// <summary>
    /// Calculates the Y offset of control
    /// </summary>
    /// <param name="c">Control</param>
    /// <param name="offSet">Additional offset</param>
    /// <returns>Y offset</returns>
    public static int GetYOffset(Control c, int offSet = 0) {
        return c.Location.Y + c.Size.Height + offSet;
    }
}
}