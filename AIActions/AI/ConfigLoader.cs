using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AIActions.AI
{
    internal class ConfigLoader
    {
        private string directory;
        private HashSet<string> loadedFiles;
        internal class ParsedConfig
        {
            public string codename { get; set; }
            public string name { get; set; }
            public string request { get; set; }
            public string response_jsonpath { get; set; }
            public string type { get; set; }
            public string headers { get; set; }
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

                // base references itself.
                string fileName = Path.GetFileName(filePath);
                if(baseString == fileName)
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
                jsonBase = await loadBase(Path.Combine(this.directory,baseString));

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
        public async Task<ParsedConfig?> LoadFromFile(string filePath)
        {
            // Reset variables to prevent issues.
            directory = null;
            loadedFiles = new HashSet<string>();

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Config file ('"+filePath+"') not found");
                return null;
            }

            //Add to loaded files to prevent infinite recursion.
            loadedFiles.Add(Path.GetFileName(filePath));

            directory = Path.GetDirectoryName(filePath);
            string json = await File.ReadAllTextAsync(filePath);

            Dictionary<string, object> jsonParsed;

            try
            {
                jsonParsed = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
            }catch(JsonException e)
            {
                MessageBox.Show("Error loading config ('"+filePath+"'):\n"+e.ToString());
                return null;
            }

            // Load base configs if they exist
            Dictionary<string, object> jsonFinal = await setupBase(jsonParsed,filePath);
            if (jsonFinal == null) {
                return null;
            }

            return null;

        }
    }
}
