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
            tableLayoutPanel1 = new TableLayoutPanel();
            configsList1 = new AIActions.Windows.ConfigWindows.ConfigsList();
            confirmButton = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(configsList1, 0, 0);
            tableLayoutPanel1.Controls.Add(confirmButton, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel1.Size = new Size(594, 333);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // configsList1
            // 
            configsList1.Location = new Point(3, 3);
            configsList1.Name = "configsList1";
            configsList1.Size = new Size(588, 282);
            configsList1.TabIndex = 0;
            configsList1.OnConfigChosen = ConfigChosen;
            // 
            // confirmButton
            // 
            confirmButton.Dock = DockStyle.Right;
            confirmButton.Enabled = false;
            confirmButton.Location = new Point(494, 294);
            confirmButton.Margin = new Padding(6);
            confirmButton.Name = "confirmButton";
            confirmButton.Padding = new Padding(3);
            confirmButton.Size = new Size(94, 33);
            confirmButton.TabIndex = 1;
            confirmButton.Text = "Confirm";
            confirmButton.UseVisualStyleBackColor = true;
            confirmButton.Click += ConfirmButton_Clicked;
            // 
            // ConfigsListWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(594, 333);
            Controls.Add(tableLayoutPanel1);
            MinimumSize = new Size(552, 275);
            Name = "ConfigsListWindow";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Config Setup";
            FormClosed += ConfigsListWindow_Closed;
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private ConfigWindows.ConfigsList configsList1;
        private Button confirmButton;
    }
}