using System;
using System.Collections.Specialized;

namespace Common.Logging.Raven
{
    public class RavenLoggingFactoryAdapter : ILoggerFactoryAdapter
    {
        private readonly RavenLog _logger;

        public RavenLoggingFactoryAdapter(NameValueCollection properties)
        {
            var connectionString = properties["connectionString"];
            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("Set connectionString parameter in app.config");

            var logger = properties["loggerName"] ?? "root";

            _logger = new RavenLog(connectionString, logger);
        }

        public ILog GetLogger(Type type)
        {
            return _logger;
        }

        public ILog GetLogger(string name)
        {
            return _logger;
        }
    }
}
