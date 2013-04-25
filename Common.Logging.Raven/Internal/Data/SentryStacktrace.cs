using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;

namespace Common.Logging.Raven.Internal.Data
{
    public class SentryStacktrace 
    {
        public SentryStacktrace(Exception e) 
        {
            var trace = new StackTrace(e, true);
            var frames = trace.GetFrames() ?? new StackFrame[0];

            Frames = frames.Reverse().Select(frame =>
            {
                var lineNo = frame.GetFileLineNumber();
                if (lineNo == 0) lineNo = frame.GetILOffset();
                var method = frame.GetMethod();
                
                return new ExceptionFrame
                {
                    FileName = frame.GetFileName(),
                    Module = method.DeclaringType == null ? "" : method.DeclaringType.FullName,
                    Function = method.Name,
                    Source = method.ToString(),
                    LineNumber = lineNo,
                    ColumnNumber = frame.GetFileColumnNumber()
                };
            }).ToList();
        }

        [JsonProperty(PropertyName = "frames")]
        public List<ExceptionFrame> Frames;
    }
}
