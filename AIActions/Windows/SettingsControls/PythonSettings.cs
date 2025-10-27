using AIActions.ExternalData;
using AIActions.Python;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace AIActions.Windows.SettingsControls
{
    public partial class PythonSettings : UserControl
    {

        private enum Packages{
            Selected,
            All,
        }

        private CancellationToken _cancellationToken = default;

        public PythonSettings()
        {
            InitializeComponent();
        }

        private long GetDirectorySize(DirectoryInfo directory)
        {
            if (directory == null || !directory.Exists)
            {
                throw new DirectoryNotFoundException("Directory does not exist.");
            }
            long size = 0;
            size += directory.GetFiles().Sum(file => file.Length);
            size += directory.GetDirectories().Sum(GetDirectorySize);
            return size;
        }

        private int GetDirectoryFileCount(DirectoryInfo directory)
        {
            if (directory == null || !directory.Exists)
            {
                throw new DirectoryNotFoundException("Directory does not exist.");
            }
            int count = directory.GetFiles().Count();
            return count;
        }

        private async void RemovePipPackages(Packages removeType,CancellationToken token=default)
        {
            List<string> packages = [];
            if(removeType == Packages.All)
            {
                foreach (string package in pipPackages.Items)
                {
                    packages.Add(package);
                }
            }
            else
            {
                foreach(string package in pipPackages.CheckedItems)
                {
                    packages.Add(package);
                }
            }

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to remove {packages.Count} package{(packages.Count == 1 ? "" : "s")}?",
                "Confirm Removal",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.No)
                return;

            removeSelectedButton.Enabled = false;
            removeAllButton.Enabled = false;
            pipPackages.Enabled = false;


            PackageManager.OnOutput += text =>
            {
                if (STDOut != null && !STDOut.IsDisposed)
                    STDOut.AppendText(text);
            };

            bool executedProperly = false;

            try
            {
               executedProperly = await PackageManager.RemovePackages(packages,this,token);
            }
            catch
            {
                executedProperly = false;
            }

            if (executedProperly) {
                if (STDOut != null && !STDOut.IsDisposed)
                    STDOut.AppendText($"Sucessfully removed {packages.Count} package{(packages.Count == 1 ? "" : "s")}.\n\n");
            }
            else
            {
                if(STDOut != null && !STDOut.IsDisposed)
                    STDOut.AppendText($"Failed to remove some/all packages.\n\n");
            }

            List<string> newPackageList = await PackageManager.GetPackagesAsync();

            await Task.Delay(500);

            UpdatePipPackagesList(newPackageList);
            UpdatePipSizeLabel();

            removeAllButton.Enabled = true;
            pipPackages.Enabled = true;

        }

        private void UpdatePipPackagesList(List<string> packages)
        {
            pipPackages.Items.Clear();
            foreach (string package in packages)
            {
                pipPackages.Items.Add(package);
            }
            pipPackages.Enabled = true;
        }

        private void UpdatePipSizeLabel()
        {
            string pipSizeText = "Pip packages size: ";
            pipSizeLabel.Text = pipSizeText + "Calculating...";
            try
            {
                long dirSize = GetDirectorySize(new DirectoryInfo(Paths.PipPackagesFolder));

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
            catch
            {
                pipSizeLabel.Text = pipSizeText + "0 MB";
            }
        }

        private void PipPackages_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int count = pipPackages.CheckedItems.Count;

            if (e.NewValue == CheckState.Checked)
                count++;
            else
                count--;

            removeSelectedButton.Enabled = count > 0;
        }

        private void RemoveSelectedButton_Click(object sender, EventArgs e)
        {
            RemovePipPackages(Packages.Selected, _cancellationToken);
        }

        private void RemoveAllButton_Click(object sender, EventArgs e)
        {
            RemovePipPackages(Packages.All, _cancellationToken);
        }

        private void PythonSettings_Load(object sender, EventArgs e)
        {
            SettingsWindow? settingsWindow = (SettingsWindow?)FindForm();
            if(settingsWindow != null && settingsWindow.CancellationTokenSource != null)
                _cancellationToken = settingsWindow.CancellationTokenSource.Token;

            UpdatePipSizeLabel();

            PythonInfo.GetPythonVersion(_cancellationToken).ContinueWith(t => { pythonVersionLabel.Text += t.Result; }, TaskContinuationOptions.ExecuteSynchronously);

            // Show amount of scripts generated, and enables the history button if >0.
            try
            {
                int scriptsHistoryCount = GetDirectoryFileCount(new DirectoryInfo(Paths.PythonScriptsFolder));
                if (scriptsHistoryCount > 0) {
                    pythonHistoryButton.Enabled = true;
                }
                pythonScriptsAmountLabel.Text += scriptsHistoryCount.ToString();
            }
            catch
            {
                pythonScriptsAmountLabel.Text += "0";
            }

            // Add pip packages to checkbox list.
            try
            {
                PackageManager.GetPackagesAsync(_cancellationToken).ContinueWith(t => {
                    UpdatePipPackagesList(t.Result);
                    if(pipPackages.Items.Count > 0)
                        removeAllButton.Enabled = true;
                }, TaskContinuationOptions.ExecuteSynchronously);
            }
            catch
            {
                pipPackages.Items.Clear();
                pipPackages.Items.Add("Failed to retrieve pip packages.");
            }


        }


    }
}
