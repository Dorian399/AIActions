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
        public CancellationTokenSource CancellationTokenSource;
        public SettingsWindow()
        {
            CancellationTokenSource = new CancellationTokenSource();
            InitializeComponent();
            tableLayoutPanel2.BorderStyle = BorderStyle.FixedSingle;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void SettingsWindow_FormClosing(object sender, EventArgs e)
        {
            await CancellationTokenSource.CancelAsync();
            ConfigLoader loader = new ConfigLoader();
            ParsedConfig? parsedConfig = await loader.LoadFromAppSettings();
            Program.MainWindow.UpdateData(configFile: parsedConfig);
        }
    }
}
