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
            Success = 1
        }

        public delegate void StatusEventHandler(string value, int code, ResultParser? parsedResult=null);
        public static event StatusEventHandler OnStatusChanged;
        public AIRequester() { }

        public async Task SendPrompt(ParsedConfig config, string folderOrFile, string rawPrompt)
        {
            if (!Path.Exists(folderOrFile))
            {
                OnStatusChanged?.Invoke("Error: File or folder is missing or inacessible.", (int)StatusCode.Error);
                return;
            }

            if (rawPrompt==null)
            {
                OnStatusChanged?.Invoke("Error: No prompt provided.", (int)StatusCode.Error);
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
            finalPrompt = JsonSerializer.Serialize(finalPrompt).Substring(1,finalPrompt.Length - 2);


            // Prepare the data for the request.

            ResultParser parsedResult = null;
            string requestBody = JsonSerializer.Serialize(config.Request).Replace("{{PROMPT}}",finalPrompt);

            Dictionary<string,string> headers = new Dictionary<string,string>();
            foreach (var kvp in config.Headers) {
                string key = kvp.Key.Replace("{{PROMPT}}", finalPrompt);
                string value = kvp.Value.Replace("{{PROMPT}}", finalPrompt);
                headers[key] = value;
            }

            // Send the request.

            OnStatusChanged?.Invoke("Sending request...", (int)StatusCode.Processing);

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

                HttpResponseMessage response = await client.SendAsync(request);
                string result = await response.Content.ReadAsStringAsync();

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                OnStatusChanged("Error: Failed to send the request: \n" + e.ToString(), (int)StatusCode.Error);
                return;
            }

        } 
    }
}
