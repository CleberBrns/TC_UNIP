using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Configuration;
using TcUnip.Model.Common;
using TcUnip.Model.Contabil;
using TcUnip.Web.Models.Proxy.Contract;
using TcUnip.Web.WebApiClient;

namespace TcUnip.Web.Models.Proxy
{
    public class CaixaProxy : ICaixaProxy
    {
        IWebApiClient _apiClient;
        readonly string apiRoute = "api/Caixa/";
        readonly ReplacesService replacesService = new ReplacesService();

        public CaixaProxy(IWebApiClient apiClient)
        {
            this._apiClient = apiClient;
            this._apiClient.baseUri = ConfigurationManager.AppSettings["tcUnipApi"];
        }

        public Result<Caixa> Get(string id)
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<Caixa>>(
                 $"{apiRoute}GetCaixa/{id}"));
        }

        public Result<List<Caixa>> ListCaixaDoDia()
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<Caixa>>>($"{apiRoute}ListCaixaDoDia"));
        }

        public Result<List<Caixa>> ListCaixaPeriodo(string dateFrom, string dateTo)
        {
            dateFrom = replacesService.ReplaceDateWebToApi(dateFrom, true);
            dateTo = replacesService.ReplaceDateWebToApi(dateTo, true);

            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<Caixa>>>(
                 $"{apiRoute}ListCaixaPeriodo/{dateFrom}/{dateTo}"));
        }

        public Result<bool> Salva(Caixa model)
        {
            return AsyncContext.Run(() => _apiClient.PostWithReturnAsync<Caixa, Result<bool>>($"{apiRoute}SalvaCaixa", model));
        }

        public Result<bool> Exclui(string id)
        {
            return AsyncContext.Run((() => _apiClient.DeleteAsync<Result<bool>>($"{apiRoute}ExcluiCaixa/{id}")));
        }
    }
}