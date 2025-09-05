namespace AIActions
{
    partial class PromptWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            RunAction = new Button();
            Settings = new Button();
            PromptBox = new RichTextBoxEx();
            SuspendLayout();
            // 
            // RunAction
            // 
            RunAction.Location = new Point(711, 52);
            RunAction.Name = "RunAction";
            RunAction.Size = new Size(77, 29);
            RunAction.TabIndex = 1;
            RunAction.Text = "Execute";
            RunAction.UseVisualStyleBackColor = true;
            RunAction.Click += RunAction_Click;
            // 
            // Settings
            // 
            Settings.Location = new Point(711, 12);
            Settings.Name = "Settings";
            Settings.Size = new Size(77, 27);
            Settings.TabIndex = 2;
            Settings.Text = "Settings";
            Settings.UseVisualStyleBackColor = true;
            Settings.Click += Settings_Click;
            // 
            // PromptBox
            // 
            PromptBox.Cue = "Enter your prompt.";
            PromptBox.Location = new Point(8, 12);
            PromptBox.MaxLength = 5000;
            PromptBox.Name = "PromptBox";
            PromptBox.Size = new Size(697, 69);
            PromptBox.TabIndex = 0;
            PromptBox.Text = "";
            PromptBox.EnterPressed += PromptBox_EnterPressed;
            // 
            // PromptWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 89);
            Controls.Add(Settings);
            Controls.Add(RunAction);
            Controls.Add(PromptBox);
            Name = "PromptWindow";
            Text = "AIActions";
            Load += PromptWindow_Load;
            ResumeLayout(false);
        }

        #endregion
        private Button RunAction;
        private Button Settings;
        private RichTextBoxEx PromptBox;
    }
}
