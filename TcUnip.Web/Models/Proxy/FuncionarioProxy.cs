using Nito.AsyncEx;
using System.Collections.Generic;
using System.Configuration;
using TcUnip.Model.Common;
using TcUnip.Model.Pessoa;
using TcUnip.Web.Models.Proxy.Contract;
using TcUnip.Web.WebApiClient;

namespace TcUnip.Web.Models.Proxy
{
    public class FuncionarioProxy : IFuncionarioProxy
    {
        IWebApiClient _apiClient;
        readonly string apiRoute = "api/Funcionario/";

        public FuncionarioProxy(IWebApiClient apiClient)
        {
            this._apiClient = apiClient;
            this._apiClient.baseUri = ConfigurationManager.AppSettings["tcUnipApi"];
        }

        public Result<Funcionario> Get(string cpf)
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<Funcionario>>($"{apiRoute}Get/{cpf}"));
        }

        public Result<List<Funcionario>> List()
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<Funcionario>>>($"{apiRoute}List"));
        }

        public Result<List<Funcionario>> ListProfissionais()
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<Funcionario>>>($"{apiRoute}ListProfissionais"));
        }

        public Result<bool> Salva(Funcionario model)
        {
            return AsyncContext.Run(() => _apiClient.PostWithReturnAsync<Funcionario, Result<bool>>($"{apiRoute}Salva", model));
        }

        public Result<bool> Exclui(string cpf)
        {
            return AsyncContext.Run((() => _apiClient.DeleteAsync<Result<bool>>($"{apiRoute}Exclui/{cpf}")));
        }
    }
}