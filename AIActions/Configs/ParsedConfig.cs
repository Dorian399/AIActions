using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIActions.Configs
{
    public class ParsedConfig
    {
        public string Codename { get; set; }
        public string Name { get; set; }
        public string Endpoint { get; set; }
        public Dictionary<string, object> Request { get; set; }
        public string ResponseJsonPath { get; set; }
        public string Type { get; set; }
        public Dictionary<string, string>? Headers { get; set; }

        public ParsedConfig() { }
    }
}
