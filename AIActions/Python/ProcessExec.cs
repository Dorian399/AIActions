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

        private const int QUEUE_CHUNK_SIZE = 100; // Maximum amount of lines sent in a single interval
        private const int OUTPUT_INTERVAL = 10; // Output sending interval in ms.

        private ConcurrentQueue<string> OutputQueue= new ConcurrentQueue<string>();

        public ProcessExec() { }

        private async Task AwaitQueue()
        {
            while (OutputQueue.Count > 0)
            {
                await Task.Delay(OUTPUT_INTERVAL);
            }
            await Task.Delay(OUTPUT_INTERVAL);
        }

        private async Task SendOutput()
        {
            string fullData = await Task.Run(() =>
            {
                StringBuilder sb = new StringBuilder();
                int dequeueCount = 0;
                while (OutputQueue.TryDequeue(out string? data) && dequeueCount < QUEUE_CHUNK_SIZE)
                {
                    sb.AppendLine(data);
                    dequeueCount++;
                }
                return sb.ToString();
            });

            if (string.IsNullOrWhiteSpace(fullData))
                return;

            if (UiControl != null && UiControl.IsHandleCreated)
            {
                var tcs = new TaskCompletionSource();
                UiControl.BeginInvoke(new Action(() =>
                {
                    OnOutput?.Invoke(fullData);
                    tcs.SetResult();
                }));
                await tcs.Task;
            }
            else
            {
                OnOutput?.Invoke(fullData);
            }
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

            System.Timers.Timer OutputThrottler = new System.Timers.Timer(OUTPUT_INTERVAL);
            OutputThrottler.AutoReset = true;
            OutputThrottler.Start();
            OutputThrottler.Elapsed += async (source, e) =>
            {
                await SendOutput();
            };

            process.OutputDataReceived += (s, e) => {
                if (!string.IsNullOrEmpty(e.Data))
                {
                        OutputQueue.Enqueue(e.Data);
                }
            };
            process.ErrorDataReceived += (s, e) => {
                if (!string.IsNullOrEmpty(e.Data))
                {
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
            // Waits until the whole queue is processed.
            await AwaitQueue();
            OutputThrottler.Stop();
            return process.ExitCode;
        }
    }
}
