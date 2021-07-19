using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GK540_Color_Changer.Components {
/// <summary>
/// Custom color dialog with ability to change position and title easly
/// </summary>
public class CustomColorDialog : ColorDialog {
    private const int WM_INITDIALOG = 0x0110;
    private const uint SWP_NOSIZE = 0x0001;
    private const uint SWP_SHOWWINDOW = 0x0040;
    private const uint SWP_NOZORDER = 0x0004;
    private const uint UFLAGS = SWP_NOSIZE | SWP_NOZORDER | SWP_SHOWWINDOW;

    private readonly IntPtr HWND_TOP = new IntPtr(0);
    
    public event EventHandler CustomColorsChanged;

    public int X { get; set; }
    public int Y { get; set; }
    public string Title { get; set; }


    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern bool SetWindowText(IntPtr hWnd, string text);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy,
        uint uFlags);

    // [DllImport("user32.dll")]
    // private static extern bool GetWindowRect(HandleRef hWnd, out RECT lpRect);
    //
    // public int GetWidth() {
    //     using UserControl uc = new UserControl();
    //     GetWindowRect(new HandleRef(uc, uc.Handle), out var r);
    //     return r.Width;
    // }


    public CustomColorDialog(int x, int y, String title = null) {
        X = x;
        Y = y;
        Title = title;
    }

    public CustomColorDialog() { }

    protected override IntPtr HookProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam) {
        IntPtr hookProc = base.HookProc(hWnd, msg, wparam, lparam);
        if (msg != WM_INITDIALOG) return hookProc;
        if (!String.IsNullOrEmpty(Title))
            SetWindowText(hWnd, Title);

        SetWindowPos(hWnd, HWND_TOP, X, Y, 0, 0, UFLAGS);

        return hookProc;
    }

    public DialogResult ShowDialog(string title, Point cursorPos) {
        this.Title = title;
        this.X = cursorPos.X;
        this.Y = cursorPos.Y;

        int[] oldCustomColors = new int[this.CustomColors.Length];
        Array.Copy(this.CustomColors, oldCustomColors, this.CustomColors.Length);

        try {
            return this.ShowDialog();
        }
        finally {
            if (DoesCustomColorChanged(oldCustomColors))
                OnCustomColorsChanged();
        }
    }

    private void OnCustomColorsChanged() {
        CustomColorsChanged?.Invoke(this, EventArgs.Empty);
    }

    private bool DoesCustomColorChanged(int[] customColors) {
        if (customColors.Length != this.CustomColors.Length)
            return false;

        for (int i = 0; i < customColors.Length; i++) {
            if (this.CustomColors[i] != customColors[i])
                return true;
        }

        return false;
    }
}
}