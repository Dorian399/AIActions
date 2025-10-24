using AIActions.ExternalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIActions.Python
{
    internal static class PythonInfo
    {
        public static async Task<string> GetPythonVersion(CancellationToken token = default)
        {
            ProcessExec process = new ProcessExec();
            StringBuilder sb = new StringBuilder();
            process.OnOutput = text => { sb.AppendLine(text); };
            try
            {
                int code = await process.StartAsync(Paths.PythonExecutable, "-V", token);
                if (code == 0)
                {
                    string version = sb.ToString().Replace("Python ", "");
                    return version;
                }
                else
                {
                    throw (new Exception());
                }

            }
            catch (Exception ex)
            {
                return "Failed to retrieve version";
            }
        }
    }
}
