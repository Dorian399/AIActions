namespace AIActions.Windows.SettingsControls
{
    partial class UpdateChecker
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
            updateLabel = new Label();
            updateButton = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(updateLabel, 0, 0);
            tableLayoutPanel1.Controls.Add(updateButton, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20.289856F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 79.710144F));
            tableLayoutPanel1.Size = new Size(328, 276);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // updateLabel
            // 
            updateLabel.Dock = DockStyle.Fill;
            updateLabel.Font = new Font("Segoe UI", 12F);
            updateLabel.ImageAlign = ContentAlignment.MiddleLeft;
            updateLabel.Location = new Point(6, 0);
            updateLabel.Margin = new Padding(6, 0, 6, 0);
            updateLabel.Name = "updateLabel";
            updateLabel.Size = new Size(316, 56);
            updateLabel.TabIndex = 0;
            updateLabel.Text = "Checking for updates...";
            updateLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // updateButton
            // 
            updateButton.Enabled = false;
            updateButton.Font = new Font("Segoe UI", 10F);
            updateButton.Location = new Point(6, 62);
            updateButton.Margin = new Padding(6);
            updateButton.Name = "updateButton";
            updateButton.Size = new Size(100, 38);
            updateButton.TabIndex = 1;
            updateButton.Text = "Update";
            updateButton.UseVisualStyleBackColor = true;
            updateButton.Click += UpdateButton_Click;
            // 
            // UpdateChecker
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "UpdateChecker";
            Size = new Size(328, 276);
            Load += Updater_OnLoad;
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label updateLabel;
        private Button updateButton;
    }
}
