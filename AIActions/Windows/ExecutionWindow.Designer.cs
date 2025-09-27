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
            InfoLabel = new Label();
            tabPage2 = new TabPage();
            STDOut = new RichTextBox();
            ShowCodeButton = new TextPreviewButton();
            Cancel = new Button();
            Confirm = new TextPreviewButton();
            tableLayoutPanel1 = new TableLayoutPanel();
            CurPromptLabel = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            progressBar1 = new ProgressBar();
            TabsWindow.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // TabsWindow
            // 
            TabsWindow.Appearance = TabAppearance.FlatButtons;
            TabsWindow.Controls.Add(tabPage1);
            TabsWindow.Controls.Add(tabPage2);
            TabsWindow.Dock = DockStyle.Fill;
            TabsWindow.ItemSize = new Size(0, 1);
            TabsWindow.Location = new Point(0, 20);
            TabsWindow.Margin = new Padding(0);
            TabsWindow.Name = "TabsWindow";
            TabsWindow.Padding = new Point(0, 0);
            TabsWindow.SelectedIndex = 0;
            TabsWindow.Size = new Size(800, 131);
            TabsWindow.SizeMode = TabSizeMode.Fixed;
            TabsWindow.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(InfoLabel);
            tabPage1.Location = new Point(4, 5);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(792, 122);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // InfoLabel
            // 
            InfoLabel.Dock = DockStyle.Fill;
            InfoLabel.Font = new Font("Segoe UI", 13F);
            InfoLabel.Location = new Point(3, 3);
            InfoLabel.Name = "InfoLabel";
            InfoLabel.Size = new Size(786, 116);
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
            tabPage2.Size = new Size(792, 122);
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
            STDOut.Size = new Size(786, 116);
            STDOut.TabIndex = 0;
            STDOut.Text = "";
            // 
            // ShowCodeButton
            // 
            ShowCodeButton.Dock = DockStyle.Right;
            ShowCodeButton.Enabled = false;
            ShowCodeButton.Location = new Point(603, 3);
            ShowCodeButton.Name = "ShowCodeButton";
            ShowCodeButton.Size = new Size(103, 28);
            ShowCodeButton.TabIndex = 3;
            ShowCodeButton.Text = "Show code";
            ShowCodeButton.UseVisualStyleBackColor = true;
            // 
            // Cancel
            // 
            Cancel.Dock = DockStyle.Right;
            Cancel.Location = new Point(517, 3);
            Cancel.Name = "Cancel";
            Cancel.Size = new Size(78, 28);
            Cancel.TabIndex = 2;
            Cancel.Text = "Cancel";
            Cancel.UseVisualStyleBackColor = true;
            Cancel.Click += ExecutionWindow_CancelClicked;
            // 
            // Confirm
            // 
            Confirm.Dock = DockStyle.Right;
            Confirm.Enabled = false;
            Confirm.Location = new Point(713, 3);
            Confirm.Name = "Confirm";
            Confirm.Size = new Size(78, 28);
            Confirm.TabIndex = 1;
            Confirm.Text = "Confirm";
            Confirm.UseVisualStyleBackColor = true;
            Confirm.Click += ExecutionWindow_ConfirmClicked;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(TabsWindow, 0, 1);
            tableLayoutPanel1.Controls.Add(CurPromptLabel, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 2);
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 99.99999F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
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
            CurPromptLabel.Size = new Size(794, 20);
            CurPromptLabel.TabIndex = 1;
            CurPromptLabel.Text = "Current prompt: ";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.ColumnCount = 4;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85.78595F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2140465F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 111F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 84F));
            tableLayoutPanel2.Controls.Add(Cancel, 1, 0);
            tableLayoutPanel2.Controls.Add(ShowCodeButton, 2, 0);
            tableLayoutPanel2.Controls.Add(Confirm, 3, 0);
            tableLayoutPanel2.Controls.Add(progressBar1, 0, 0);
            tableLayoutPanel2.Location = new Point(3, 154);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(794, 34);
            tableLayoutPanel2.TabIndex = 2;
            // 
            // progressBar1
            // 
            progressBar1.Dock = DockStyle.Fill;
            progressBar1.Location = new Point(5, 5);
            progressBar1.Margin = new Padding(5);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(503, 24);
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.TabIndex = 4;
            // 
            // ExecutionWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 191);
            Controls.Add(tableLayoutPanel1);
            Name = "ExecutionWindow";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AI Actions";
            FormClosed += ExecutionWindow_FormClosed;
            Load += ExecutionWindow_Load;
            TabsWindow.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
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
        private ProgressBar progressBar1;
        private TableLayoutPanel tableLayoutPanel2;
    }
}