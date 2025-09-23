using AIActions.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TextPreviewButton : Button
{
    private string _previewText;
    private string _previewTitle;
    private TextPreviewWindow? _childWindow;

    private void RemoveChildWindow(object sender, FormClosedEventArgs e)
    {
        _childWindow = null;
    }

    public void SetPreviewText(string text,string title)
    {
        _previewText = text;
        _previewTitle = title;
    }

    protected override void OnClick(EventArgs e)
    {
        if (_previewText != null && _childWindow == null)
        {
            _childWindow = new TextPreviewWindow(_previewText,_previewTitle);
            _childWindow.Show();
            _childWindow.FormClosed += RemoveChildWindow;
        }
        base.OnClick(e);
    }
}
