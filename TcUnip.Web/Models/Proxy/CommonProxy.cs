using Nito.AsyncEx;
using System.Collections.Generic;
using TcUnip.Model.Cadastro;
using TcUnip.Model.Common;
using TcUnip.Web.Models.Proxy.Contract;
using TcUnip.Web.WebApiClient;

namespace TcUnip.Web.Models.Proxy
{
    public class CommonProxy : ICommonProxy
    {
        IWebApiClient _apiClient;
        readonly string apiRoute = "api/Common/";

        public CommonProxy(IWebApiClient apiClient)
        {
            this._apiClient = apiClient;
            this._apiClient.baseUri = System.Configuration.ConfigurationManager.AppSettings["tcUnipApi"];
        }

        public Result<List<TipoPerfilModel>> ListTipoPerfil()
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<TipoPerfilModel>>>($"{apiRoute}ListTipoPerfil"));
        }

        public Result<List<ModalidadeModel>> ListModalidades()
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<ModalidadeModel>>>($"{apiRoute}ListModalidades"));
        }
    }
}