using Nito.AsyncEx;
using System.Collections.Generic;
using System.Configuration;
using TcUnip.Model.Common;
using TcUnip.Model.Pessoa;
using TcUnip.Web.Models.Proxy.Contract;
using TcUnip.Web.WebApiClient;

namespace TcUnip.Web.Models.Proxy
{
    public class PacienteProxy : IPacienteProxy
    {
        IWebApiClient _apiClient;
        readonly string apiRoute = "api/Pessoa/";
        ReplacesService replacesService = new ReplacesService();

        public PacienteProxy(IWebApiClient apiClient)
        {
            this._apiClient = apiClient;
            this._apiClient.baseUri = ConfigurationManager.AppSettings["tcUnipApi"];
        }

        public Result<Paciente> Get(string cpf)
        {
            cpf = replacesService.ReplaceCpfEmailWebToApi(cpf, true);
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<Paciente>>($"{apiRoute}GetPaciente/{cpf}"));
        }

        public Result<List<Paciente>> List()
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<Paciente>>>($"{apiRoute}ListPacientes"));
        }

        public Result<bool> Salva(Paciente model)
        {
            return AsyncContext.Run(() => _apiClient.PostWithReturnAsync<Paciente, Result<bool>>($"{apiRoute}SalvaPaciente", model));
        }

        public Result<bool> Exclui(string cpf)
        {
            cpf = replacesService.ReplaceCpfEmailWebToApi(cpf, true);
            return AsyncContext.Run((() => _apiClient.DeleteAsync<Result<bool>>($"{apiRoute}ExcluiPaciente/{cpf}")));
        }
    }
}