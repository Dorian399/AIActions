using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIActions.AI.Results
{
    internal class ParsedResult
    {
        public bool IsValid { get; set; }
        public bool Accepted { get; set; }
        public string[] Packages { get; set; } = Array.Empty<string>();
        public string Script { get; set; }
        public string Comments { get; set; }
        public ParsedResult() { }
    }
}
