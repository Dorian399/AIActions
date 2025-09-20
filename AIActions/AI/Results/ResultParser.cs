using Json.Path;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace AIActions.AI.Results
{
    internal class ResultParser
    {
        private static string PrepareJson(string json)
        {
            int start = json.IndexOf('{');
            int end = json.LastIndexOf('}');

            if (start == -1 || end == -1 || end < start)
                return string.Empty;

            return json.Substring(start, end - start + 1);
        }

        public static ParsedResult FromJson(string json,string jsonPath)
        {
            json = PrepareJson(json);
            try
            {
                // Parse JsonPath.
                JsonPath path = JsonPath.Parse(jsonPath);
                JsonNode instance = JsonNode.Parse(json);
                PathResult jsonPathResults = path.Evaluate(instance);

                string jsonPathed = "";
                foreach (Node i in jsonPathResults.Matches)
                {
                    jsonPathed += i.Value;
                }

                // Parse results.

                json = PrepareJson(jsonPathed);

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                ParsedResult obj = new ParsedResult();
                obj = JsonSerializer.Deserialize<ParsedResult>(json,options);

                if(obj.Accepted == null || obj.Script == null || obj.Comments == null)
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

