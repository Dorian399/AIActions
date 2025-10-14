namespace AIActions.Windows
{
    partial class SettingsWindow
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
            tableLayoutPanel1 = new TableLayoutPanel();
            tabControl1 = new TabControl();
            Configs = new TabPage();
            Python = new TabPage();
            Updates = new TabPage();
            updateChecker1 = new AIActions.Windows.SettingsControls.UpdateChecker();
            tableLayoutPanel2 = new TableLayoutPanel();
            settingsTabButton1 = new AIActions.Windows.SettingsControls.SettingsTabButton();
            settingsTabButton2 = new AIActions.Windows.SettingsControls.SettingsTabButton();
            settingsTabButton3 = new AIActions.Windows.SettingsControls.SettingsTabButton();
            closeButton = new Button();
            tableLayoutPanel1.SuspendLayout();
            tabControl1.SuspendLayout();
            Updates.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.75F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 81.25F));
            tableLayoutPanel1.Controls.Add(tabControl1, 1, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(closeButton, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.Size = new Size(800, 450);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.Controls.Add(Configs);
            tabControl1.Controls.Add(Python);
            tabControl1.Controls.Add(Updates);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("Segoe UI", 1F);
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.Location = new Point(150, 0);
            tabControl1.Margin = new Padding(0);
            tabControl1.Multiline = true;
            tabControl1.Name = "tabControl1";
            tabControl1.Padding = new Point(0, 0);
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(650, 410);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.TabIndex = 1;
            // 
            // Configs
            // 
            Configs.Location = new Point(4, 5);
            Configs.Margin = new Padding(0);
            Configs.Name = "Configs";
            Configs.Size = new Size(642, 401);
            Configs.TabIndex = 0;
            Configs.Text = "Configs";
            Configs.UseVisualStyleBackColor = true;
            // 
            // Python
            // 
            Python.Location = new Point(4, 5);
            Python.Name = "Python";
            Python.Padding = new Padding(3);
            Python.Size = new Size(642, 401);
            Python.TabIndex = 1;
            Python.Text = "Python";
            Python.UseVisualStyleBackColor = true;
            // 
            // Updates
            // 
            Updates.Controls.Add(updateChecker1);
            Updates.Location = new Point(4, 5);
            Updates.Name = "Updates";
            Updates.Size = new Size(642, 401);
            Updates.TabIndex = 2;
            Updates.Text = "Updates";
            Updates.UseVisualStyleBackColor = true;
            // 
            // updateChecker1
            // 
            updateChecker1.Dock = DockStyle.Fill;
            updateChecker1.Location = new Point(0, 0);
            updateChecker1.Name = "updateChecker1";
            updateChecker1.Size = new Size(642, 401);
            updateChecker1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = SystemColors.Window;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(settingsTabButton1, 0, 0);
            tableLayoutPanel2.Controls.Add(settingsTabButton2, 0, 1);
            tableLayoutPanel2.Controls.Add(settingsTabButton3, 0, 2);
            tableLayoutPanel2.Dock = DockStyle.Top;
            tableLayoutPanel2.Location = new Point(6, 6);
            tableLayoutPanel2.Margin = new Padding(6, 6, 3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.Padding = new Padding(0, 1, 0, 0);
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(141, 286);
            tableLayoutPanel2.TabIndex = 2;
            // 
            // settingsTabButton1
            // 
            settingsTabButton1.ActiveBackgroundColor = SystemColors.GradientInactiveCaption;
            settingsTabButton1.BackColor = SystemColors.GradientInactiveCaption;
            settingsTabButton1.BorderColor = SystemColors.Highlight;
            settingsTabButton1.Dock = DockStyle.Top;
            settingsTabButton1.HoverBackgroundColor = Color.FromArgb(223, 236, 249);
            settingsTabButton1.ImageMargin = new Padding(2);
            settingsTabButton1.LabelText = "Configs";
            settingsTabButton1.Location = new Point(1, 1);
            settingsTabButton1.Margin = new Padding(1, 0, 1, 2);
            settingsTabButton1.Name = "settingsTabButton1";
            settingsTabButton1.Size = new Size(139, 43);
            settingsTabButton1.TabControl = tabControl1;
            settingsTabButton1.TabIndex = 0;
            // 
            // settingsTabButton2
            // 
            settingsTabButton2.ActiveBackgroundColor = SystemColors.GradientInactiveCaption;
            settingsTabButton2.BackColor = SystemColors.Window;
            settingsTabButton2.BorderColor = SystemColors.Highlight;
            settingsTabButton2.Dock = DockStyle.Top;
            settingsTabButton2.HoverBackgroundColor = Color.FromArgb(223, 236, 249);
            settingsTabButton2.ImageMargin = new Padding(2);
            settingsTabButton2.LabelText = "Python";
            settingsTabButton2.Location = new Point(1, 46);
            settingsTabButton2.Margin = new Padding(1, 0, 1, 2);
            settingsTabButton2.Name = "settingsTabButton2";
            settingsTabButton2.Size = new Size(139, 43);
            settingsTabButton2.TabControl = tabControl1;
            settingsTabButton2.TabIndex = 1;
            // 
            // settingsTabButton3
            // 
            settingsTabButton3.ActiveBackgroundColor = SystemColors.GradientInactiveCaption;
            settingsTabButton3.BackColor = SystemColors.Window;
            settingsTabButton3.BorderColor = SystemColors.Highlight;
            settingsTabButton3.Dock = DockStyle.Top;
            settingsTabButton3.HoverBackgroundColor = Color.FromArgb(223, 236, 249);
            settingsTabButton3.ImageMargin = new Padding(2);
            settingsTabButton3.LabelText = "Updates";
            settingsTabButton3.Location = new Point(1, 91);
            settingsTabButton3.Margin = new Padding(1, 0, 1, 2);
            settingsTabButton3.Name = "settingsTabButton3";
            settingsTabButton3.Size = new Size(139, 43);
            settingsTabButton3.TabControl = tabControl1;
            settingsTabButton3.TabIndex = 2;
            // 
            // closeButton
            // 
            closeButton.Dock = DockStyle.Right;
            closeButton.Font = new Font("Segoe UI", 10F);
            closeButton.Location = new Point(700, 413);
            closeButton.Margin = new Padding(3, 3, 6, 6);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(94, 31);
            closeButton.TabIndex = 3;
            closeButton.Text = "Close";
            closeButton.UseVisualStyleBackColor = true;
            // 
            // SettingsWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel1);
            Icon = Properties.Resources.Icon;
            Name = "SettingsWindow";
            Text = "Settings";
            tableLayoutPanel1.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            Updates.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TabControl tabControl1;
        private TabPage Configs;
        private TabPage Python;
        private TabPage Updates;
        private SettingsControls.UpdateChecker updateChecker1;
        private TableLayoutPanel tableLayoutPanel2;
        private SettingsControls.SettingsTabButton settingsTabButton1;
        private SettingsControls.SettingsTabButton settingsTabButton2;
        private SettingsControls.SettingsTabButton settingsTabButton3;
        private Button closeButton;
    }
}