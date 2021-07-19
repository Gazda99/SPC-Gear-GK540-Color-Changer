using System;
using System.Windows.Forms;
using GK540_Color_Changer.Languages;

namespace GK540_Color_Changer.Components {
/// <summary>
/// Custom ContextMenuStrip with 3 options: copy, cut, paste
/// </summary>
public class CustomContextMenuStrip : ContextMenuStrip {
    private readonly TextBox _parentTextBox;

    public CustomContextMenuStrip(TextBox parentTextBox) {
        _parentTextBox = parentTextBox;
        InitItems();
    }

    private void InitItems() {
        ToolStripMenuItem ctxCopy = new ToolStripMenuItem {Text = Locals.GetString("copy")};
        ctxCopy.Click += Copy;
        this.Items.Add(ctxCopy);

        ToolStripMenuItem ctxCut = new ToolStripMenuItem {Text = Locals.GetString("cut")};
        ctxCut.Click += Cut;
        this.Items.Add(ctxCut);

        ToolStripMenuItem ctxPaste = new ToolStripMenuItem {Text = Locals.GetString("paste")};
        ctxPaste.Click += Paste;
        this.Items.Add(ctxPaste);
    }

    private void Copy(object sender, EventArgs e) {
        _parentTextBox?.Copy();
    }


    private void Cut(object sender, EventArgs e) {
        _parentTextBox?.Cut();
    }

    private void Paste(object sender, EventArgs e) {
        _parentTextBox?.Paste();
    }
}
}