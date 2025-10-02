using AIActions.Configs;
using AIActions.UserData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIActions.Windows.ConfigWindows.ConfigFilesSelector
{
    public partial class UserVarInput : UserControl
    {
        private int _inputOrigWidth;
        private string _configCodename;
        private string _variableName;

        public UserVarInput(string configCodename, string variableName)
        {
            InitializeComponent();
            _inputOrigWidth = varInput.Width;
            _configCodename = configCodename;
            _variableName = variableName;
            _configCodename = configCodename;
            varName.Text = (string.IsNullOrWhiteSpace(_variableName) ? "Invalid variable name" : _variableName) + ":";

            string savedVarText = AppSettings.GetConfigVariable(_configCodename, variableName);

            if (!string.IsNullOrWhiteSpace(savedVarText)) {
                varInput.Text = savedVarText;
            }

            varInput.TextChanged += VarInput_TextChanged;
        }

        private void LayoutResized(object sender, EventArgs e)
        {
            VarInput_AdjustWidth();
        }

        private void VarInput_AdjustWidth()
        {
            Size textSize = TextRenderer.MeasureText(varInput.Text, varInput.Font);

            int min = _inputOrigWidth;
            int max = varInput.Parent.ClientSize.Width - 130;

            if (min > max)
                return;

            int inputWidth = Math.Clamp(textSize.Width + 5, min, max);


            varInput.Width = inputWidth;
        }

        private void VarInput_TextChanged(object sender, EventArgs e)
        {
            VarInput_AdjustWidth();
            AppSettings.SetConfigVariable(_configCodename, _variableName, varInput.Text);
        }
    }
}
