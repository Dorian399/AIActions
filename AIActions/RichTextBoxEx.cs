using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

class RichTextBoxEx : RichTextBox
{
    public string Cue
    {
        get { return cue; }
        set
        {
            showCue(false);
            cue = value;
            if (this.Focused) showCue(true);
        }
    }
    private string cue;
    private bool stopshowingcue;

    public event EventHandler EnterPressed;

    protected override void OnCreateControl()
    {
        base.OnCreateControl();
        this.SelectionStart = 0;
        this.SelectionLength = 0;
        this.stopshowingcue = false;
    }

    protected override void OnEnter(EventArgs e)
    {
        if (this.stopshowingcue)
            showCue(false);
        else
            showCue(true);
        this.stopshowingcue = true;
        base.OnEnter(e);
    }

    protected override void OnClick(EventArgs e)
    {
        showCue(false);
        this.stopshowingcue = true;
        base.OnEnter(e);
    }

    protected override void OnLeave(EventArgs e)
    {
        showCue(true);
        this.stopshowingcue = true;
        base.OnLeave(e);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        if(this.Text == cue)
            showCue(false);

        if (e.KeyCode == Keys.Enter)
        {
            if (e.Shift)
            {
                int caret = this.SelectionStart;
                this.Text = this.Text.Insert(caret, Environment.NewLine);
                this.SelectionStart = caret + Environment.NewLine.Length;
            }

            e.SuppressKeyPress = true;
            EnterPressed?.Invoke(this,e);
        }

        base.OnKeyDown(e);
    }

    protected override void OnVisibleChanged(EventArgs e)
    {
        if (!this.Focused) showCue(true);
        base.OnVisibleChanged(e);
    }

    private void showCue(bool visible)
    {
        if (this.DesignMode) visible = false;
        if (visible)
        {
            if (this.Text.Length == 0)
            {
                this.Text = cue;
                this.SelectAll();
                this.SelectionColor = Color.FromArgb(87, 87, 87);
            }
        }
        else
        {
            if (this.Text == cue)
            {
                this.Text = "";
                this.SelectionColor = this.ForeColor;
            }
        }
    }
}