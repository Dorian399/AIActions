using AIActions.Configs;
using AIActions.ExternalData;
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
        private Dictionary<string, ParsedConfig> _configsCache;
        private Dictionary<int, ParsedConfig> _loadedConfigs;
        public ConfigsList()
        {
            InitializeComponent();
        }

        private async void ConfigsList_Loaded(object sender, EventArgs e)
        {
            ConfigComboBox.Items.Clear();
            _loadedConfigs = new Dictionary<int, ParsedConfig>();

            SortedDictionary<string, string> configFiles = Paths.ConfigFiles;

            ConfigLoader configLoader = new ConfigLoader();

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
                index++;
            }
        }
    }
}
