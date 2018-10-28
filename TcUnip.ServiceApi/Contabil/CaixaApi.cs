using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using TcUnip.Model.Contabil;
using TcUnip.Service.Contract.ServiceApi;
using TcUnip.ServiceApi.Common;

namespace TcUnip.ServiceApi.Contabil
{
    public class CaixaApi : IServiceAPIBase<Caixa>
    {
        #region Definições Url

        //Seleciona by id
        //GET BaseUrl + baseRoute/1

        //Listar todos
        //GET BaseUrl  baseRoute

        //Apagar um
        //DELETE BaseUrl + baseRoute/1

        //Inserir um
        //POST BaseUrl + baseRoute

        //Atualizar um por id
        //PUT BaseUrl + baseRoute/1

        //Atualizando o Status de um Agendamento
        //POST BaseUrl + baseRoute1/status/OK(ConstStatus Agendamento)

        #endregion

        readonly string baseRoute = "fluxo-caixa";

        public string BaseUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["BaseUrlApi"];
            }
        }

        public Caixa Get(string id)
        {
            string action = string.Format("{0}{1}/{2}", BaseUrl, baseRoute, id);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, action);
            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            var model = new Caixa();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                model = JsonConvert.DeserializeObject<Caixa>(response.Content.ReadAsStringAsync().Result);

            return model;
        }

        public List<Caixa> List()
        {
            throw new System.NotImplementedException();
        }

        public bool Save(Caixa model)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(Caixa model)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateStatus(string id, string constStatus)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
