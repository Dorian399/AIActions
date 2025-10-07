using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AIActions.Python
{
    internal class ProcessExec
    {
        public Action<string>? OnOutput { get; set; }
        public Control? UiControl { get; set; }

        private const int QUEUE_MAX_SIZE = 100;
        private const int OUTPUT_INTERVAL = 10;

        public ProcessExec() { }

        private Task SendOutput(ref ConcurrentQueue<string> queue)
        {
            TaskCompletionSource tcs = new TaskCompletionSource();
            StringBuilder sb = new StringBuilder();
            while (queue.TryDequeue(out string? data))
            {
                sb.AppendLine(data);
            }
            string fullData = sb.ToString();
            if (UiControl != null && UiControl.IsHandleCreated && !string.IsNullOrWhiteSpace(fullData))
            {
                UiControl?.BeginInvoke(new Action(() => {
                    OnOutput?.Invoke(fullData);
                    tcs.SetResult();
                }));
            }
            else
            {
                tcs.SetResult();
            }
            
            return tcs.Task;
        }

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

            // Throttle the output to 1000 lines/calls per seconds, to prevent UI freezes.
            ConcurrentQueue<string> OutputQueue = new ConcurrentQueue<string>();

            System.Timers.Timer OutputThrottler = new System.Timers.Timer(OUTPUT_INTERVAL);
            OutputThrottler.AutoReset = true;
            OutputThrottler.Start();
            OutputThrottler.Elapsed += (source, e) =>
            {
                SendOutput(ref OutputQueue);
            };

            process.OutputDataReceived += (s, e) => {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    if(OutputQueue.Count <= QUEUE_MAX_SIZE)
                        OutputQueue.Enqueue(e.Data);
                }
            };
            process.ErrorDataReceived += (s, e) => {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    if (OutputQueue.Count <= QUEUE_MAX_SIZE)
                        OutputQueue.Enqueue(e.Data);
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
            OutputThrottler.Stop();
            // Send the rest of the output.
            await SendOutput(ref OutputQueue);
            return process.ExitCode;
        }
    }
}
