using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AIActions.AI.Results
{
    internal class ResultParser
    {
        public bool IsValid { get; private set; }
        public bool Accepted { get; private set; }
        public string[] Packages { get; private set; }
        public string Script { get; private set; }
        public string Comments { get; private set; }

        private ResultParser() { }

        private static string PrepareJson(string json)
        {
            int start = json.IndexOf('{');
            int end = json.LastIndexOf('}');

            if (start == -1 || end == -1 || end < start)
                return string.Empty;

            return json.Substring(start, end - start + 1);
        }

        public static ParsedResult FromJson(string json)
        {
            json = PrepareJson(json);
            try
            {
                ParsedResult obj = new ParsedResult();
                obj = JsonSerializer.Deserialize<ParsedResult>(json);

                if(obj.Accepted == null || obj.Packages == null || obj.Script == null || obj.Comments == null)
                {
                    obj.IsValid = false;
                    return obj;
                }

                obj.IsValid = true;
                return obj;
            } catch (Exception ex) {
                return new ParsedResult
                {
                    IsValid = false,
                    Comments = ex.Message
                };
            }
        }
    }
}

