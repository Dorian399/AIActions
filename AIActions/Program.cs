using AIActions.AI;
using AIActions.Configs;
using AIActions.ExternalData;
using AIActions.UserData;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AIActions
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            if (!File.Exists(Paths.PythonExecutable))
            {
                MessageBox.Show("Missing Python executable, check your /data/python directory or reinstall the program.");
                return;
            }

            if (!File.Exists(Paths.PipExecutable))
            {
                MessageBox.Show("Missing pip module for Python, check your /data/python directory or reinstall the program.");
                return;
            }

            string? workFolder=null;
            if (args.Length > 0)
            {
                workFolder = args[0];
            }

            ConfigLoader loader = new ConfigLoader();
            ParsedConfig parsedConfig;
            string currentConfigName = AppSettings.GetCurrentConfig();
            string? configFullPath=null;
            SortedDictionary<string,string> configFiles = Paths.ConfigFiles;
            if (configFiles.ContainsKey(currentConfigName))
            {
                configFullPath = configFiles[currentConfigName];
            }

            if (configFullPath != null)
            {
                parsedConfig = await loader.LoadFromFile(configFullPath);
            }
            else
            {
                return;
            }

                ApplicationConfiguration.Initialize();

            PromptWindow window = new PromptWindow(workFolder, parsedConfig);
            window.ShouldHide = true;

            Application.Run(window);
        }
    }
}