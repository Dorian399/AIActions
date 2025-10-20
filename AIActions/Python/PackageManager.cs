using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AIActions.ExternalData;

namespace AIActions.Python
{
    internal static class PackageManager
    {
        public static Action<string>? OnOutput;

        public static async Task<SortedDictionary<string,string>> GetListAsync(CancellationToken token=default)
        {
            SortedDictionary<string, string> packages = new SortedDictionary<string, string>();

            ProcessExec executor = new ProcessExec();
            StringBuilder stringBuilder = new StringBuilder();
            executor.OnOutput = text => { stringBuilder.AppendLine(text); };

            int code = await executor.StartAsync(Paths.PythonExecutable, "-m pip list --format json", token);

            if (code != 0)
                throw new Exception("Pip exited with a non-zero code: "+code.ToString());

            string packagesJsonRaw = stringBuilder.ToString();
            List<Dictionary<string, string>>? parsedJson = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(packagesJsonRaw);
            if (parsedJson == null)
                return packages;

            foreach(var dict in parsedJson)
            {
                string? key = dict.GetValueOrDefault("name");
                string? val = dict.GetValueOrDefault("version");

                if(key==null || val==null) continue;

                packages[key] = val;
            }

            return packages;
        }

        public static async Task<bool> RemovePackages(string[] names, Control? uiControl=null,CancellationToken token = default)
        {

            if(!Path.Exists(Paths.TemporaryFilesFolder))
                Directory.CreateDirectory(Paths.TemporaryFilesFolder);

            StringBuilder packages = new StringBuilder();
            foreach (string name in names)
            {
                if (name == "pip")
                    continue;
                packages.AppendLine(name);
            }

            string filePath = Path.Combine(Paths.TemporaryFilesFolder,packages.GetHashCode().ToString()+".txt");

            await File.WriteAllTextAsync(filePath, packages.ToString(), token);

            ProcessExec executor = new ProcessExec();
            executor.OnOutput = text => { 
                if(uiControl != null && uiControl.IsHandleCreated)
                {
                    uiControl.BeginInvoke(new Action(() =>
                    {
                        OnOutput?.Invoke(text);
                    }));
                }
                else if(uiControl == null)
                {
                    OnOutput?.Invoke(text);
                }
            };

            int code = await executor.StartAsync(Paths.PythonExecutable, $"-m pip uninstall -r \"{Path.GetFullPath(filePath)}\"", token);

            if (code != 0)
                return false;

            return true;
        }
    }
}
