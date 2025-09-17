using AIActions.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                OnStatusChanged("Error: File or folder is missing or inacessible.", (int)StatusCode.Error);
                return;
            }

            object promptContext;

            FileAttributes attr = File.GetAttributes(folderOrFile);

            if (attr.HasFlag(FileAttributes.Directory))
                promptContext = new PromptContextFolder(folderOrFile, rawPrompt);
            else
                promptContext = new PromptContextFolder(folderOrFile, rawPrompt); // TO DO PromptContextFile

            string fullPrompt = await PromptFormatter.GetPrompt(rawPrompt);

            

        } 
    }
}
