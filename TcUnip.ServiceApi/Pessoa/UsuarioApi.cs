using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TcUnip.Model.Pessoa;
using TcUnip.Service.Contract.ServiceApi;
using TcUnip.ServiceApi.Common;

namespace TcUnip.ServiceApi.Pessoa
{
    public class UsuarioApi : IServiceAPIBase<Usuario>
    {
        readonly string tipoModel = "usuario";

        #region Definições Url

        //Listar todos
        //GET BaseUrl 

        //Listar id(email)
        //GET BaseUrl + model/1

        //Apagar um
        //DELETE BaseUrl + model/1

        //Inserir um
        //POST BaseUrl + model

        //Atualizar um por id(email)
        //PUT BaseUrl + model/1

        #endregion

        public string BaseUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["BaseUrlApi"];
            }
        }

        public Usuario Get(string email)
        {
            string action = string.Format("{0}{1}/{2}", BaseUrl, tipoModel, email);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, action);
            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            Usuario model =
               JsonConvert.DeserializeObject<Usuario>(response.Content.ReadAsStringAsync().Result);

            return model;
        }

        public List<Usuario> List()
        {
            string action = BaseUrl + tipoModel;

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, action);
            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            List<Usuario> listModel = 
                JsonConvert.DeserializeObject<List<Usuario>>(response.Content.ReadAsStringAsync().Result);

            return listModel;
        }

        public bool Save(Usuario model)
        {            
            var jsonModel = JsonConvert.SerializeObject(model);            
            var jsonContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");
            string action = string.Format("{0}{1}", BaseUrl, tipoModel);

            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().PostAsync(action, jsonContent).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            return false;
        }

        public bool Update(Usuario model)
        {
            var jsonModel = JsonConvert.SerializeObject(model);
            var jsonContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");
            string action = string.Format("{0}{1}/{2}", BaseUrl, tipoModel, model.Email);

            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().PutAsync(action, jsonContent).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            return false;
        }

        public bool Delete(string email)
        {
            string action = string.Format("{0}{1}/{2}", BaseUrl, tipoModel, email);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, action);
            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            return false;
        }

        public Tuple<Usuario, bool> Auth(Usuario model)
        {
            var statusAuth = false;

            var tipoAcao = "auth";
            var objAuth = new { email = model.Email, senha = model.Senha };
            var jsonModel = JsonConvert.SerializeObject(objAuth);
            var jsonContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");

            string action = string.Format("{0}{1}", BaseUrl, tipoModel + "/" + tipoAcao);
            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().PostAsync(action, jsonContent).Result;

            model = new Usuario();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                statusAuth = true;
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);

                var objUsuario = jsonObject.Property("usuario").Value;
                if (objUsuario.HasValues)
                {
                    var usuario = (JObject)(jsonObject.Property("usuario").Value);
                    model = usuario.ToObject<Usuario>();
                }                

                var objFuncionario = jsonObject.Property("funcionario").Value;
                if (objFuncionario.HasValues)
                {
                    var funcionario = (JObject)(objFuncionario);
                    model.Funcionario = funcionario.ToObject<Funcionario>();
                }               
            }

            return new Tuple<Usuario, bool>(model, statusAuth);
        }

    }
}