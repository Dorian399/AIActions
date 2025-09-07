using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIActions
{
    public partial class ExecutionWindow : Form
    {
        public PromptWindow ParentWindow;
        private bool SuccessfullyExecuted=false;
        public ExecutionWindow(string Prompt)
        {
            InitializeComponent();
            this.FormClosed += ExecutionWindow_FormClosed;
            if (Prompt != null) {
                CurPromptLabel.Text += Prompt;
            }
        }

        private void ExecutionWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ParentWindow != null)
            {
                if (SuccessfullyExecuted)
                    ParentWindow.Close();
                else
                    ParentWindow.Show();
            }
        }
    }
}
