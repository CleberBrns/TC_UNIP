using System.Net;
using System.Net.Http;

namespace TcUnip.ServiceApi.Common
{
    public class HttpInstance
    {
        private static HttpClient httpClientInstance;

        private HttpInstance()
        {
        }

        public static HttpClient GetHttpClientInstance()
        {
            if (httpClientInstance == null)
            {
                var httpClientHandler = GetHttpClientHandler();
                httpClientInstance = new HttpClient(httpClientHandler);
                httpClientInstance.DefaultRequestHeaders.ConnectionClose = false;
            }
            return httpClientInstance;
        }

        private static HttpClientHandler GetHttpClientHandler()
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                Proxy = WebRequest.DefaultWebProxy
            };
            handler.Proxy.Credentials = CredentialCache.DefaultCredentials;

            return handler;
        }
    }
}