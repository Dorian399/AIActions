using AIActions.ExternalData;
using AIActions.Python;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIActions.Windows.SettingsControls
{
    public partial class PythonSettings : UserControl
    {
        public PythonSettings()
        {
            InitializeComponent();
        }

        private static long GetDirectorySize(DirectoryInfo directory)
        {
            if (directory == null || !directory.Exists)
            {
                throw new DirectoryNotFoundException("Directory does not exist.");
            }
            long size = 0;
            try
            {
                size += directory.GetFiles().Sum(file => file.Length);
                size += directory.GetDirectories().Sum(GetDirectorySize);
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"We do not have access to {directory}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"We encountered an error processing {directory}: ");
                Console.WriteLine($"{ex.Message}");
            }
            return size;
        }

        private void UpdatePipSizeLabel()
        {
            string pipSizeText = pipSizeLabel.Text;
            pipSizeLabel.Text = pipSizeText + "Calculating...";
            try
            {
                long dirSize = GetDirectorySize(new DirectoryInfo(Paths.PipPackagesFolder));
                float readableSize;

                if (dirSize < 1024)
                {
                    pipSizeLabel.Text = pipSizeText + dirSize.ToString() + " Bytes";
                }
                else if (dirSize < 1024 * 1024)
                {
                    pipSizeLabel.Text = pipSizeText + (dirSize / 1024.0).ToString("F2") + " KB";
                }
                else if (dirSize < 1024 * 1024 * 1024)
                {
                    pipSizeLabel.Text = pipSizeText + (dirSize / (1024.0 * 1024)).ToString("F2") + " MB";
                }
                else
                {
                    pipSizeLabel.Text = pipSizeText + (dirSize / (1024.0 * 1024 * 1024)).ToString("F2") + " GB";
                }
            }
            catch (Exception ex)
            {
                pipSizeLabel.Text = pipSizeText + "0 MB";
            }
        }

        private void PythonSettings_Load(object sender, EventArgs e)
        {
            CancellationToken token = default;
            SettingsWindow? settingsWindow = (SettingsWindow?)FindForm();
            if(settingsWindow != null && settingsWindow.CancellationTokenSource != null)
                token = settingsWindow.CancellationTokenSource.Token;

            UpdatePipSizeLabel();

            PythonInfo.GetPythonVersion(token).ContinueWith(t => { pythonVersionLabel.Text += t.Result; }, TaskContinuationOptions.ExecuteSynchronously);

        }


    }
}
