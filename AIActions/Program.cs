using AIActions.AI;
using AIActions.Configs;
using AIActions.ExternalData;
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

            string configName = "gemini_flash.json"; // TO DO, Use persistent user settings.
            string configFullPath = Path.Combine(Paths.ConfigFilesFolder, configName);

            ConfigLoader loader = new ConfigLoader();
            ParsedConfig parsedConfig = await loader.LoadFromFile(configFullPath);

            ApplicationConfiguration.Initialize();

            PromptWindow window = new PromptWindow(workFolder, parsedConfig);
            window.ShouldHide = true;

            Application.Run(window);
        }
    }
}