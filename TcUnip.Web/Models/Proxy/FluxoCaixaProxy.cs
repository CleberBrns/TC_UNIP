using Nito.AsyncEx;
using System.Collections.Generic;
using TcUnip.Model.Common;
using TcUnip.Model.FluxoCaixa;
using TcUnip.Web.Models.Proxy.Contract;
using TcUnip.Web.WebApiClient;

namespace TcUnip.Web.Models.Proxy
{
    public class FluxoCaixaProxy : IFluxoCaixaProxy
    {
        IWebApiClient _apiClient;
        readonly string apiRoute = "api/FluxoCaixa/";

        public FluxoCaixaProxy(IWebApiClient apiClient)
        {
            this._apiClient = apiClient;
            this._apiClient.baseUri = System.Configuration.ConfigurationManager.AppSettings["tcUnipApi"];
        }

        #region Caixa
        public Result<CaixaModel> GetCaixa(int id)
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<CaixaModel>>(
                 $"{apiRoute}GetCaixa/{id}"));
        }

        public Result<List<CaixaModel>> ListCaixaDoDia()
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<CaixaModel>>>($"{apiRoute}ListCaixaDoDia"));
        }

        public Result<List<CaixaModel>> ListCaixaPeriodo(PesquisaModel pesquisaModel)
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<CaixaModel>>>(
                             $"{apiRoute}ListCaixaPeriodo/{pesquisaModel}"));
        }

        public Result<bool> SalvaCaixa(CaixaModel model)
        {
            return AsyncContext.Run(() =>
            _apiClient.PostWithReturnAsync<CaixaModel, Result<bool>>($"{apiRoute}SalvaCaixa", model));
        }

        public Result<bool> ExcluiCaixa(int id)
        {
            return AsyncContext.Run((() => _apiClient.DeleteAsync<Result<bool>>($"{apiRoute}ExcluiCaixa/{id}")));
        }

        #endregion

        #region Recibos

        public Result<ReciboModel> GetRecibo(int id)
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<ReciboModel>>(
                 $"{apiRoute}GetRecibo/{id}"));
        }
        public Result<List<ReciboModel>> ListRecibosDoDia()
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<ReciboModel>>>($"{apiRoute}ListRecibosDoDia"));
        }

        public Result<List<ReciboModel>> ListRecibosPeriodo(PesquisaModel pesquisaModel)
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<ReciboModel>>>(
                             $"{apiRoute}ListRecibosPeriodo/{pesquisaModel}"));
        }

        public Result<List<ReciboModel>> ListRecibosPeriodoPaciente(PesquisaModel pesquisaModel)
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<ReciboModel>>>(
                             $"{apiRoute}ListRecibosPeriodoPaciente/{pesquisaModel}"));
        }

        public Result<List<ReciboModel>> ListRecibosPeriodoFuncionario(PesquisaModel pesquisaModel)
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<ReciboModel>>>(
                             $"{apiRoute}ListRecibosPeriodoFuncionario/{pesquisaModel}"));
        }

        #endregion

    }
}