using AIActions.Configs;
using AIActions.ExternalData;
using AIActions.UserData;
using AIActions.Windows.ConfigWindows.ConfigFilesSelector;
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

namespace AIActions.Windows.ConfigWindows
{
    public partial class ConfigsList : UserControl
    {
        private Dictionary<string, ParsedConfig> _configsCache = new Dictionary<string, ParsedConfig>();
        private Dictionary<int, ParsedConfig> _loadedConfigs = new Dictionary<int, ParsedConfig>();
        public ConfigsList()
        {
            InitializeComponent();
        }

        private async void ComboBox_IndexChanged(object sender, EventArgs e)
        {
            VariablesPanel.Controls.Clear();

            int selectedIndex = ConfigComboBox.SelectedIndex;

            // TO DO: Add config import logic.

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
        }
    }
}
