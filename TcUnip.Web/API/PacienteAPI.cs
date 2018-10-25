using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TcUnip.Web.Contracts.API;
using TcUnip.Web.Models.Servico;

namespace TcUnip.Web.API
{
    public class PacienteAPI : IAPIBase<Paciente>
    {
        readonly string tipoModel = "paciente";

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
            string action = string.Format("{0}{1}/{2}", BaseUrl, tipoModel, cpf);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, action);
            HttpResponseMessage response = Tools.HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            Paciente model =
               JsonConvert.DeserializeObject<Paciente>(response.Content.ReadAsStringAsync().Result);

            return model;
        }

        public List<Paciente> List()
        {
            string action = BaseUrl + tipoModel;

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, action);
            HttpResponseMessage response = Tools.HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            List<Paciente> listModel = 
                JsonConvert.DeserializeObject<List<Paciente>>(response.Content.ReadAsStringAsync().Result);

            return listModel;
        }

        public bool Save(Paciente model)
        {            
            var jsonModel = JsonConvert.SerializeObject(model);            
            var jsonContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");
            string action = string.Format("{0}{1}", BaseUrl, tipoModel);

            HttpResponseMessage response = Tools.HttpInstance.GetHttpClientInstance().PostAsync(action, jsonContent).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            return false;
        }

        public bool Update(Paciente model)
        {
            var jsonModel = JsonConvert.SerializeObject(model);
            var jsonContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");
            string action = string.Format("{0}{1}/{2}", BaseUrl, tipoModel, model.Cpf);

            HttpResponseMessage response = Tools.HttpInstance.GetHttpClientInstance().PutAsync(action, jsonContent).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            return false;
        }

        public bool Delete(string cpf)
        {
            string action = string.Format("{0}{1}/{2}", BaseUrl, tipoModel, cpf);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, action);
            HttpResponseMessage response = Tools.HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            return false;
        }

    }
}