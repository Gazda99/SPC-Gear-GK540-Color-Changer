using System.Collections.Generic;
using KeyDims = GK540_Color_Changer.Keys.KeyDimensions;

namespace GK540_Color_Changer.Keys {
/// <summary>
/// Class for creating collection of keys 
/// </summary>
public class KeyData {
    public Dictionary<int, Key> GetGK540KeyDataDict() {
        Dictionary<int, Key> keys = new Dictionary<int, Key>(_gk540KeysData.Length);

        for (int i = 0; i < _gk540KeysData.Length; i++)
            keys.Add(_gk540KeysData[i].KeyNumber, _gk540KeysData[i]);

        return keys;
    }

    private readonly Key[] _gk540KeysData = new[] {
        //First row
        new Key(0, "Esc", 21, KeyDims.RowHeights[0], KeyDims.NormalWidth, KeyDims.NormalWidth),
        new Key(1, "F1", 113, KeyDims.RowHeights[0], KeyDims.NormalWidth, KeyDims.NormalWidth),
        new Key(2, "F2", 158, KeyDims.RowHeights[0], KeyDims.NormalWidth, KeyDims.NormalWidth),
        new Key(3, "F3", 203, KeyDims.RowHeights[0], KeyDims.NormalWidth, KeyDims.NormalWidth),
        new Key(4, "F4", 248, KeyDims.RowHeights[0], KeyDims.NormalWidth, KeyDims.NormalWidth),

        new Key(5, "F5", 317, KeyDims.RowHeights[0], KeyDims.NormalWidth, KeyDims.NormalWidth),
        new Key(6, "F6", 362, KeyDims.RowHeights[0], KeyDims.NormalWidth, KeyDims.NormalWidth),
        new Key(7, "F7", 407, KeyDims.RowHeights[0], KeyDims.NormalWidth, KeyDims.NormalWidth),
        new Key(8, "F8", 452, KeyDims.RowHeights[0], KeyDims.NormalWidth, KeyDims.NormalWidth),

        new Key(9, "F9", 521, KeyDims.RowHeights[0], KeyDims.NormalWidth, KeyDims.NormalWidth),
        new Key(10, "F10", 566, KeyDims.RowHeights[0], KeyDims.NormalWidth, KeyDims.NormalWidth),
        new Key(11, "F11", 611, KeyDims.RowHeights[0], KeyDims.NormalWidth, KeyDims.NormalWidth),
        new Key(12, "F12", 656, KeyDims.RowHeights[0], KeyDims.NormalWidth, KeyDims.NormalWidth),
        //13????
        new Key(14, "PrtSc", 717, KeyDims.RowHeights[0], KeyDims.NormalWidth, KeyDims.NormalWidth),
        new Key(15, "ScrLk", 762, KeyDims.RowHeights[0], KeyDims.NormalWidth, KeyDims.NormalWidth),
        new Key(16, "Pause", 807, KeyDims.RowHeights[0], KeyDims.NormalWidth, KeyDims.NormalWidth),
        //
        //17-20???

        //Second row
        new Key(21, "Tilde", 21, KeyDims.RowHeights[1], KeyDims.TildeWidth, KeyDims.TildeHeight),
        new Key(22, "1", 67, KeyDims.RowHeights[1], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(23, "2", 112, KeyDims.RowHeights[1], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(24, "3", 157, KeyDims.RowHeights[1], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(25, "4", 203, KeyDims.RowHeights[1], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(26, "5", 248, KeyDims.RowHeights[1], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(27, "6", 293, KeyDims.RowHeights[1], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(28, "7", 338, KeyDims.RowHeights[1], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(29, "8", 384, KeyDims.RowHeights[1], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(30, "9", 429, KeyDims.RowHeights[1], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(31, "10", 474, KeyDims.RowHeights[1], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(32, "Minus", 519, KeyDims.RowHeights[1], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(33, "Plus", 564, KeyDims.RowHeights[1], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(34, "Backspace", 610, KeyDims.RowHeights[1], KeyDims.BackspaceWidth,
            KeyDims.BackspaceHeight),

        new Key(35, "Ins", 717, KeyDims.RowHeights[1], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(36, "Home", 762, KeyDims.RowHeights[1], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(37, "Page Up", 807, KeyDims.RowHeights[1], KeyDims.NormalWidth, KeyDims.NormalHeight),

        new Key(38, "NumLock", 869, KeyDims.RowHeights[1], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(39, "Num Slash", 914, KeyDims.RowHeights[1], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(40, "Num Asterisk", 959, KeyDims.RowHeights[1], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(41, "Num Minus", 1004, KeyDims.RowHeights[1], KeyDims.NormalWidth, KeyDims.NormalHeight),
        //

        //Third row
        new Key(42, "Tab", 21, KeyDims.RowHeights[2], KeyDims.TabWidth, KeyDims.TabHeight),
        new Key(43, "Q", 90, KeyDims.RowHeights[2], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(44, "W", 135, KeyDims.RowHeights[2], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(45, "E", 180, KeyDims.RowHeights[2], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(46, "R", 226, KeyDims.RowHeights[2], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(47, "T", 271, KeyDims.RowHeights[2], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(48, "Y", 316, KeyDims.RowHeights[2], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(49, "U", 361, KeyDims.RowHeights[2], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(50, "I", 406, KeyDims.RowHeights[2], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(51, "O", 452, KeyDims.RowHeights[2], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(52, "P", 497, KeyDims.RowHeights[2], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(53, "Left Bracket", 542, KeyDims.RowHeights[2], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(54, "Right Bracket", 587, KeyDims.RowHeights[2], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(55, "Backslash", 633, KeyDims.RowHeights[2], KeyDims.BackslashWidth, KeyDims.BackslashHeight),

        new Key(56, "Del", 717, KeyDims.RowHeights[2], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(57, "End", 762, KeyDims.RowHeights[2], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(58, "Page Down", 807, KeyDims.RowHeights[2], KeyDims.NormalWidth, KeyDims.NormalHeight),

        new Key(59, "Num 7", 869, KeyDims.RowHeights[2], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(60, "Num 8", 914, KeyDims.RowHeights[2], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(61, "Num 9", 959, KeyDims.RowHeights[2], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(62, "Num Plus", 1004, KeyDims.RowHeights[2], KeyDims.NumVerticalWidth, KeyDims.NumVerticalHeight),
        //

        //Fourth row
        new Key(63, "Caps ", 21, KeyDims.RowHeights[3], KeyDims.CapsWidth, KeyDims.CapsHeight),
        new Key(64, "A", 101, KeyDims.RowHeights[3], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(65, "S", 146, KeyDims.RowHeights[3], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(66, "D", 191, KeyDims.RowHeights[3], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(67, "F", 236, KeyDims.RowHeights[3], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(68, "G", 281, KeyDims.RowHeights[3], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(69, "H", 326, KeyDims.RowHeights[3], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(70, "J", 372, KeyDims.RowHeights[3], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(71, "K", 417, KeyDims.RowHeights[3], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(72, "L", 462, KeyDims.RowHeights[3], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(73, "Semicolon", 508, KeyDims.RowHeights[3], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(74, "Apostrophe", 553, KeyDims.RowHeights[3], KeyDims.NormalWidth, KeyDims.NormalHeight),
        //75?
        new Key(76, "Enter", 598, KeyDims.RowHeights[3], KeyDims.EnterWidth, KeyDims.EnterHeight),
        //77-79?
        new Key(80, "Num 4", 869, KeyDims.RowHeights[3], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(81, "Num 5", 914, KeyDims.RowHeights[3], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(82, "Num 6", 959, KeyDims.RowHeights[3], KeyDims.NormalWidth, KeyDims.NormalHeight),
        //

        //83?

        //Fifth row
        new Key(84, "Left Shift", 21, KeyDims.RowHeights[4], KeyDims.LShiftWidth, KeyDims.LShiftHeight),
        //85?
        new Key(86, "Z", 125, KeyDims.RowHeights[4], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(87, "X", 170, KeyDims.RowHeights[4], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(88, "C", 215, KeyDims.RowHeights[4], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(89, "V", 260, KeyDims.RowHeights[4], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(90, "B", 305, KeyDims.RowHeights[4], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(91, "N", 350, KeyDims.RowHeights[4], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(92, "M", 395, KeyDims.RowHeights[4], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(93, "Comma", 441, KeyDims.RowHeights[4], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(94, "Dot", 486, KeyDims.RowHeights[4], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(95, "Slash", 532, KeyDims.RowHeights[4], KeyDims.NormalWidth, KeyDims.NormalHeight),
        //96?
        new Key(97, "Right Shift", 577, KeyDims.RowHeights[4], KeyDims.RShiftWidth, KeyDims.RShiftHeight),
        //98?
        new Key(99, "Arrow Up", 762, KeyDims.RowHeights[4], KeyDims.NormalWidth, KeyDims.NormalWidth),
        //100?
        new Key(101, "Num 1", 869, KeyDims.RowHeights[4], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(102, "Num 2", 914, KeyDims.RowHeights[4], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(103, "Num 3", 959, KeyDims.RowHeights[4], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(104, "Num Enter", 1004, KeyDims.RowHeights[4], KeyDims.NumVerticalWidth, KeyDims.NumVerticalHeight),
        //


        //Sixth row
        new Key(105, "Ctrl Left", 21, KeyDims.RowHeights[5], KeyDims.SpecialKeyWidth, KeyDims.SpecialKeyHeight),
        new Key(106, "Windows", 78, KeyDims.RowHeights[5], KeyDims.SpecialKeyWidth, KeyDims.SpecialKeyHeight),
        new Key(107, "Alt Left", 135, KeyDims.RowHeights[5], KeyDims.SpecialKeyWidth, KeyDims.SpecialKeyHeight),
        new Key(108, "Space", 192, KeyDims.RowHeights[5], KeyDims.SpaceWidth, KeyDims.SpaceHeight),
        new Key(109, "Alt Right", 475, KeyDims.RowHeights[5], KeyDims.SpecialKeyWidth, KeyDims.SpecialKeyHeight),
        new Key(110, "Windows SPC", 531, KeyDims.RowHeights[5], KeyDims.SpecialKeyWidth, KeyDims.SpecialKeyHeight),
        new Key(111, "Menu", 588, KeyDims.RowHeights[5], KeyDims.SpecialKeyWidth, KeyDims.SpecialKeyHeight),
        //112?
        new Key(113, "Ctrl Right", 643, KeyDims.RowHeights[5], KeyDims.SpecialKeyWidth, KeyDims.SpecialKeyHeight),
        //114-118?
        new Key(119, "Arrow Left", 717, KeyDims.RowHeights[5], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(120, "Arrow Down", 762, KeyDims.RowHeights[5], KeyDims.NormalWidth, KeyDims.NormalHeight),
        new Key(121, "Arrow Right", 807, KeyDims.RowHeights[5], KeyDims.NormalWidth, KeyDims.NormalHeight),
        //122?
        new Key(123, "Num 0", 869, KeyDims.RowHeights[5], KeyDims.NumHorizontalWidth, KeyDims.NumHorizontalHeight),
        new Key(124, "Num Comma", 959, KeyDims.RowHeights[5], KeyDims.NormalWidth, KeyDims.NormalHeight),
    };
}
}