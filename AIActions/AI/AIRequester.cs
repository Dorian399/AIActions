using AIActions.AI.Results;
using AIActions.Configs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIActions.AI
{
    internal class AIRequester
    {
        public enum StatusCode
        {
            Processing = -1,
            Error = 0,
            Accepted = 1,
            Rejected = 2,
        }

        public delegate Task StatusEventHandler(string value, StatusCode code, ParsedResult? parsedResult=null, string? error=null);
        public event StatusEventHandler OnStatusChanged;
        public AIRequester() { }

        public async Task SendPrompt(ParsedConfig config, string folderOrFile, string rawPrompt, CancellationToken token)
        {
            if (!Path.Exists(folderOrFile))
            {
                OnStatusChanged?.Invoke("Error: File or folder is missing or inacessible.", StatusCode.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(rawPrompt))
            {
                OnStatusChanged?.Invoke("Error: No prompt provided.", StatusCode.Error);
                return;
            }

            // Create a prompt with proper context.
            object promptContext;

            FileAttributes attr = File.GetAttributes(folderOrFile);

            if (attr.HasFlag(FileAttributes.Directory))
                promptContext = new PromptContextFolder(folderOrFile, rawPrompt);
            else
                promptContext = new PromptContextFolder(folderOrFile, rawPrompt); // TO DO PromptContextFile

            string finalPrompt = await PromptFormatter.GetPrompt(promptContext);
            // Make it json safe and remove the quotes.
            finalPrompt = JsonSerializer.Serialize(finalPrompt);
            finalPrompt = finalPrompt.Substring(1, finalPrompt.Length - 2);


            // Prepare the data for the request.

            string requestBody = JsonSerializer.Serialize(config.Request).Replace("{{PROMPT}}",finalPrompt);

            Dictionary<string,string> headers = new Dictionary<string,string>();
            foreach (var kvp in config.Headers) {
                string key = kvp.Key.Replace("{{PROMPT}}", finalPrompt);
                string value = kvp.Value.Replace("{{PROMPT}}", finalPrompt);
                headers[key] = value;
            }

            // Send the request.

            OnStatusChanged?.Invoke("Sending request...", StatusCode.Processing);

            string result;
            try
            {
                using HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(new HttpMethod(config.Type), config.Endpoint)
                {
                    Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
                };
                foreach (var kvp in headers) { 
                    request.Headers.Add(kvp.Key, kvp.Value);
                }

                HttpResponseMessage response = await client.SendAsync(request,token);
                result = await response.Content.ReadAsStringAsync();

            }
            catch (Exception e)
            {
                OnStatusChanged?.Invoke("Error: Failed to send the request.", StatusCode.Error, error: e.ToString());
                return;
            }

            // Parse results

            OnStatusChanged?.Invoke("Parsing results...", StatusCode.Processing);

            ParsedResult parsedResult = ResultParser.FromJson(result,config.ResponseJsonPath);

            if (!parsedResult.IsValid) {
                string headersString = "";
                foreach (var kvp in headers)
                {
                    headersString = headersString + kvp.Key + ": " + kvp.Value+"\n";
                }
                string errorString = parsedResult.Comments + "\n\nRequest headers:\n" + headersString + "\n\nRequest body:\n"+ requestBody + "\n\nResponse:\n" + result + "\n\nJSONPath: " + config.ResponseJsonPath;
                OnStatusChanged?.Invoke("Error: Results failed to parse.", StatusCode.Error, error: errorString);
                return;
            }

            if (parsedResult.Accepted)
            {
                OnStatusChanged?.Invoke("Request accepted:\n" + parsedResult.Comments, StatusCode.Accepted,parsedResult);
            }
            else
            {
                OnStatusChanged?.Invoke("Request rejected:\n" + parsedResult.Comments, StatusCode.Rejected);
            }
            

        } 
    }
}
