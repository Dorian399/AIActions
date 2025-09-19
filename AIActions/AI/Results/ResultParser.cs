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

        private class ResultParserDto
        {
            public bool accepted { get; set; }
            public string[] packages { get; set; }
            public string script { get; set; }
            public string comments { get; set; }
        }

        private ResultParser() { }

        private static string PrepareJson(string json)
        {
            int start = json.IndexOf('{');
            int end = json.LastIndexOf('}');

            if (start == -1 || end == -1 || end < start)
                return string.Empty;

            return json.Substring(start, end - start + 1);
        }

        public static ResultParser FromJson(string json)
        {
            json = PrepareJson(json);
            try
            {
                var obj = JsonSerializer.Deserialize<ResultParserDto>(json);

                if(obj.accepted == null || obj.packages == null || obj.script == null || obj.comments == null)
                {
                    return new ResultParser
                    {
                        IsValid = false,
                        Comments = "Could not perform action, try again using a different prompt."
                    };
                }

                return new ResultParser
                {
                    IsValid = true,
                    Accepted = obj.accepted,
                    Packages = obj.packages,
                    Script = obj.script,
                    Comments = obj.comments
                };
            } catch (Exception ex) {
                return new ResultParser
                {
                    IsValid = false,
                    Comments = ex.Message
                };
            }
        }
    }
}

