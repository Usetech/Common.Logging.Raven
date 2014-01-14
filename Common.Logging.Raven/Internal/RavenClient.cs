using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using Common.Logging.Raven.Internal.Data;
using System.Net;
using System.IO;
using System.Collections.Generic;

namespace Common.Logging.Raven.Internal 
{
    public class RavenClient
    {
        private readonly Dsn _currentDsn;
        public string Logger { get; private set; }

        public RavenClient(string dsn, string logger)
            : this(new Dsn(dsn), logger) { }

        public RavenClient(Dsn dsn, string logger) 
        { 
            _currentDsn = dsn;
            Logger = logger;
        }

        public void Write(Exception exception, object message, LogLevel level, Dictionary<string, string> tags = null, object extra = null)
        {
            string levelValue;
            if (!Levels.TryGetValue(level, out levelValue)) return;

            var packet = new JsonPacket(_currentDsn.ProjectID, exception, Logger, levelValue)
            {
                Message = (message ?? "").ToString(),
                Tags = tags,
                Extra = extra
            };

            if (_currentDsn.IsUDP)
                Udp(packet);
            else
                Send(packet);
        }

        private static readonly Dictionary<LogLevel, string> Levels = new Dictionary<LogLevel, string>
        {
            { LogLevel.Debug, "debug" },
            { LogLevel.Error, "error" },
            { LogLevel.Fatal, "fatal" },
            { LogLevel.Info, "info" },
            { LogLevel.Warn, "warning" }
        };

        private void Send(JsonPacket packet) 
        {
            try
            {
                var body = packet.Serialize();
                var request = (HttpWebRequest)WebRequest.Create(_currentDsn.Url);
                request.Method = "POST";
                request.Accept = "application/json";
                request.ContentType = "application/json; charset=utf-8";
                request.Headers.Add("X-Sentry-Auth", _currentDsn.GetHeader(body));
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                request.UserAgent = Dsn.UserAgent;

                using (var s = request.GetRequestStream())
                using (var sw = new StreamWriter(s)) 
                    sw.Write(body);
                using (var wr = (HttpWebResponse)request.GetResponse())
                    wr.Close();
            } 
            catch (WebException e) 
            {
                string messageBody = null;
                if (e.Response != null)
                {
                    using (var rs = e.Response.GetResponseStream())
                    {
                        if (rs != null)
                            using (var sw = new StreamReader(rs)) messageBody = sw.ReadToEnd();
                    }
                }
                else
                {
                    messageBody = e.Message;
                }

                if (messageBody != null)
                    Trace.WriteLine("[MESSAGE BODY] " + messageBody);
            }
        }

        private void Udp(JsonPacket packet)
        {
            var body = packet.Serialize();
            var bytes = Encoding.UTF8.GetBytes("X-Sentry-Auth: " + _currentDsn.GetHeader(body) + "\n\n" + body);

            var client = new UdpClient(_currentDsn.Port);
            try
            {
                client.Connect(_currentDsn.Host, _currentDsn.Port);
                client.Send(bytes, bytes.Length);
                client.Close();
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }
    }
}
