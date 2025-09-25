using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // Configs related.
        public static string ConfigFilesFolder
        {
            get
            {
                return Path.Combine(ExternalDataFolder, "config_files");
            }
        }

        public static string[] ConfigFiles
        {
            get
            {
                return (string[])Directory.GetFiles(ConfigFilesFolder);
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
