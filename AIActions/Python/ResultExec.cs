using AIActions.AI.Results;
using AIActions.ExternalData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIActions.Python
{
    internal class ResultExec
    {
        public Action<string>? OnOutput;

        public async Task<bool> ExecuteResults(
            ParsedResult results,
            string? prompt,
            string workingDirectory,
            Control uiControl, 
            CancellationToken token = default
        )
        {
            // Create the script file

            long unixTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            string scriptPath = Path.Combine(Paths.PythonScriptsFolder, unixTimestamp + ".py");

            FileStream script = File.Create( scriptPath );

            if (!String.IsNullOrWhiteSpace(prompt))
            {
                string promptAsComment = "#"+prompt.Trim().Replace("\n"," ").Replace("\r"," ")+"\n";
                byte[] promptBytes = Encoding.UTF8.GetBytes(promptAsComment);
                await script.WriteAsync(promptBytes,token);
            }

            // Fix faulty indenting (trims n tabs from each line).
            string[] lines = results.Script.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            int minIndents = int.MaxValue;
            foreach (string line in lines) {
                int curIndents = 0;

                if (String.IsNullOrWhiteSpace(line))
                    continue;

                for(int i = 0; i < line.Length; i++)
                {
                    if (line[i] != '\t')
                        break;
                    curIndents++;
                }
                if(curIndents < minIndents)
                    minIndents = curIndents;
            }

            string scriptText = "";
            if (minIndents > 0)
            {
                foreach (string line in lines)
                {
                    if (String.IsNullOrWhiteSpace(line))
                    {
                        scriptText += line + "\n";
                        continue;
                    }
                    string trimmedLine = line.Substring(minIndents);
                    scriptText += trimmedLine+"\n";
                }
            }
            else
            {
                scriptText = results.Script;
            }

            // Writes the script to the file.

            byte[] scriptContent = Encoding.UTF8.GetBytes(scriptText);

            await script.WriteAsync(scriptContent,token);

            script.Close();

            // Install packages

            if (results.Packages.Length > 0)
            {
                DependencyInstall pip = new DependencyInstall();
                pip.OnOutput = text => OnOutput?.Invoke(text);
                await pip.InstallList(results.Packages, token);
            }

            // Run the script

            ScriptExec python = new ScriptExec();
            python.OnOutput = text => OnOutput?.Invoke(text);
            bool exitBool = await python.RunScript(
                scriptPath,
                workingDirectory,
                uiControl,
                token
            );

            return exitBool;   
        }
    }
}
