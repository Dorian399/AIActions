namespace AIActions.Windows.ConfigWindows
{
    partial class ConfigsList
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MainLayout = new TableLayoutPanel();
            HeaderLayout = new TableLayoutPanel();
            ConfigComboBox = new ComboBox();
            LoadedConfigsLabel = new Label();
            VariablesPanel = new Panel();
            HelpLabel = new Label();
            MainLayout.SuspendLayout();
            HeaderLayout.SuspendLayout();
            VariablesPanel.SuspendLayout();
            SuspendLayout();
            // 
            // MainLayout
            // 
            MainLayout.ColumnCount = 1;
            MainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            MainLayout.Controls.Add(HeaderLayout, 0, 0);
            MainLayout.Controls.Add(VariablesPanel, 0, 2);
            MainLayout.Dock = DockStyle.Fill;
            MainLayout.Location = new Point(0, 0);
            MainLayout.Margin = new Padding(0);
            MainLayout.Name = "MainLayout";
            MainLayout.RowCount = 3;
            MainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 39F));
            MainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 21F));
            MainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 207F));
            MainLayout.Size = new Size(555, 301);
            MainLayout.TabIndex = 0;
            // 
            // HeaderLayout
            // 
            HeaderLayout.ColumnCount = 2;
            HeaderLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 29.0090084F));
            HeaderLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70.99099F));
            HeaderLayout.Controls.Add(ConfigComboBox, 1, 0);
            HeaderLayout.Controls.Add(LoadedConfigsLabel, 0, 0);
            HeaderLayout.Dock = DockStyle.Fill;
            HeaderLayout.Location = new Point(0, 0);
            HeaderLayout.Margin = new Padding(0);
            HeaderLayout.Name = "HeaderLayout";
            HeaderLayout.RowCount = 1;
            HeaderLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            HeaderLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            HeaderLayout.Size = new Size(555, 39);
            HeaderLayout.TabIndex = 0;
            // 
            // ConfigComboBox
            // 
            ConfigComboBox.Dock = DockStyle.Fill;
            ConfigComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ConfigComboBox.FormattingEnabled = true;
            ConfigComboBox.Location = new Point(167, 6);
            ConfigComboBox.Margin = new Padding(6, 6, 20, 6);
            ConfigComboBox.Name = "ConfigComboBox";
            ConfigComboBox.Size = new Size(368, 28);
            ConfigComboBox.TabIndex = 0;
            ConfigComboBox.SelectedIndexChanged += ComboBox_IndexChanged;
            // 
            // LoadedConfigsLabel
            // 
            LoadedConfigsLabel.AutoSize = true;
            LoadedConfigsLabel.Dock = DockStyle.Fill;
            LoadedConfigsLabel.Font = new Font("Segoe UI", 10F);
            LoadedConfigsLabel.Location = new Point(3, 0);
            LoadedConfigsLabel.Name = "LoadedConfigsLabel";
            LoadedConfigsLabel.Size = new Size(155, 39);
            LoadedConfigsLabel.TabIndex = 1;
            LoadedConfigsLabel.Text = "Available configs:";
            LoadedConfigsLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // VariablesPanel
            // 
            VariablesPanel.AutoScroll = true;
            VariablesPanel.Controls.Add(HelpLabel);
            VariablesPanel.Dock = DockStyle.Fill;
            VariablesPanel.Location = new Point(3, 63);
            VariablesPanel.Name = "VariablesPanel";
            VariablesPanel.Padding = new Padding(5);
            VariablesPanel.Size = new Size(549, 235);
            VariablesPanel.TabIndex = 2;
            // 
            // HelpLabel
            // 
            HelpLabel.Dock = DockStyle.Top;
            HelpLabel.Font = new Font("Segoe UI", 10F);
            HelpLabel.Location = new Point(5, 5);
            HelpLabel.Name = "HelpLabel";
            HelpLabel.Size = new Size(539, 33);
            HelpLabel.TabIndex = 0;
            HelpLabel.Text = "Choose a config from the list above.";
            HelpLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ConfigsList
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(MainLayout);
            Name = "ConfigsList";
            Size = new Size(555, 301);
            Load += ConfigsList_Loaded;
            MainLayout.ResumeLayout(false);
            HeaderLayout.ResumeLayout(false);
            HeaderLayout.PerformLayout();
            VariablesPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel MainLayout;
        private TableLayoutPanel HeaderLayout;
        private ComboBox ConfigComboBox;
        private Label LoadedConfigsLabel;
        private Panel VariablesPanel;
        private Label HelpLabel;
    }
}
