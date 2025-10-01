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

        public UserVarInput()
        {
            InitializeComponent();
            _inputOrigWidth = varInput.Width;
        }

        private void LayoutResized(object sender, EventArgs e)
        {
            VarInput_TextChanged(sender, e);
        }

        private void VarInput_TextChanged(object sender, EventArgs e)
        {
            Size textSize = TextRenderer.MeasureText(varInput.Text, varInput.Font);

            int min = _inputOrigWidth;
            int max = varInput.Parent.ClientSize.Width - 130;

            if (min > max)
                return;

            int inputWidth = Math.Clamp(textSize.Width + 5, min, max);


            varInput.Width = inputWidth;
        }
    }
}
