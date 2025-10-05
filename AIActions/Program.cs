using AIActions.AI;
using AIActions.Configs;
using AIActions.ExternalData;
using AIActions.UserData;
using AIActions.Windows;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AIActions
{
    internal static class Program
    {
        public static PromptWindow MainWindow;
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
            ParsedConfig? parsedConfig = await loader.LoadFromAppSettings();

            ApplicationConfiguration.Initialize();

            // The window will auto redirect to config selection if  something is wrong.
            MainWindow = new PromptWindow(workFolder, parsedConfig);

            Application.Run(MainWindow);
        }
    }
}