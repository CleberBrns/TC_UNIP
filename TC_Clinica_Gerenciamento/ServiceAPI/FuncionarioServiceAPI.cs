using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TC_Clinica_Gerenciamento.Models.Servico;

namespace TC_Clinica_Gerenciamento.ServicesAPI
{
    public class FuncionarioServiceApi : Contracts.IServiceApiBase<Funcionario>
    {
        readonly string tipoModel = "funcionario";

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

        public Funcionario Get(string cpf)
        {
            string action = string.Format("{0}{1}/{2}", BaseUrl, tipoModel, cpf);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, action);
            HttpResponseMessage response = Tools.HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            Funcionario model =
               JsonConvert.DeserializeObject<Funcionario>(response.Content.ReadAsStringAsync().Result);

            return model;
        }

        public List<Funcionario> List()
        {
            string action = BaseUrl + tipoModel;

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, action);
            HttpResponseMessage response = Tools.HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            List<Funcionario> listModel = 
                JsonConvert.DeserializeObject<List<Funcionario>>(response.Content.ReadAsStringAsync().Result);

            return listModel;
        }

        public bool Save(Funcionario model)
        {            
            var jsonModel = JsonConvert.SerializeObject(model);            
            var jsonContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");
            string action = string.Format("{0}{1}", BaseUrl, tipoModel);

            HttpResponseMessage response = Tools.HttpInstance.GetHttpClientInstance().PostAsync(action, jsonContent).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            return false;
        }

        public bool Update(Funcionario model)
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