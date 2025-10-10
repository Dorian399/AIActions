namespace AIActions.Windows
{
    partial class TextPreviewWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            TextPreview = new RichTextBox();
            SuspendLayout();
            // 
            // TextPreview
            // 
            TextPreview.Dock = DockStyle.Fill;
            TextPreview.ForeColor = SystemColors.WindowText;
            TextPreview.Location = new Point(0, 0);
            TextPreview.Name = "TextPreview";
            TextPreview.ReadOnly = true;
            TextPreview.Size = new Size(518, 498);
            TextPreview.TabIndex = 0;
            TextPreview.Text = "";
            // 
            // TextPreviewWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(518, 498);
            Controls.Add(TextPreview);
            Name = "TextPreviewWindow";
            Text = "Code preview";
            Icon = Properties.Resources.Icon;
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox TextPreview;
    }
}