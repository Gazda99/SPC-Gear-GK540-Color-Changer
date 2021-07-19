namespace GK540_Color_Changer.Keys {
/// <summary>
/// Stores key dimensions in pixels needed for keyboard picture box
/// </summary>
public static class KeyDimensions {
    public const int NormalWidth = 31;
    public const int NormalHeight = 32;

    public const int TildeWidth = 29;
    public const int TildeHeight = NormalHeight;

    public const int BackspaceWidth = 76;
    public const int BackspaceHeight = NormalHeight;

    public const int TabWidth = 55;
    public const int TabHeight = NormalHeight;

    public const int BackslashWidth = 53;
    public const int BackslashHeight = NormalHeight;

    public const int NumVerticalWidth = NormalWidth;
    public const int NumVerticalHeight = 78;

    public const int NumHorizontalWidth = 76;
    public const int NumHorizontalHeight = NormalHeight;

    public const int CapsWidth = 66;
    public const int CapsHeight = NormalHeight;

    public const int EnterWidth = 88;
    public const int EnterHeight = NormalHeight;

    public const int LShiftWidth = 90;
    public const int LShiftHeight = NormalHeight;

    public const int RShiftWidth = 109;
    public const int RShiftHeight = NormalHeight;

    public const int SpaceWidth = 268;
    public const int SpaceHeight = NormalHeight;

    public const int SpecialKeyWidth = 43;
    public const int SpecialKeyHeight = NormalHeight;

    public static readonly int[] RowHeights = new[] {19, 80, 125, 170, 216, 261};
}
}