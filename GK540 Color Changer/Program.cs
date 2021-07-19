using System;
using System.Windows.Forms;
using GK540_Color_Changer.Components;
using GK540_Color_Changer.Languages;

namespace GK540_Color_Changer {
static class Program {
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main() {
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        Locals.SetLanguage();
        
        Application.Run(new MainForm());
    }
}
}