using AIActions.ExternalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AIActions.UserData
{
    internal class AppSettings
    {
        private class AppSettingsLayout
        {
            public string CurrentConfig { get; set; } = "";
            public Dictionary<string, Dictionary<string, string>> ConfigUserVariables { get; set; } = new Dictionary<string, Dictionary<string, string>>();

            public AppSettingsLayout() { }
        }

        private static AppSettingsLayout _appSettingsCache = new AppSettingsLayout();
        private static bool configsLoaded = false;

        private static void LoadAppSettings()
        {
            if( !Path.Exists(Paths.UserDataFolder) )
                Directory.CreateDirectory(Paths.UserDataFolder);

            if (!Path.Exists(Paths.AppSettingsFile))
            {
                string jsonConfig = JsonSerializer.Serialize(_appSettingsCache);
                File.WriteAllText(Paths.AppSettingsFile, jsonConfig);
                configsLoaded = true;
                return;
            }

            string configFileText = File.ReadAllText(Paths.AppSettingsFile);
            _appSettingsCache = JsonSerializer.Deserialize<AppSettingsLayout>(configFileText);
            configsLoaded = true;
        }

        private static void UpdateAppSettings()
        {
            string jsonConfig = JsonSerializer.Serialize(_appSettingsCache);
            File.WriteAllText(Paths.AppSettingsFile, jsonConfig);
        }

        public static string GetCurrentConfig()
        {
            if (!configsLoaded)
                LoadAppSettings();
            return _appSettingsCache.CurrentConfig;
        }

        public static void SetCurrentConfig(string codename)
        {
            _appSettingsCache.CurrentConfig = codename;
            UpdateAppSettings();
        }

        public static string GetConfigVariable(string configCodename, string varName)
        {
            if (!configsLoaded)
                LoadAppSettings();

            if (!_appSettingsCache.ConfigUserVariables.ContainsKey(configCodename))
                return "";

            if (!_appSettingsCache.ConfigUserVariables[configCodename].ContainsKey(varName))
                return "";

            return _appSettingsCache.ConfigUserVariables[configCodename][varName];
        }

        public static void SetConfigVariable(string configCodename, string varName, string varValue)
        {
            if (!configsLoaded)
                LoadAppSettings();

            if (!_appSettingsCache.ConfigUserVariables.ContainsKey(configCodename))
                _appSettingsCache.ConfigUserVariables[configCodename] = new Dictionary<string, string>();

            _appSettingsCache.ConfigUserVariables[configCodename][varName] = varValue;
            UpdateAppSettings();
        }

    }
}
