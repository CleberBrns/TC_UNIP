using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TcUnip.Model.Pessoa;
using TcUnip.Service.Contract.ServiceApi;
using TcUnip.ServiceApi.Common;

namespace TcUnip.ServiceApi.Pessoa
{
    public class PacienteApi : IServiceAPIBase<Paciente>
    {
        readonly string baseRoute = "paciente";

        #region Definições Url

        //Listar todos
        //GET BaseUrl 

        //Listar id(cpf)
        //GET BaseUrl + model/1

        //Apagar um
        //DELETE BaseUrl + model/1

        //Inserir um
        //POST BaseUrl + model

        //Atualizar um por id(cpf)
        //PUT BaseUrl + model/1

        #endregion

        public string BaseUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["BaseUrlApi"];
            }
        }

        public Paciente Get(string cpf)
        {
            string action = string.Format("{0}{1}/{2}", BaseUrl, baseRoute, cpf);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, action);
            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            Paciente model =
               JsonConvert.DeserializeObject<Paciente>(response.Content.ReadAsStringAsync().Result);

            return model;
        }

        public List<Paciente> List()
        {
            string action = BaseUrl + baseRoute;

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, action);
            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            List<Paciente> listModel = 
                JsonConvert.DeserializeObject<List<Paciente>>(response.Content.ReadAsStringAsync().Result);

            return listModel;
        }

        public bool Save(Paciente model)
        {            
            var jsonModel = JsonConvert.SerializeObject(model);            
            var jsonContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");
            string action = string.Format("{0}{1}", BaseUrl, baseRoute);

            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().PostAsync(action, jsonContent).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            return false;
        }

        public bool Update(Paciente model)
        {
            var jsonModel = JsonConvert.SerializeObject(model);
            var jsonContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");
            string action = string.Format("{0}{1}/{2}", BaseUrl, baseRoute, model.Cpf);

            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().PutAsync(action, jsonContent).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            return false;
        }

        public bool Delete(string cpf)
        {
            string action = string.Format("{0}{1}/{2}", BaseUrl, baseRoute, cpf);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, action);
            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            return false;
        }

    }
}