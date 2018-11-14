using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using TcUnip.Model.Agenda;
using TcUnip.Model.Common;
using TcUnip.Web.Models.Proxy.Contract;
using TcUnip.Web.WebApiClient;

namespace TcUnip.Web.Models.Proxy
{
    public class AgendaProxy : IAgendaProxy
    {
        IWebApiClient _apiClient;
        readonly string apiRoute = "api/Calendario/";

        public AgendaProxy(IWebApiClient apiClient)
        {
            this._apiClient = apiClient;
            this._apiClient.baseUri = System.Configuration.ConfigurationManager.AppSettings["tcUnipApi"];
        }

        public Result<SessaoModel> Get(int id)
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<SessaoModel>>(
                 $"{apiRoute}GetAgenda/{id}"));
        }

        public Result<List<SessaoModel>> ListAgendaDoDia()
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<SessaoModel>>>($"{apiRoute}ListAgendaDoDia"));
        }

        public Result<List<SessaoModel>> ListAgendaPeriodo(PesquisaModel pesquisaModel)
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<SessaoModel>>>(
                 $"{apiRoute}ListAgendaPeriodo/{pesquisaModel}"));
        }

        public Result<List<SessaoModel>> ListAgendaPeriodoPaciente(PesquisaModel pesquisaModel)
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<SessaoModel>>>(
                 $"{apiRoute}ListAgendaPeriodoPaciente/{pesquisaModel}"));
        }

        public Result<List<SessaoModel>> ListAgendaPeriodoFuncionario(PesquisaModel pesquisaModel)
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<SessaoModel>>>(
                 $"{apiRoute}ListAgendaPeriodoFuncionario/{pesquisaModel}"));
        }

        public Result<bool> Salva(SessaoModel model)
        {
            return AsyncContext.Run(() => 
            _apiClient.PostWithReturnAsync<SessaoModel, Result<bool>>($"{apiRoute}SalvaAgenda", model));
        }

        public Result<bool> Exclui(int id)
        {
            return AsyncContext.Run((() => _apiClient.DeleteAsync<Result<bool>>($"{apiRoute}ExcluiAgenda/{id}")));
        }
    }
}