using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using TcUnip.Model.Pessoa;
using TcUnip.Service.Contract.ServiceApi;
using TcUnip.ServiceApi.Common;

namespace TcUnip.ServiceApi.Pessoa
{
    public class UsuarioApi : ServiceApiBase<Usuario>, IUsuarioApi
    {
        readonly string baseRoute = "usuario";
        readonly string baseUrl = ConfigurationManager.AppSettings["BaseUrlApi"];

        public UsuarioApi() : base("usuario", ConfigurationManager.AppSettings["BaseUrlApi"])
        {
        }       

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

        public Tuple<Usuario, bool> Auth(Usuario model)
        {
            var statusAuth = false;

            var tipoAcao = "auth";
            var objAuth = new { email = model.Email, senha = model.Senha };
            var jsonModel = JsonConvert.SerializeObject(objAuth);
            var jsonContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");

            string action = string.Format("{0}{1}", baseUrl, baseRoute + "/" + tipoAcao);
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