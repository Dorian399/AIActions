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
            Cancel = new Button();
            Confirm = new Button();
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
            TabsWindow.ItemSize = new Size(10, 20);
            TabsWindow.Location = new Point(0, 22);
            TabsWindow.Margin = new Padding(0);
            TabsWindow.Name = "TabsWindow";
            TabsWindow.Padding = new Point(0, 0);
            TabsWindow.SelectedIndex = 0;
            TabsWindow.Size = new Size(800, 129);
            TabsWindow.SizeMode = TabSizeMode.Fixed;
            TabsWindow.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(Cancel);
            tabPage1.Controls.Add(Confirm);
            tabPage1.Controls.Add(InfoLabel);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(792, 101);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // Cancel
            // 
            Cancel.Location = new Point(630, 90);
            Cancel.Name = "Cancel";
            Cancel.Size = new Size(78, 28);
            Cancel.TabIndex = 2;
            Cancel.Text = "Cancel";
            Cancel.UseVisualStyleBackColor = true;
            // 
            // Confirm
            // 
            Confirm.Location = new Point(714, 90);
            Confirm.Name = "Confirm";
            Confirm.Size = new Size(78, 28);
            Confirm.TabIndex = 1;
            Confirm.Text = "Confirm";
            Confirm.UseVisualStyleBackColor = true;
            // 
            // InfoLabel
            // 
            InfoLabel.Dock = DockStyle.Fill;
            InfoLabel.Font = new Font("Segoe UI", 13F);
            InfoLabel.Location = new Point(3, 3);
            InfoLabel.Name = "InfoLabel";
            InfoLabel.Size = new Size(786, 95);
            InfoLabel.TabIndex = 0;
            InfoLabel.Text = "Sending request...";
            InfoLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(STDOut);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(792, 101);
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
            STDOut.Size = new Size(786, 95);
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
            tableLayoutPanel1.Size = new Size(800, 151);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // CurPromptLabel
            // 
            CurPromptLabel.AutoSize = true;
            CurPromptLabel.Dock = DockStyle.Fill;
            CurPromptLabel.Font = new Font("Segoe UI", 9F);
            CurPromptLabel.Location = new Point(3, 0);
            CurPromptLabel.Name = "CurPromptLabel";
            CurPromptLabel.Size = new Size(794, 22);
            CurPromptLabel.TabIndex = 1;
            CurPromptLabel.Text = "Current prompt: ";
            // 
            // ExecutionWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 151);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "ExecutionWindow";
            Text = "Executing...";
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
        private Button Confirm;
        private TableLayoutPanel tableLayoutPanel1;
        private Label CurPromptLabel;
        private RichTextBox STDOut;
    }
}