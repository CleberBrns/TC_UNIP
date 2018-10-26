using System;
using System.Net;

namespace TcUnip.Web.WebApiClient
{

    public class ApiException : Exception
    {
        public ApiException(HttpStatusCode statusCode, string jsonData)
        {
            StatusCode = statusCode;
            JsonData = jsonData;
        }

        public HttpStatusCode StatusCode { get; private set; }
        public string JsonData { get; private set; }
    }
}