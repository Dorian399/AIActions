using AIActions.Configs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIActions.Windows
{
    public partial class ConfigsListWindow : Form
    {
        public ConfigsListWindow()
        {
            InitializeComponent();
        }

        private void ConfigChosen()
        {
            confirmButton.Enabled = true;
        }

        private void ConfirmButton_Clicked(object sender, EventArgs e)
        {
            // Confirm button just closes the window as all data is saved dynamically.
            this.Close();
        }

        private async void ConfigsListWindow_FormClosing(object sender, EventArgs e)
        {
            ConfigLoader loader = new ConfigLoader();
            ParsedConfig? parsedConfig = await loader.LoadFromAppSettings();
            Program.MainWindow.UpdateData(configFile: parsedConfig, shouldPopUp: true);
        }
    }
}
