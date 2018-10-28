using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TcUnip.Service.Contract.ServiceApi;

namespace TcUnip.ServiceApi.Common
{
    public class ServiceApiBase<TModel> : IServiceApiBase<TModel>
        where TModel : class, new()
    {
        public string _BaseRoute { get; set; }
        public string _BaseUrl { get; set; }

        public ServiceApiBase(string BaseRoute, string BaseUrl) : base()
        {
            this._BaseRoute = BaseRoute;
            this._BaseUrl = BaseUrl;
        }

        public TModel Get(string id)
        {
            string action = string.Format("{0}{1}/{2}", _BaseUrl, _BaseRoute, id);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, action);
            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            var model = new TModel();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                model = JsonConvert.DeserializeObject<TModel>(response.Content.ReadAsStringAsync().Result);

            return model;
        }

        public List<TModel> List()
        {
            string action = _BaseUrl + _BaseRoute;

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, action);
            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            List<TModel> listModel =
                JsonConvert.DeserializeObject<List<TModel>>(response.Content.ReadAsStringAsync().Result);

            return listModel;
        }

        public bool Save(TModel model)
        {
            var jsonModel = JsonConvert.SerializeObject(model);
            var jsonContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");
            string action = string.Format("{0}{1}", _BaseUrl, _BaseRoute);

            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().PostAsync(action, jsonContent).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            return false;
        }

        public bool Update(TModel model, string id)
        {
            var jsonModel = JsonConvert.SerializeObject(model);
            var jsonContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");
            string action = string.Format("{0}{1}/{2}", _BaseUrl, _BaseRoute, id);

            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().PutAsync(action, jsonContent).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            return false;
        }

        public bool Delete(string id)
        {
            string action = string.Format("{0}{1}/{2}", _BaseUrl, _BaseRoute, id);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, action);
            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            return false;
        }
    }
}
