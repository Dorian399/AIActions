using AIActions.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIActions.Windows.SettingsControls
{
    public partial class UpdateChecker : UserControl
    {
        private string? _updateUrl;
        public UpdateChecker()
        {
            InitializeComponent();
        }

        private async void Updater_OnLoad(object sender, EventArgs e)
        {
            (bool, string) result = await Updater.GetUpdateStatusAsync();
            if (result.Item1)
            {
                updateLabel.Text = "New version is available.";
                _updateUrl = result.Item2;
                updateButton.Enabled = true;
            }
            else
            {
                updateLabel.Text = result.Item2;
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_updateUrl))
                return;
            if(Uri.TryCreate(_updateUrl, UriKind.RelativeOrAbsolute, out Uri? url))
            {
                if (url == null)
                    return;
                if (url.Scheme != Uri.UriSchemeHttps && url.Host != "github.com")
                    return;

                Process.Start(new ProcessStartInfo { FileName = url.AbsoluteUri, UseShellExecute = true });
            }
        }
    }
}
