namespace AIActions.Windows.SettingsControls
{
    partial class PythonSettings
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
            tableLayoutPanel1 = new TableLayoutPanel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            pipSizeLabel = new Label();
            removeAllButton = new Button();
            removeSelectedButton = new Button();
            groupBox1 = new GroupBox();
            pipPackages = new CheckedListBox();
            groupBox2 = new GroupBox();
            STDOut = new RichTextBox();
            flowLayoutPanel2 = new FlowLayoutPanel();
            pythonVersionLabel = new Label();
            pythonScriptsAmountLabel = new Label();
            pythonHistoryButton = new Button();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45.9459457F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 54.0540543F));
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 0);
            tableLayoutPanel1.Controls.Add(groupBox1, 0, 1);
            tableLayoutPanel1.Controls.Add(groupBox2, 1, 1);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel2, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 99F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(555, 301);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(pipSizeLabel);
            flowLayoutPanel1.Controls.Add(removeAllButton);
            flowLayoutPanel1.Controls.Add(removeSelectedButton);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Margin = new Padding(0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(255, 99);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // pipSizeLabel
            // 
            pipSizeLabel.AutoSize = true;
            pipSizeLabel.Dock = DockStyle.Left;
            pipSizeLabel.Location = new Point(3, 0);
            pipSizeLabel.Name = "pipSizeLabel";
            pipSizeLabel.Size = new Size(132, 20);
            pipSizeLabel.TabIndex = 0;
            pipSizeLabel.Text = "Pip packages size: ";
            // 
            // removeAllButton
            // 
            removeAllButton.Font = new Font("Segoe UI", 8F);
            removeAllButton.Location = new Point(3, 23);
            removeAllButton.Name = "removeAllButton";
            removeAllButton.Size = new Size(148, 29);
            removeAllButton.TabIndex = 1;
            removeAllButton.Text = "Remove all packages";
            removeAllButton.UseVisualStyleBackColor = true;
            // 
            // removeSelectedButton
            // 
            removeSelectedButton.Font = new Font("Segoe UI", 8F);
            removeSelectedButton.Location = new Point(3, 58);
            removeSelectedButton.Name = "removeSelectedButton";
            removeSelectedButton.Size = new Size(186, 29);
            removeSelectedButton.TabIndex = 2;
            removeSelectedButton.Text = "Remove selected packages";
            removeSelectedButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(pipPackages);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(3, 102);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(249, 196);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Pip packages";
            // 
            // pipPackages
            // 
            pipPackages.CheckOnClick = true;
            pipPackages.Dock = DockStyle.Fill;
            pipPackages.FormattingEnabled = true;
            pipPackages.Items.AddRange(new object[] { "test1", "test2", "test3", "test4", "test5" });
            pipPackages.Location = new Point(3, 23);
            pipPackages.Name = "pipPackages";
            pipPackages.Size = new Size(243, 170);
            pipPackages.Sorted = true;
            pipPackages.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(STDOut);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(258, 102);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 3, 3, 6);
            groupBox2.Size = new Size(294, 196);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Output window";
            // 
            // STDOut
            // 
            STDOut.Dock = DockStyle.Fill;
            STDOut.Location = new Point(3, 23);
            STDOut.Name = "STDOut";
            STDOut.ReadOnly = true;
            STDOut.Size = new Size(288, 167);
            STDOut.TabIndex = 0;
            STDOut.Text = "";
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(pythonVersionLabel);
            flowLayoutPanel2.Controls.Add(pythonScriptsAmountLabel);
            flowLayoutPanel2.Controls.Add(pythonHistoryButton);
            flowLayoutPanel2.Dock = DockStyle.Fill;
            flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel2.Location = new Point(255, 0);
            flowLayoutPanel2.Margin = new Padding(0);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(300, 99);
            flowLayoutPanel2.TabIndex = 3;
            // 
            // pythonVersionLabel
            // 
            pythonVersionLabel.Dock = DockStyle.Left;
            pythonVersionLabel.Location = new Point(3, 0);
            pythonVersionLabel.Margin = new Padding(3, 0, 3, 3);
            pythonVersionLabel.Name = "pythonVersionLabel";
            pythonVersionLabel.Size = new Size(294, 20);
            pythonVersionLabel.TabIndex = 0;
            pythonVersionLabel.Text = "Python version: ";
            // 
            // pythonScriptsAmountLabel
            // 
            pythonScriptsAmountLabel.Location = new Point(3, 23);
            pythonScriptsAmountLabel.Margin = new Padding(3, 0, 3, 3);
            pythonScriptsAmountLabel.Name = "pythonScriptsAmountLabel";
            pythonScriptsAmountLabel.Size = new Size(297, 20);
            pythonScriptsAmountLabel.TabIndex = 1;
            pythonScriptsAmountLabel.Text = "Scripts generated: ";
            // 
            // pythonHistoryButton
            // 
            pythonHistoryButton.Font = new Font("Segoe UI", 8F);
            pythonHistoryButton.Location = new Point(3, 49);
            pythonHistoryButton.Name = "pythonHistoryButton";
            pythonHistoryButton.Size = new Size(140, 29);
            pythonHistoryButton.TabIndex = 2;
            pythonHistoryButton.Text = "Show scripts history";
            pythonHistoryButton.UseVisualStyleBackColor = true;
            // 
            // PythonSettings
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "PythonSettings";
            Size = new Size(555, 301);
            Load += PythonSettings_Load;
            tableLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            flowLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label pipSizeLabel;
        private Button removeAllButton;
        private Button removeSelectedButton;
        private GroupBox groupBox1;
        private CheckedListBox pipPackages;
        private GroupBox groupBox2;
        private RichTextBox STDOut;
        private FlowLayoutPanel flowLayoutPanel2;
        private Label pythonVersionLabel;
        private Label pythonScriptsAmountLabel;
        private Button pythonHistoryButton;
    }
}
