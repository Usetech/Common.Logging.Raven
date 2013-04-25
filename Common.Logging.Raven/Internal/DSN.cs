using System;
using System.Security.Cryptography;
using System.Text;

namespace Common.Logging.Raven.Internal
{
    public class Dsn 
    {
        private readonly string _publicKey;
        private readonly string _privateKey;
        private readonly string _path;
        public int Port { get; private set; }

        public string Url { get; private set; }
        public string ProjectID { get; private set; }
        public bool IsUDP { get; private set; }
        public string Host { get; private set; }

        public const string UserAgent = "Common.Logging.Raven/1.0";

        public Dsn(string dsn) 
        {
            var uri = new Uri(dsn);
            var useSSl = dsn.StartsWith("https://");
            IsUDP = dsn.StartsWith("udp://");
            _privateKey = uri.UserInfo.Split(':')[1];
            _publicKey = uri.UserInfo.Split(':')[0];
            Port = uri.Port;
            Host = uri.DnsSafeHost;
            ProjectID = uri.AbsoluteUri.Substring(uri.AbsoluteUri.LastIndexOf("/") + 1);
            _path = uri.AbsolutePath.Substring(0, uri.AbsolutePath.LastIndexOf("/"));

            Url = string.Format(@"{0}://{1}:{2}{3}/api/{4}/store/", 
                useSSl ? "https" : (IsUDP ? "udp" : "http"),
                Host,
                Port,
                _path,
                ProjectID);
        }

        public string GetHeader(string body)
        {
            var timestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            var secretKey = new HMACSHA1(Encoding.UTF8.GetBytes(_privateKey));
            var hash = secretKey.ComputeHash(Encoding.UTF8.GetBytes(timestamp + " " + body));
            var signature = BitConverter.ToString(hash).ToLower().Replace("-", "");

            return string.Format("Sentry sentry_version=2.0, sentry_signature={0}, sentry_timestamp={1}, sentry_key={2}, sentry_client={3}",
                signature, timestamp, _publicKey, UserAgent);
        }
    }
}
