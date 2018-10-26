using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TcUnip.Web.WebApiClient
{
    public class WebApiClient : IWebApiClient
    {
        public string baseUri { get; set; }

        private EnumAuthentication authentication;
        private string authToken;
        private string basicPassword;
        private string basicUsername;
        private string clientId;
        private string clientSecret;


        public async Task<T> GetAsync<T>(string action)
        {
            using (var client = new HttpClient())
            {
                //Add the authorization header
                AddAuthorizationHeader(client);
                var result = await client.GetAsync(BuildActionUri(action));

                string json = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    return await Format<T>(json);
                }

                throw new ApiException(result.StatusCode, json);
            }
        }

        public void GetNoWait(string action)
        {
            using (var client = new HttpClient())
            {
                //Add the authorization header
                AddAuthorizationHeader(client);
                var result = client.GetAsync(BuildActionUri(action));
            }
        }


        public async Task PutAsync<T>(string action, T data)
        {
            using (var client = new HttpClient())
            {
                AddAuthorizationHeader(client);

                var result = await client.PutAsJsonAsync(BuildActionUri(action), data);
                if (result.IsSuccessStatusCode)
                {
                    return;
                }

                string json = await result.Content.ReadAsStringAsync();
                throw new ApiException(result.StatusCode, json);
            }
        }

        public async Task PostAsync<T>(string action, T data)
        {
            using (var client = new HttpClient())
            {
                AddAuthorizationHeader(client);

                var result = await client.PostAsJsonAsync(BuildActionUri(action), data);
                if (result.IsSuccessStatusCode)
                {
                    return;
                }

                string json = await result.Content.ReadAsStringAsync();
                throw new ApiException(result.StatusCode, json);
            }
        }

        public async Task<Tout> PostWithReturnAsync<Tin, Tout>(string action, Tin data)
        {
            using (var client = new HttpClient())
            {
                AddAuthorizationHeader(client);

                var result = await client.PostAsJsonAsync(BuildActionUri(action), data);
                string json = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    return (typeof(Tout) != typeof(String)) ? await Format<Tout>(json) : (Tout)(object)json;
                }

                throw new ApiException(result.StatusCode, json);
            }
        }

        private void AddAuthorizationHeader(HttpClient client)
        {
            if (this.authentication == EnumAuthentication.Bearer && !authToken.IsNullOrWhiteSpace())
                client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + authToken);

            if (this.authentication == EnumAuthentication.Basic)
            {
                string svcCredentials = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(basicUsername + ":" + basicPassword));
                client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Basic " + svcCredentials);
            }
        }

        private async Task<T> Format<T>(string json)
        {
            return await Task.Run<T>(() => { return JsonConvert.DeserializeObject<T>(json); });
        }

        private string BuildActionUri(string action)
        {
            return baseUri + action;
        }

        public async Task<T> DeleteAsync<T>(string action)
        {
            using (var client = new HttpClient())
            {
                //Add the authorization header
                AddAuthorizationHeader(client);
                var result = await client.DeleteAsync(BuildActionUri(action));

                string json = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    return await Format<T>(json);
                }

                throw new ApiException(result.StatusCode, json);
            }
        }

        public IWebApiClient SetBearerAuthentication(string authToken)
        {
            this.authToken = authToken;
            this.authentication = EnumAuthentication.Bearer;
            return this;
        }

        public IWebApiClient SetBasicAuthentication()
        {
            this.authentication = EnumAuthentication.Basic;
            return this;
        }

    }
}