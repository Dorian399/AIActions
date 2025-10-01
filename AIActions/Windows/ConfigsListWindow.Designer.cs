namespace AIActions.Windows
{
    partial class ConfigsListWindow
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
            ConfigsList = new AIActions.Windows.ConfigWindows.ConfigsList();
            SuspendLayout();
            // 
            // ConfigsList
            // 
            ConfigsList.Dock = DockStyle.Fill;
            ConfigsList.Location = new Point(0, 0);
            ConfigsList.Name = "ConfigsList";
            ConfigsList.Size = new Size(537, 432);
            ConfigsList.TabIndex = 0;
            // 
            // ConfigsListWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(537, 432);
            Controls.Add(ConfigsList);
            Name = "ConfigsListWindow";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private ConfigWindows.ConfigsList ConfigsList;
    }
}