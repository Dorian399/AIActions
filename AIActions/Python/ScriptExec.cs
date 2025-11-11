using AIActions.ExternalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIActions.Python
{
    internal class ScriptExec
    {
        public Action<string>? OnOutput { get; set; }
        public async Task<bool> RunScript(string scriptPath,string workingDirectory,Control uiControl,CancellationToken token=default)
        {
            ProcessExec process = new ProcessExec();
            process.UiControl = uiControl;
            process.OnOutput = text => OnOutput?.Invoke(text);
            OnOutput?.Invoke("Running script: " + scriptPath+"\n\n");
            int exitCode = await process.StartAsync(Paths.PythonExecutable,scriptPath,token,workingDirectory);
            if(exitCode == 0)
            {
                OnOutput?.Invoke("\nScript completed successfully.");
                return true;
            }
            else
            {
                OnOutput?.Invoke("\nScript execution failed, code: "+exitCode.ToString()+".");
                return false;
            }
        }
    }
}
