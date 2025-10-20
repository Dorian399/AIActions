using AIActions.Configs;
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
    public partial class SettingsWindow : Form
    {
        public SettingsWindow()
        {
            InitializeComponent();
            tableLayoutPanel2.BorderStyle = BorderStyle.FixedSingle;
        }

        private async void SettingsWindow_FormClosing(object sender, EventArgs e)
        {
            ConfigLoader loader = new ConfigLoader();
            ParsedConfig? parsedConfig = await loader.LoadFromAppSettings();
            Program.MainWindow.UpdateData(configFile: parsedConfig);
        }
    }
}
