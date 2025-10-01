using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AIActions.ExternalData
{
    internal class Paths
    {
        // Root folder.
        public static string ExternalDataFolder { 
            get
            {
                return Path.Combine(AppContext.BaseDirectory, "data");
            }
        }

        // Userdata folder.
        public static string UserDataFolder
        {
            get
            {
                return Path.Combine(AppContext.BaseDirectory, "user_data");
            }
        }

        // Configs related.
        public static string ConfigFilesFolder
        {
            get
            {
                return Path.Combine(ExternalDataFolder, "config_files");
            }
        }

        public static string UserConfigFilesFolder
        {
            get
            {
                return Path.Combine(UserDataFolder, "config_files");
            }
        }

        public static SortedDictionary<string,string> ConfigFiles
        {
            get
            {
                // User configs can override regular configs.
                // Example: (user_data/config_files/config.json) will have priority over (data/config_files/config.json).
                SortedDictionary<string, string> configs = new SortedDictionary<string, string>();
                // User configs
                foreach(string file in Directory.GetFiles(Paths.ConfigFilesFolder))
                {
                    string ext = Path.GetExtension(file);
                    string filename = Path.GetFileName(file);

                    if (ext.ToLower() != ".json")
                        continue;

                    configs[filename] = file;
                }

                foreach (string file in Directory.GetFiles(Paths.ConfigFilesFolder))
                {
                    string ext = Path.GetExtension(file);
                    string filename = Path.GetFileName(file);

                    if (ext.ToLower() != ".json")
                        continue;

                    configs[filename] = file;
                }

                return configs;
            }
        }

        // Prompt related.
        public static string PromptsFolder
        {
            get
            {
                return Path.Combine(ExternalDataFolder, "prompts");
            }
        }

        public static string PromptFolderTextFile
        {
            get
            {
                return Path.Combine(PromptsFolder, "prompt_folder.txt");
            }
        }

        public static string PromptFileTextFile
        {
            get
            {
                return Path.Combine(PromptsFolder, "prompt_file.txt");
            }
        }

        // Python related

        public static string PythonFolder
        {
            get
            {
                return Path.Combine(ExternalDataFolder, "python");
            }
        }
        public static string PythonExecutable
        {
            get
            {
                return Path.Combine(PythonFolder, "python.exe");
            }
        }

        public static string PipExecutable
        {
            get
            {
                return Path.Combine(PythonFolder,"Scripts","pip.exe");
            }
        }

        public static string PythonScriptsFolder
        {
            get
            {
                return Path.Combine(Paths.ExternalDataFolder, "scripts_history");
            }
        }
    }
}
