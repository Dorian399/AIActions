using AIActions.Configs;
using AIActions.ExternalData;
using AIActions.UserData;
using AIActions.Windows.ConfigWindows.ConfigFilesSelector;
using Json.Path;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIActions.Windows.ConfigWindows
{
    public partial class ConfigsList : UserControl
    {
        private Dictionary<string, ParsedConfig> _configsCache = new Dictionary<string, ParsedConfig>();
        private Dictionary<int, ParsedConfig> _loadedConfigs = new Dictionary<int, ParsedConfig>();

        public Action? OnConfigChosen { get; set; }

        public ConfigsList()
        {
            InitializeComponent();
        }

        private async void ConfigImportDialog()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = "c:\\";
                ofd.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                ofd.RestoreDirectory = true;

                if (ofd.ShowDialog() != DialogResult.OK)
                {
                    ConfigsList_Loaded(this, new EventArgs());
                    return;
                }

                string filePath = ofd.FileName;

                bool configValid = false;

                ConfigLoader loader = new ConfigLoader();
                ParsedConfig? config = await loader.LoadFromFile(filePath,configValidation: true);

                if (config != null)
                    configValid = true;

                if (!configValid)
                {
                    MessageBox.Show("Failed to import config file.");
                    ConfigsList_Loaded(this, new EventArgs());
                    return;
                }

                string importFilePath = Path.Combine(Paths.UserConfigFilesFolder, Path.GetFileName(filePath));

                if (!Directory.Exists(Paths.UserConfigFilesFolder))
                    Directory.CreateDirectory(Paths.UserConfigFilesFolder);

                if (File.Exists(importFilePath))
                {
                    DialogResult replaceDialog = MessageBox.Show(
                        "This config already exists. Do you want to replace it?",
                        "AI Actions",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (replaceDialog != DialogResult.Yes)
                    {
                        ConfigsList_Loaded(this, new EventArgs());
                        return;
                    }
                    try
                    {
                        File.Delete(importFilePath);
                    }
                    catch (Exception ex) {
                        MessageBox.Show("Failed to replace config file, existing config will be used.\nError:\n" + ex.Message);
                    }
                }

                try
                {
                    File.Copy(filePath, importFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to import config file, error:\n" + ex.Message);
                    ConfigsList_Loaded(this, new EventArgs());
                    return;
                }

                MessageBox.Show("Config file imported sucessfully.", "AI Actions", MessageBoxButtons.OK, MessageBoxIcon.Information);

            };
            _configsCache.Clear();
            ConfigsList_Loaded(this, new EventArgs());
        }

        private void ComboBox_IndexChanged(object sender, EventArgs e)
        {
            VariablesPanel.Controls.Clear();

            int selectedIndex = ConfigComboBox.SelectedIndex;

            // Import logic, only runs when import option is selected.
            if(ConfigComboBox.Items.IndexOf("Import config file...") == selectedIndex)
            {
                ConfigImportDialog();
                return;
            }

            ParsedConfig selectedConfig = _loadedConfigs[selectedIndex];
            if (selectedConfig == null)
                return;
            if(selectedConfig.UserVariables == null || selectedConfig.UserVariables.Length <= 0)
            {
                Label emptyLabel = new Label();
                emptyLabel.Text = "No variables available.";
                emptyLabel.Dock = DockStyle.Top;
                emptyLabel.TextAlign = ContentAlignment.MiddleCenter;

                VariablesPanel.Controls.Add(emptyLabel);
                return;
            }

            AppSettings.SetCurrentConfig(selectedConfig.Codename);
            OnConfigChosen?.Invoke();

            foreach (string var in selectedConfig.UserVariables)
            {
                UserVarInput varInput = new UserVarInput(selectedConfig.Codename,var);
                varInput.Parent = VariablesPanel;
                varInput.Dock = DockStyle.Top;
            }

        }

        private async void ConfigsList_Loaded(object sender, EventArgs e)
        {
            ConfigComboBox.Items.Clear();
            _loadedConfigs = new Dictionary<int, ParsedConfig>();

            SortedDictionary<string, string> configFiles = Paths.ConfigFiles;

            ConfigLoader configLoader = new ConfigLoader();

            string currentConfigSetting = AppSettings.GetCurrentConfig();

            int index = 0;
            foreach (var kvp in configFiles) {
                ParsedConfig? config;

                if (_configsCache.ContainsKey(kvp.Value)) {
                    config = _configsCache[kvp.Value];
                }
                else
                {
                    config = await configLoader.LoadFromFile(kvp.Value);
                }

                if (config == null)
                    continue;
                ConfigComboBox.Items.Insert(index,config.Name);
                _configsCache[kvp.Value] = config;
                _loadedConfigs[index] = config;

                if (config.Codename == currentConfigSetting)
                    ConfigComboBox.SelectedIndex = index;

                index++;
            }

            ConfigComboBox.Items.Insert(index, "Import config file...");

        }
    }
}
