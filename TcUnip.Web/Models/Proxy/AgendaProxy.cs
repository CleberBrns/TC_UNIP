using Nito.AsyncEx;
using System.Collections.Generic;
using System.Configuration;
using TcUnip.Model.Calendario;
using TcUnip.Model.Common;
using TcUnip.Model.Pessoa;
using TcUnip.Web.Models.Proxy.Contract;
using TcUnip.Web.WebApiClient;

namespace TcUnip.Web.Models.Proxy
{
    public class AgendaProxy : IAgendaProxy
    {
        IWebApiClient _apiClient;
        readonly string apiRoute = "api/Agenda/";

        public AgendaProxy(IWebApiClient apiClient)
        {
            this._apiClient = apiClient;
            this._apiClient.baseUri = ConfigurationManager.AppSettings["tcUnipApi"];
        }

        public Result<Agenda> GetAgenda(string id)
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<Agenda>>(
                 $"{apiRoute}GetAgenda/{id}"));
        }

        public Result<List<Agenda>> ListAgendaDoDia()
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<Agenda>>>($"{apiRoute}ListAgendaDoDia"));
        }

        public Result<List<Agenda>> ListAgendaPeriodo(string dateFrom, string dateTo)
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<Agenda>>>(
                 $"{apiRoute}ListAgendaPeriodo/{dateFrom}/{dateTo}"));
        }

        public Result<Funcionario> ConsultasPeriodoFuncionario(string cpf, string dateFrom, string dateTo)
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<Funcionario>>(
                 $"{apiRoute}ConsultasPeriodoFuncionario/{cpf}/{dateFrom}/{dateTo}"));
        }

        public Result<Paciente> ConsultasPeriodoPaciente(string cpf, string dateFrom, string dateTo)
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<Paciente>>(
                 $"{apiRoute}ConsultasPeriodoPaciente/{cpf}/{dateFrom}/{dateTo}"));
        }

        public Result<bool> SalvaAgenda(Agenda model)
        {
            return AsyncContext.Run(() => 
            _apiClient.PostWithReturnAsync<Agenda, Result<bool>>
            ($"{apiRoute}SalvaAgenda", model));
        }

        public Result<bool> ExcluiAgenda(string id)
        {
            return AsyncContext.Run((() => _apiClient.DeleteAsync<Result<bool>>($"{apiRoute}ExcluiAgenda/{id}")));
        }
    }
}