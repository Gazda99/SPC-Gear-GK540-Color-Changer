using System.Windows.Forms;
using GK540_Color_Changer.Components;
using GK540_Color_Changer.Languages;

namespace GK540_Color_Changer.Files {
public static class FileNotLoadedError {
    public static void InvokeImageNotLoaded(string fileNotLoadedExceptionMessage) {
        string errorMessage =
            $"{Locals.GetString("imageNotLoadedErrorMsg")}\n{fileNotLoadedExceptionMessage}";
        InvokeError(errorMessage);
    }

    public static void InvokeConfigNotLoaded(string configPath) {
        string errorMessage =
            $"{Locals.GetString("configNotLoadedErrorMsg")}\n{configPath}";
        InvokeError(errorMessage);
    }

    private static void InvokeError(string errorMessage) {
        CustomBox errorBox = new CustomBox(errorMessage, CustomBoxType.ErrorBox, false, true);
        errorBox.ShowDialog();

        Application.Exit();
    }
}
}