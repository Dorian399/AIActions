using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AIActions.AI
{
    internal class ConfigLoader
    {
        private HashSet<string>? loadedFiles;
        private Dictionary<string, object>? currentJson;
        private string? currentFilePath;
        internal class ParsedConfig
        {
            public string Codename { get; set; }
            public string Name { get; set; }
            public string Endpoint { get; set; }
            public Dictionary<string,object> Request { get; set; }
            public string ResponseJsonPath { get; set; }
            public string Type { get; set; }
            public Dictionary<string,string>? Headers { get; set; }
        }

        public ConfigLoader() { }

        private async Task<Dictionary<string,object>?> loadBase(string filePath)
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

            Dictionary<string, object> jsonFinal = await setupBase(jsonParsed, filePath);

            return jsonFinal;
        }

        private async Task<Dictionary<string,object>?> setupBase(Dictionary<string,object> jsonParsed,string filePath)
        {
            Dictionary<string, object> jsonBase;
            // Load base if exists and is longer than 0.
            if (jsonParsed.TryGetValue("base", out object baseValue))
            {
                string baseString;

                // no base, means its the base itself.
                if(baseValue == null)
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
                if (loadedFiles.Contains(baseString))
                {
                    MessageBox.Show("Potential infinite recursion detected when loading ('" + filePath + "'), consider fixing you config file");
                    return null;
                }

                // Overlap the child over the base.
                string directory = Path.GetDirectoryName(filePath);
                jsonBase = await loadBase(Path.Combine(directory,baseString));

                if(jsonBase == null)
                {
                    return null;
                }

                loadedFiles.Add(baseString);

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

        private T? parseJsonElement<T>(string keyName)
        {
            if(currentJson==null || currentFilePath == null)
            {
                return default;
            }

            if (currentJson.TryGetValue(keyName, out var elem) && elem is JsonElement vars)
            {
                try
                {
                    return JsonSerializer.Deserialize<T>(vars.GetRawText());
                }
                catch (JsonException e)
                {
                    MessageBox.Show("Your '" + keyName + "' key is not correct, consider fixing it. If you do not plan to use any user variables consider removing this key altogether.\nAffected file: ('" + currentFilePath + "')");
                    return default;
                }
            }
            return default;
        }

        private static string trimQuotes(string text) {
            if (text.Length < 2)
                return text;
            return text.Substring(1,text.Length-2).Trim();
        } 

        private string? convertJsonElementToString(string keyName,bool required)
        {
            if (currentJson == null || currentFilePath == null)
            {
                return null;
            }

            if (currentJson.TryGetValue(keyName, out var elem) && elem is JsonElement vars)
            {
                string text = vars.GetRawText();
                return text;
            }

            if (required)
            {
                MessageBox.Show("Your '" + keyName + "' key is missing, it is required to properly operate.\nAffected file: ('" + currentFilePath + "')");
            }
            return null;
        }

        public async Task<ParsedConfig?> LoadFromFile(string filePath)
        {
            // Reset variables to prevent issues.
            loadedFiles = new HashSet<string>();
            currentJson = null;
            currentFilePath = null;

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Config file ('"+filePath+"') not found");
                return null;
            }

            currentFilePath = filePath;

            //Add to loaded files to prevent infinite recursion.
            loadedFiles.Add(Path.GetFileName(filePath));

            string directory = Path.GetDirectoryName(filePath);
            string json = await File.ReadAllTextAsync(filePath);

            Dictionary<string, object> jsonParsed;

            try
            {
                jsonParsed = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
            }
            catch(JsonException e)
            {
                MessageBox.Show("Error loading config ('"+filePath+"'):\n"+e.ToString());
                return null;
            }

            // Load base config if it exists
            currentJson = await setupBase(jsonParsed,filePath);
            if (currentJson == null) {
                return null;
            }

            // This will contain json and user variables, user variables will override any duplicate json variable
            Dictionary<string, string> jsonAllVariables = new Dictionary<string, string>();

            // Parse json_variables.
            Dictionary<string, string>? parsedJsonVars = parseJsonElement<Dictionary<string, string>>(keyName: "json_variables");
            if (parsedJsonVars != null) {
                jsonAllVariables = parsedJsonVars;
            }

            // Parse user_variables.
            List<string>? parsedUserVars = parseJsonElement<List<string>>(keyName: "user_variables"); ;

            if (parsedUserVars != null)
            {
                foreach(string key in parsedUserVars)
                {
                    string keyval = key.Replace("{{", "").Replace("}}", ""); // To do, actually get var from user settings.
                    jsonAllVariables[key] = keyval;
                }
            }

            // Parse the rest and replace any variables (Except for {{PROMPT}} as it it will be replaced during the request).
            Dictionary<string, string> keysToRename = new Dictionary<string, string>
            {
                ["name"] = "Name",
                ["endpoint"] = "Endpoint",
                ["request_body"] = "Request",
                ["response_jsonpath"] = "ResponseJsonPath",
                ["request_headers"] = "Headers",
                ["type"] = "Type",
            };
            ParsedConfig parsedConfigs = new ParsedConfig();

            // Rename the keys for proper assignement to the ParsedConfig class, also replace user and json variables present in any of those elements.
            foreach (var kvp in keysToRename)
            {
                // Modify and rename key if exist
                if(currentJson.TryGetValue(kvp.Key, out object val) && val is JsonElement jsonVal)
                {
                    string rawVal = jsonVal.GetRawText();
                    // Loop trough vars and replace them to their values.
                    foreach(var kvvp in jsonAllVariables)
                    {
                        string safeVal = trimQuotes(JsonSerializer.Serialize(kvvp.Value));
                        rawVal = rawVal.Replace("{{"+kvvp.Key+"}}", safeVal);
                    }
                    // Apply modifications and rename the key.
                    JsonElement finalVal = JsonDocument.Parse(rawVal).RootElement;
                    currentJson[kvp.Value] = finalVal;
                    currentJson.Remove(kvp.Key);
                }
            }


            string serializedJson = JsonSerializer.Serialize(currentJson);

            try {
                parsedConfigs = JsonSerializer.Deserialize<ParsedConfig>(serializedJson);
            }catch(JsonException e)
            {
                MessageBox.Show("Error loading config ('" + filePath + "'):\n" + e.ToString());
                return null;
            }

            Debug.WriteLine(parsedConfigs.Name);
            Debug.WriteLine(parsedConfigs.Endpoint);
            Debug.WriteLine(parsedConfigs.Request);
            Debug.WriteLine(parsedConfigs.ResponseJsonPath);
            Debug.WriteLine(parsedConfigs.Headers);

            return parsedConfigs;

        }
    }
}
