using Nito.AsyncEx;
using System.Collections.Generic;
using System.Configuration;
using TcUnip.Model.Common;
using TcUnip.Model.Pessoa;
using TcUnip.Web.Models.Proxy.Contract;
using TcUnip.Web.WebApiClient;

namespace TcUnip.Web.Models.Proxy
{
    public class UsuarioProxy : IUsuarioProxy
    {
        IWebApiClient _apiClient;
        readonly string apiRoute = "api/Usuario/";

        public UsuarioProxy(IWebApiClient apiClient)
        {
            this._apiClient = apiClient;
            this._apiClient.baseUri = ConfigurationManager.AppSettings["tcUnipApi"];
        }

        public Result<Usuario> Get(string email)
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<Usuario>>($"{apiRoute}Get/{email}"));
        }

        public Result<List<Usuario>> List()
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<Usuario>>>($"{apiRoute}List"));
        }

        public Result<bool> Salva(Usuario model)
        {
            return AsyncContext.Run(() => _apiClient.PostWithReturnAsync<Usuario, Result<bool>>($"{apiRoute}Salva", model));
        }

        public Result<bool> Excliu(string email)
        {
            return AsyncContext.Run((() => _apiClient.DeleteAsync<Result<bool>>($"{apiRoute}Exclui/{email}")));
        }

        public Result<Usuario> Autentica(Usuario model)
        {
            return AsyncContext.Run(() => _apiClient.PostWithReturnAsync<Usuario, Result<Usuario>>($"{apiRoute}Autentica", model));
        }
    }
}