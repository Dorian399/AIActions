using AIActions.ExternalData;
using AIActions.UserData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AIActions.Configs
{
    internal class ConfigLoader
    {
        private HashSet<string>? _loadedFiles;
        private Dictionary<string, object>? _currentJson;
        private string? _currentFilePath;

        public ConfigLoader() { }

        private async Task<Dictionary<string, object>?> LoadBase(string filePath)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Base config file (" + filePath + ") not found");
                return null;
            }

            string json = await File.ReadAllTextAsync(filePath);


            Dictionary<string, object> jsonParsed;

            try
            {
                jsonParsed = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
            }
            catch (JsonException e)
            {
                MessageBox.Show("Error loading Base config ('" + filePath + "'):\n" + e.ToString());
                return null;
            }

            Dictionary<string, object> jsonFinal = await SetupBase(jsonParsed, filePath);

            return jsonFinal;
        }

        private async Task<Dictionary<string, object>?> SetupBase(Dictionary<string, object> jsonParsed, string filePath)
        {
            Dictionary<string, object> jsonBase;
            // Load base if exists and is longer than 0.
            if (jsonParsed.TryGetValue("base", out object baseValue))
            {
                string baseString;

                // no base, means its the base itself.
                if (baseValue == null)
                {
                    return jsonParsed;
                }

                baseString = baseValue.ToString();

                // no base either.
                if (baseString.Length <= 0)
                {
                    return jsonParsed;
                }

                string fileName = Path.GetFileName(filePath);
                // base references itself.
                if (baseString == fileName)
                {
                    return jsonParsed;
                }

                // Prevent infinite recursion, by checking what base file was loaded.
                if (_loadedFiles.Contains(baseString))
                {
                    MessageBox.Show("Potential infinite recursion detected when loading ('" + filePath + "'), consider fixing you config file");
                    return null;
                }

                // Check if base config exists.
                string? baseFilePath=null;
                foreach(var kvp in Paths.ConfigFiles)
                {
                    if (kvp.Key+".json" == baseString)
                    {
                        baseFilePath = kvp.Value;
                    }
                }
                if (baseFilePath == null)
                {
                    MessageBox.Show("Couldn't find base config file for ('" + filePath + "'), make sure the base config file exists and is properly imported.");
                    return null;
                }

                // Overlap the child over the base.
                jsonBase = await LoadBase(baseFilePath);

                if (jsonBase == null)
                {
                    return null;
                }

                _loadedFiles.Add(baseString);

                if (jsonBase != null)
                {
                    foreach (var kvp in jsonParsed)
                    {
                        if (kvp.Key == "base")
                        {
                            continue;
                        }
                        jsonBase[kvp.Key] = kvp.Value;
                    }
                    jsonParsed = jsonBase;
                    return jsonParsed;
                }
            }
            return jsonParsed;
        }

        private T? ParseJsonElement<T>(string keyName)
        {
            if (_currentJson == null || _currentFilePath == null)
            {
                return default;
            }

            if (_currentJson.TryGetValue(keyName, out var elem) && elem is JsonElement vars)
            {
                try
                {
                    return JsonSerializer.Deserialize<T>(vars.GetRawText());
                }
                catch (JsonException e)
                {
                    MessageBox.Show("Your '" + keyName + "' key is not correct, consider fixing it. If you do not plan to use any user variables consider removing this key altogether.\nAffected file: ('" + _currentFilePath + "')");
                    return default;
                }
            }
            return default;
        }

        private static string TrimQuotes(string text)
        {
            if (text.Length < 2)
                return text;
            return text.Substring(1, text.Length - 2).Trim();
        }

        public async Task<ParsedConfig?> LoadFromFile(string filePath,bool configValidation=false)
        {
            // Reset variables to prevent issues.
            _loadedFiles = new HashSet<string>();
            _currentJson = null;
            _currentFilePath = null;

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Config file ('" + filePath + "') not found");
                return null;
            }

            _currentFilePath = filePath;

            //Add to loaded files to prevent infinite recursion.
            _loadedFiles.Add(Path.GetFileName(filePath));

            string directory = Path.GetDirectoryName(filePath);
            string json = await File.ReadAllTextAsync(filePath);

            Dictionary<string, object> jsonParsed;

            try
            {
                jsonParsed = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
            }
            catch (JsonException e)
            {
                MessageBox.Show("Error loading config ('" + filePath + "'):\n" + e.ToString());
                return null;
            }

            // Don't load when hidden=true
            if (!configValidation)
            {
                if (jsonParsed.TryGetValue("hidden", out var hiddenElem) && hiddenElem is JsonElement hidden)
                {
                    if (hidden.GetBoolean())
                        return null;
                }
            }

            // Load base config if it exists
            _currentJson = await SetupBase(jsonParsed, filePath);
            if (_currentJson == null)
            {
                return null;
            }

            // This will contain json and user variables, user variables will override any duplicate json variable.
            Dictionary<string, string> jsonAllVariables = new Dictionary<string, string>();

            // Parse json_variables.
            Dictionary<string, string>? parsedJsonVars = ParseJsonElement<Dictionary<string, string>>(keyName: "json_variables");
            if (parsedJsonVars != null)
            {
                jsonAllVariables = parsedJsonVars;
            }

            // Parse user_variables.
            List<string>? parsedUserVars = ParseJsonElement<List<string>>(keyName: "user_variables");

            if (parsedUserVars != null)
            {
                foreach (string key in parsedUserVars)
                {
                    string userVar="";
                    try
                    {
                        string currentConfig = AppSettings.GetCurrentConfig();
                        userVar = AppSettings.GetConfigVariable(currentConfig,key);
                    }catch(Exception ex)
                    {
                        MessageBox.Show("Failed to load user variable ("+key+")");
                    }
                    string keyval = userVar;
                    jsonAllVariables[key] = keyval;
                }
            }

            // Parse the rest and replace any variables (Except for {{PROMPT}} as it it will be replaced during the request).

            Dictionary<string, string> keysToRename = new Dictionary<string, string>
            {
                ["name"] = "Name",
                ["user_variables"] = "UserVariables",
                ["endpoint"] = "Endpoint",
                ["request_body"] = "Request",
                ["response_jsonpath"] = "ResponseJsonPath",
                ["request_headers"] = "Headers",
                ["request_type"] = "Type",
            };
            ParsedConfig parsedConfigs = new ParsedConfig();

            // Rename the keys for proper assignement to the ParsedConfig class, also replace user and json variables present in any of those elements.
            foreach (var kvp in keysToRename)
            {
                // Modify and rename key if exist
                if (_currentJson.TryGetValue(kvp.Key, out object val) && val is JsonElement jsonVal)
                {
                    string rawVal = jsonVal.GetRawText();
                    // Loop trough vars and replace them to their values.
                    foreach (var kvpVars in jsonAllVariables)
                    {
                        string safeVal = TrimQuotes(JsonSerializer.Serialize(kvpVars.Value));
                        rawVal = rawVal.Replace("{{" + kvpVars.Key + "}}", safeVal);
                    }
                    // Apply modifications and rename the key.
                    JsonElement finalVal = JsonDocument.Parse(rawVal).RootElement;
                    _currentJson[kvp.Value] = finalVal;
                    _currentJson.Remove(kvp.Key);
                }
            }


            string serializedJson = JsonSerializer.Serialize(_currentJson);

            try
            {
                parsedConfigs = JsonSerializer.Deserialize<ParsedConfig>(serializedJson);
                parsedConfigs.Codename = Path.GetFileNameWithoutExtension(filePath);
                parsedConfigs.Type = parsedConfigs.Type.ToUpper();
            }
            catch (JsonException e)
            {
                MessageBox.Show("Error loading config ('" + filePath + "'):\n" + e.ToString());
                return null;
            }

            if (parsedConfigs.Request == null || parsedConfigs.Endpoint == null || parsedConfigs.ResponseJsonPath == null)
            {
                MessageBox.Show("Error loading config ('" + filePath + "'):\n Your missing a required key (request_body, endpoint, response_jsonpath), check your config file");
                return null;
            }

            return parsedConfigs;

        }

        public async Task<ParsedConfig?> LoadFromName(string codename)
        {
            string? configFullPath = null;
            SortedDictionary<string, string> configFiles = Paths.ConfigFiles;
            if (configFiles.ContainsKey(codename))
            {
                configFullPath = configFiles[codename];
            }

            if (configFullPath != null)
            {
                return await LoadFromFile(configFullPath);
            }
            return null;
        }

        public async Task<ParsedConfig?> LoadFromAppSettings()
        {
            try
            {
                string currentConfigName = AppSettings.GetCurrentConfig();
                return await LoadFromName(currentConfigName);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Failed to load application settings (if the issue persists remove the "+Paths.AppSettingsFile+" file or ensure that the program has permisison to open it)");
                return null;
            }
        }
    }
}
