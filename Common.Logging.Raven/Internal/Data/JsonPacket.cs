using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Common.Logging.Raven.Internal.Data 
{
    public class JsonPacket 
    {
        /// <summary>
        /// Hexadecimal string representing a uuid4 value.
        /// </summary>
        [JsonProperty("event_id", NullValueHandling = NullValueHandling.Ignore)]
        public string EventID { get; set; }

        /// <summary>
        /// String value representing the project
        /// </summary>
        [JsonProperty("project", NullValueHandling = NullValueHandling.Ignore)]
        public string Project { get; set; }

        /// <summary>
        /// Function call which was the primary perpetrator of this event.
        /// A map or list of tags for this event.
        /// </summary>
        [JsonProperty("culprit", NullValueHandling = NullValueHandling.Ignore)]
        public string Culprit { get; set; }

        /// <summary>
        /// The record severity.
        /// Defaults to error.
        /// </summary>
        [JsonProperty("level", NullValueHandling = NullValueHandling.Ignore)]
        public string Level { get; set; }

        /// <summary>
        /// Indicates when the logging record was created (in the Sentry client).
        /// Defaults to DateTime.UtcNow()
        /// </summary>
        [JsonProperty("timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// The name of the logger which created the record.
        /// If missing, defaults to the string root.
        /// 
        /// Ex: "my.logger.name"
        /// </summary>
        [JsonProperty("logger", NullValueHandling = NullValueHandling.Ignore)]
        public string Logger { get; set; }

        /// <summary>
        /// A string representing the platform the client is submitting from. 
        /// This will be used by the Sentry interface to customize various components in the interface.
        /// </summary>
        [JsonProperty("platform", NullValueHandling = NullValueHandling.Ignore)]
        public string Platform { get; set; }

        /// <summary>
        /// User-readable representation of this event
        /// </summary>
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        /// <summary>
        /// A map or list of tags for this event.
        /// </summary>
        [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Tags;

        /// <summary>
        /// An arbitrary mapping of additional metadata to store with the event.
        /// </summary>
        [JsonProperty("extra", NullValueHandling = NullValueHandling.Ignore)]
        public object Extra;

        /// <summary>
        /// Identifies the host client from which the event was recorded.
        /// </summary>
        [JsonProperty("server_name", NullValueHandling = NullValueHandling.Ignore)]
        public string ServerName { get; set; }

        [JsonProperty(PropertyName="sentry.interfaces.Exception", NullValueHandling=NullValueHandling.Ignore)]
        public SentryException Exception { get; set; }

        [JsonProperty("sentry.interfaces.Stacktrace", NullValueHandling = NullValueHandling.Ignore)]
        public SentryStacktrace StackTrace { get; set; }

        public JsonPacket(string project, Exception e, string logger, string level)
        {
            ServerName = Environment.MachineName;
            TimeStamp = DateTime.UtcNow;
            Logger = logger;
            Level = level;
            EventID = Guid.NewGuid().ToString().Replace("-", String.Empty);
            Project = project;
            Platform = "csharp";
            
            if (e == null) return;

            Message = e.Message;
            if (e.TargetSite != null) Culprit = String.Format("{0} in {1}", e.TargetSite.ReflectedType.FullName, e.TargetSite.Name);
            Exception = new SentryException(e) { Module = e.Source, Type = e.GetType().Name, Value = e.Message };
            StackTrace = new SentryStacktrace(e);
            if (StackTrace.Frames.Count == 0) StackTrace = null;
        }

        /// <summary>
        /// Turn into a JSON string.
        /// </summary>
        /// <returns>json string</returns>
        public string Serialize() 
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
