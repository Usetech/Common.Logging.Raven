using System;
using Newtonsoft.Json;

namespace Common.Logging.Raven.Internal.Data 
{
    public class SentryException 
    {
        /// <summary>
        /// The type of exception.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type;
        /// <summary>
        /// The message of the exception.
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public string Value;
        /// <summary>
        /// The module where the exception happened.
        /// </summary>
        [JsonProperty(PropertyName = "module")]
        public string Module;

        public SentryException(Exception e) 
        {
            Module = e.Source;
            Type = e.GetType().FullName;
            Value = e.Message;
        }
    }
}
