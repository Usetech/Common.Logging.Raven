using Newtonsoft.Json;

namespace Common.Logging.Raven.Internal.Data
{
    public class SentryHttp
    {
        /*
         'sentry.interfaces.Http': {
                'method': request.method,
                'url': uri,
                'query_string': request.META.get('QUERY_STRING'),
                'data': data,
                'cookies': dict(request.COOKIES),
                'headers': dict(get_headers(environ)),
                'env': dict(get_environ(environ)),
            }
         */
        [JsonProperty(PropertyName = "method")]
        public string Method;

        [JsonProperty(PropertyName = "url")]
        public string Url;

        [JsonProperty(PropertyName = "query_string")]
        public string QueryString;
    }
}