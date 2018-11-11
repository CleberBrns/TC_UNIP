using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using TcUnip.Model.Contabil;
using TcUnip.Service.Contract.ServiceApi;
using TcUnip.ServiceApi.Common;

namespace TcUnip.ServiceApi.Contabil
{
    public class CaixaApi : ServiceApiBase<Caixa>, ICaixaApi
    {
        readonly string baseRoute = "fluxo-caixa";
        readonly string baseUrl = ConfigurationManager.AppSettings["BaseUrlApi"];

        public CaixaApi() : base("fluxo-caixa", ConfigurationManager.AppSettings["BaseUrlApi"])
        {            
        }

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

        #endregion

        /// <summary>
        /// https://clinica-unip.herokuapp.com/clinica/agenda/from/12/09/2018/to/12/09/2018
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public List<Caixa> ListCaixaPeriodo(string dateFrom, string dateTo)
        {
            var parametros = string.Format("{0}", "from/" + dateFrom + "/to/" + dateTo);
            string action = string.Format("{0}{1}", baseUrl, baseRoute + "/" + parametros);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, action);
            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            var listModel = new List<Caixa>();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                listModel = JsonConvert.DeserializeObject<List<Caixa>>(response.Content.ReadAsStringAsync().Result);

            return listModel;
        }

    }
}
