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
        static async Task<int> Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            if (!File.Exists(Paths.PythonExecutable))
            {
                MessageBox.Show("Missing Python executable, check your /data/python directory or reinstall the program.");
                return 1;
            }

            if (!File.Exists(Paths.PipExecutable))
            {
                MessageBox.Show("Missing pip module for Python, check your /data/python directory or reinstall the program.");
                return 1;
            }

            // Register logic explanation:
            // register argument = register with results in a message box (stops execution after register).
            // registersilent argument = register silently (stops execution after register).
            // no arguments = register silently (dont stop execution, will show missing file/folder error).
            string? fileOrFolder=null;
            bool shouldRegister = false;
            bool silentRegister = false;
            if (args.Length > 0)
            {
                if (args[0] == "register")
                    shouldRegister = true;
                else if (args[0] == "silentregister")
                {
                    shouldRegister = true;
                    silentRegister = true;
                }
                else
                    // Set file or folder.
                    fileOrFolder = args[0];
            }
            else
            {
                shouldRegister = true;
                silentRegister = true;
            }

            if (shouldRegister) 
            {
                try
                {
                    RegisterContextMenu();
                }catch(Exception e)
                {
                    if (!silentRegister)
                    {
                        MessageBox.Show($"Failed to register the app to the context menu.\nFull exception: {e.Message}");
                    }
                    if(args.Length > 0)
                        return 1;
                    }
                }

                if(!silentRegister)
                {
                    MessageBox.Show("Sucessfully registered the app to the context menu.");
                    return 0;
                }
            }

            ConfigLoader loader = new ConfigLoader();
            ParsedConfig? parsedConfig = await loader.LoadFromAppSettings();

            ApplicationConfiguration.Initialize();

            // The window will auto redirect to config selection if  something is wrong with the config.
            MainWindow = new PromptWindow(fileOrFolder, parsedConfig);

            Application.Run(MainWindow);
            return MainWindow.ExitCode;
        }
    }
}