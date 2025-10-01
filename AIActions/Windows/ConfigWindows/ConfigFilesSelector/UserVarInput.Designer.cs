namespace AIActions.Windows.ConfigWindows.ConfigFilesSelector
{
    partial class UserVarInput
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
            varLayout = new TableLayoutPanel();
            varName = new Label();
            varInput = new TextBox();
            varLayout.SuspendLayout();
            SuspendLayout();
            // 
            // VarLayout
            // 
            varLayout.ColumnCount = 2;
            varLayout.ColumnStyles.Add(new ColumnStyle());
            varLayout.ColumnStyles.Add(new ColumnStyle());
            varLayout.Controls.Add(varName, 0, 0);
            varLayout.Controls.Add(varInput, 1, 0);
            varLayout.Dock = DockStyle.Fill;
            varLayout.Location = new Point(0, 0);
            varLayout.Margin = new Padding(0);
            varLayout.Name = "VarLayout";
            varLayout.RowCount = 1;
            varLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            varLayout.Size = new Size(196, 35);
            varLayout.TabIndex = 0;
            // 
            // VarName
            // 
            varName.AutoSize = true;
            varName.Dock = DockStyle.Fill;
            varName.Font = new Font("Segoe UI", 10F);
            varName.Location = new Point(3, 0);
            varName.Name = "VarName";
            varName.Size = new Size(100, 35);
            varName.TabIndex = 0;
            varName.Text = "VAR_NAME:";
            varName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // VarInput
            // 
            varInput.Anchor = AnchorStyles.Left;
            varInput.Location = new Point(109, 4);
            varInput.Name = "VarInput";
            varInput.Size = new Size(78, 27);
            varInput.TabIndex = 1;
            varInput.TextChanged += VarInput_TextChanged;
            // 
            // UserVarInput
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(varLayout);
            Name = "UserVarInput";
            Size = new Size(196, 35);
            varLayout.SizeChanged += LayoutResized;
            varLayout.ResumeLayout(false);
            varLayout.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel varLayout;
        private Label varName;
        private TextBox varInput;
    }
}
