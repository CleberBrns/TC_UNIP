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
        readonly string apiRoute = "api/Pessoa/";
        ReplacesService replacesService = new ReplacesService();

        public UsuarioProxy(IWebApiClient apiClient)
        {
            this._apiClient = apiClient;
            this._apiClient.baseUri = ConfigurationManager.AppSettings["tcUnipApi"];
        }

        public Result<Usuario> Get(string email)
        {
            email = replacesService.ReplaceCpfEmailWebToApi(email, true);
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<Usuario>>($"{apiRoute}GetUsuario/{email}"));
        }

        public Result<List<Usuario>> List()
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<Usuario>>>($"{apiRoute}ListUsuarios"));
        }

        public Result<bool> Salva(Usuario model)
        {
            return AsyncContext.Run(() => _apiClient.PostWithReturnAsync<Usuario, Result<bool>>($"{apiRoute}SalvaUsuario", model));
        }

        public Result<bool> Excliu(string email)
        {
            email = replacesService.ReplaceCpfEmailWebToApi(email, true);
            return AsyncContext.Run((() => _apiClient.DeleteAsync<Result<bool>>($"{apiRoute}ExcluiUsuario/{email}")));
        }

        public Result<Usuario> Autentica(Usuario model)
        {
            return AsyncContext.Run(() => _apiClient.PostWithReturnAsync<Usuario, Result<Usuario>>($"{apiRoute}AutenticaUsuario", model));
        }
    }
}