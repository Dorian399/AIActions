using AIActions.AI;
using AIActions.Configs;
using AIActions.ExternalData;
using AIActions.UserData;
using AIActions.Windows;
using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIActions
{
    internal static class Program
    {
        public static PromptWindow MainWindow;

        private static void RegisterContextMenu()
        {
            // Right click on directory background registries.
            string directoryKeyPath = "Directory\\background\\shell";
            RegistryKey? directoryKey = Registry.ClassesRoot.OpenSubKey(directoryKeyPath, writable: true);
            if (directoryKey == null)
                throw new Exception("Failed to open registry key: "+directoryKeyPath);

            RegistryKey directoryKeyRoot = directoryKey.CreateSubKey("AIActions", writable: true);
            if (directoryKeyRoot == null)
                throw new Exception("Failed to create registry sub key \"AIActions\"");

            RegistryKey directoryKeyCommand = directoryKeyRoot.CreateSubKey("command", writable: true);
            if (directoryKeyRoot == null)
                throw new Exception("Failed to create registry sub key \"command\"");

            directoryKeyRoot.SetValue("","Open with AIActions");
            directoryKeyRoot.SetValue("Icon", "\""+Paths.ContextMenuIcon+"\"");
            directoryKeyCommand.SetValue("","\""+Application.ExecutablePath+"\" \"%V\"");

        }

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
            bool shouldRegister = false;
            if (args.Length > 0)
            {
                if (args[0] == "register")
                    shouldRegister = true;
                else
                workFolder = args[0];
            }
            else
            {
                shouldRegister = true;
            }

            if (shouldRegister) 
            {
                try
                {
                    RegisterContextMenu();
                }catch(Exception e)
                {
                    if (args.Length > 0)
                    {
                        MessageBox.Show($"Failed to register the app to the context menu.\nFull exception: {e.Message}");
                        return 1;
                    }
                }

                // no arguments = silent register
                if(args.Length > 0)
                {
                    MessageBox.Show("Sucessfully registered the app to the context menu.");
                    return 0;
                }
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