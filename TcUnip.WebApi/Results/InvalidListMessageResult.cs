using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace TcUnip.WebApi.Results
{
    public class InvalidListMessageResult : IHttpActionResult
    {
        private string[] messages;
        private HttpStatusCode httpCode;
        public InvalidListMessageResult(ModelStateDictionary dictionary, HttpStatusCode code = HttpStatusCode.BadRequest)
        {
            List<string> str = new List<string>();

            if (dictionary.IsValid == false)
            {
                foreach (var dic in dictionary.Values)
                {
                    foreach (var erro in dic.Errors)
                    {
                        str.Add(erro.ErrorMessage);
                    }
                }
            }
            messages = str.ToArray();
            httpCode = code;
        }

        public InvalidListMessageResult(string msg = "", HttpStatusCode code = HttpStatusCode.BadRequest)
        {
            List<string> str = new List<string>();

            if (!string.IsNullOrEmpty(msg))
            {
                str.Add(msg);
            }
            messages = str.ToArray();
            httpCode = code;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(httpCode);
            response.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(messages.ToArray()));
            return Task.FromResult(response);
        }
    }
}