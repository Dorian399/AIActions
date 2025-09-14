using AIActions.ExternalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AIActions.AI
{
    internal class PromptFormatter
    {
        public static async Task<string> GetPrompt(object promptContext)
        {
            string className = promptContext.GetType().Name;

            string promptTemplateFile="";

            if (className == "PromptContextFolder")
            {
                promptTemplateFile = Paths.PromptFolderTextFile;
            }else if (className == "PromptContextFile")
            {
                promptTemplateFile= Paths.PromptFileTextFile;
            }

            if (!File.Exists(promptTemplateFile))
            {
                return "Missing template file in ./data/prompts/";
            }

            // Read template file
            string promptTemplate = await File.ReadAllTextAsync(promptTemplateFile);

            // Replace instances of {{Prop}} in the prompt text.
            PropertyInfo[] promptContextProperties = promptContext.GetType().GetProperties();

            foreach(PropertyInfo prop in promptContextProperties)
            {
                string? value = (string)prop.GetValue(promptContext, null);
                if (value == null)
                    continue;

                string key = "{{"+prop.Name+"}}";

                promptTemplate = promptTemplate.Replace(key, value);
            }

            return promptTemplate;
        }
    }
}
