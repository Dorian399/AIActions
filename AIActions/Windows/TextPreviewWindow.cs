using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIActions.Windows
{
    public partial class TextPreviewWindow : Form
    {
        public TextPreviewWindow(string text,string title)
        {
            InitializeComponent();
            if (!String.IsNullOrWhiteSpace(title))
                this.Text = title;
            TextPreview.Text = text;
        }
    }
}
