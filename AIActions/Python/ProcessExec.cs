using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIActions.Python
{
    internal class ProcessExec
    {
        public Action<string>? OnOutput { get; set; }
        public Control? UiControl { get; set; }

        public ProcessExec() { }

        public async Task<int> StartAsync(string executable, string arguments, CancellationToken token = default, string workingDirectory = "")
        {
            using Process process = new Process();
            process.StartInfo.FileName = executable;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.WorkingDirectory = workingDirectory;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;

            process.OutputDataReceived += (s, e) => {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    UiControl?.BeginInvoke(new Action(() => OnOutput?.Invoke(e.Data)));
                }
            };
            process.ErrorDataReceived += (s, e) => {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    UiControl?.BeginInvoke(new Action(() => OnOutput?.Invoke(e.Data)));
                }
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            try
            {
                await process.WaitForExitAsync(token);
            }
            catch (OperationCanceledException e) { 
                if(!process.HasExited)
                    process.Kill(entireProcessTree: true);
                throw;
            }
            return process.ExitCode;
        }
    }
}
