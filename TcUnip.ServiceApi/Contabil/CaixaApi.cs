using System.Configuration;
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

        //Atualizando o Status de um Agendamento
        //POST BaseUrl + baseRoute1/status/OK(ConstStatus Agendamento)

        #endregion

        //readonly string baseRoute = "fluxo-caixa";

    }
}
