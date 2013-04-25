using System;
using Common.Logging.Factory;
using Common.Logging.Raven.Internal;

namespace Common.Logging.Raven
{
    public class RavenLog : AbstractLogger
    {
        private readonly RavenClient _client;

        public RavenLog(string connectionString, string logger)
        {
            _client = new RavenClient(connectionString, logger);
        }

        protected override void WriteInternal(LogLevel level, object message, Exception exception)
        {
            _client.Write(exception, message ?? (exception == null ? null : exception.Message), level);
        }

        public override bool IsTraceEnabled { get { return false; } }
        public override bool IsDebugEnabled { get { return true; } }
        public override bool IsErrorEnabled { get { return true; } }
        public override bool IsFatalEnabled { get { return true; } }
        public override bool IsInfoEnabled { get { return true; } }
        public override bool IsWarnEnabled { get { return true; } }
    }
}
