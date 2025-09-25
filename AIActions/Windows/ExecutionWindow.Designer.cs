namespace AIActions
{
    partial class ExecutionWindow
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
            TabsWindow = new TabControl();
            tabPage1 = new TabPage();
            ShowCodeButton = new TextPreviewButton();
            Cancel = new Button();
            Confirm = new TextPreviewButton();
            InfoLabel = new Label();
            tabPage2 = new TabPage();
            STDOut = new RichTextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            CurPromptLabel = new Label();
            TabsWindow.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // TabsWindow
            // 
            TabsWindow.Appearance = TabAppearance.FlatButtons;
            TabsWindow.Controls.Add(tabPage1);
            TabsWindow.Controls.Add(tabPage2);
            TabsWindow.Dock = DockStyle.Fill;
            TabsWindow.ItemSize = new Size(0, 1);
            TabsWindow.Location = new Point(0, 27);
            TabsWindow.Margin = new Padding(0);
            TabsWindow.Name = "TabsWindow";
            TabsWindow.Padding = new Point(0, 0);
            TabsWindow.SelectedIndex = 0;
            TabsWindow.Size = new Size(800, 164);
            TabsWindow.SizeMode = TabSizeMode.Fixed;
            TabsWindow.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(ShowCodeButton);
            tabPage1.Controls.Add(Cancel);
            tabPage1.Controls.Add(Confirm);
            tabPage1.Controls.Add(InfoLabel);
            tabPage1.Location = new Point(4, 5);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(792, 155);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // ShowCodeButton
            // 
            ShowCodeButton.Enabled = false;
            ShowCodeButton.Location = new Point(605, 127);
            ShowCodeButton.Name = "ShowCodeButton";
            ShowCodeButton.Size = new Size(103, 28);
            ShowCodeButton.TabIndex = 3;
            ShowCodeButton.Text = "Show code";
            ShowCodeButton.UseVisualStyleBackColor = true;
            // 
            // Cancel
            // 
            Cancel.Location = new Point(521, 127);
            Cancel.Name = "Cancel";
            Cancel.Size = new Size(78, 28);
            Cancel.TabIndex = 2;
            Cancel.Text = "Cancel";
            Cancel.UseVisualStyleBackColor = true;
            Cancel.Click += ExecutionWindow_CancelClicked;
            // 
            // Confirm
            // 
            Confirm.Enabled = false;
            Confirm.Location = new Point(714, 127);
            Confirm.Name = "Confirm";
            Confirm.Size = new Size(78, 28);
            Confirm.TabIndex = 1;
            Confirm.Text = "Confirm";
            Confirm.UseVisualStyleBackColor = true;
            Confirm.Click += ExecutionWindow_ConfirmClicked;
            // 
            // InfoLabel
            // 
            InfoLabel.Dock = DockStyle.Fill;
            InfoLabel.Font = new Font("Segoe UI", 13F);
            InfoLabel.Location = new Point(3, 3);
            InfoLabel.Name = "InfoLabel";
            InfoLabel.Size = new Size(786, 149);
            InfoLabel.TabIndex = 0;
            InfoLabel.Text = "Creating prompt...";
            InfoLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(STDOut);
            tabPage2.Location = new Point(4, 5);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(792, 155);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // STDOut
            // 
            STDOut.BackColor = SystemColors.ControlLight;
            STDOut.Dock = DockStyle.Fill;
            STDOut.Location = new Point(3, 3);
            STDOut.Name = "STDOut";
            STDOut.ReadOnly = true;
            STDOut.Size = new Size(786, 149);
            STDOut.TabIndex = 0;
            STDOut.Text = "";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(TabsWindow, 0, 1);
            tableLayoutPanel1.Controls.Add(CurPromptLabel, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.5695362F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 85.4304657F));
            tableLayoutPanel1.Size = new Size(800, 191);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // CurPromptLabel
            // 
            CurPromptLabel.AutoSize = true;
            CurPromptLabel.Dock = DockStyle.Fill;
            CurPromptLabel.Font = new Font("Segoe UI", 9F);
            CurPromptLabel.Location = new Point(3, 0);
            CurPromptLabel.Name = "CurPromptLabel";
            CurPromptLabel.Size = new Size(794, 27);
            CurPromptLabel.TabIndex = 1;
            CurPromptLabel.Text = "Current prompt: ";
            // 
            // ExecutionWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 191);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "ExecutionWindow";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Executing...";
            FormClosed += ExecutionWindow_FormClosed;
            Load += ExecutionWindow_Load;
            TabsWindow.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl TabsWindow;
        private TabPage tabPage1;
        private Label InfoLabel;
        private TabPage tabPage2;
        private Button Cancel;
        private TextPreviewButton Confirm;
        private TableLayoutPanel tableLayoutPanel1;
        private Label CurPromptLabel;
        private RichTextBox STDOut;
        private TextPreviewButton ShowCodeButton;
    }
}