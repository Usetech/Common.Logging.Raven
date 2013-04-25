using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Logging.Raven.Internal.Data
{
    public class ExceptionFrame
    {
        [JsonProperty("abs_path")]
        public string AbsolutePath;

        [JsonProperty("filename")]
        public string FileName;

        [JsonProperty("module")]
        public string Module;

        [JsonProperty("function")]
        public string Function;

        [JsonProperty("vars")]
        public Dictionary<string, string> Variables;

        [JsonProperty("pre_context")]
        public List<string> PreContext;

        [JsonProperty("context_line")]
        public string Source;

        [JsonProperty("lineno")]
        public int LineNumber;

        [JsonProperty("colno")]
        public int ColumnNumber;

        [JsonProperty("in_app")]
        public bool InApp;

        [JsonProperty("post_context")]
        public List<string> PostContext;
    }
}
