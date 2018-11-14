using Nito.AsyncEx;
using System.Collections.Generic;
using System.Configuration;
using TcUnip.Model.Common;
using TcUnip.Model.Contabil;
using TcUnip.Web.Models.Proxy.Contract;
using TcUnip.Web.WebApiClient;

namespace TcUnip.Web.Models.Proxy
{
    public class ReciboProxy_old : IReciboProxy_old
    {
        IWebApiClient _apiClient;
        readonly string apiRoute = "api/Recibo/";
        readonly ReplacesService replacesService = new ReplacesService();

        public ReciboProxy_old(IWebApiClient apiClient)
        {
            this._apiClient = apiClient;
            this._apiClient.baseUri = ConfigurationManager.AppSettings["tcUnipApi"];
        }

        public Result<Recibo> Get(string id)
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<Recibo>>(
                 $"{apiRoute}GetRecibo/{id}"));
        }

        public Result<List<Recibo>> ListRecibosDoDia()
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<Recibo>>>($"{apiRoute}ListRecibosDoDia"));
        }

        public Result<List<Recibo>> ListRecibosPeriodo(string dateFrom, string dateTo)
        {
            dateFrom = replacesService.ReplaceDateWebToApi(dateFrom, true);
            dateTo = replacesService.ReplaceDateWebToApi(dateTo, true);

            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<Recibo>>>(
                 $"{apiRoute}ListRecibosPeriodo/{dateFrom}/{dateTo}"));
        }

        public Result<List<Recibo>> ListRecibosPeriodoPaciente(string cpf, string dateFrom, string dateTo)
        {
            dateFrom = replacesService.ReplaceDateWebToApi(dateFrom, true);
            dateTo = replacesService.ReplaceDateWebToApi(dateTo, true);

            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<Recibo>>>(
                 $"{apiRoute}ListRecibosPeriodoPaciente/{cpf}/{dateFrom}/{dateTo}"));
        }

        public Result<List<Recibo>> ListRecibosPeriodoFuncionario(string cpf, string dateFrom, string dateTo)
        {
            dateFrom = replacesService.ReplaceDateWebToApi(dateFrom, true);
            dateTo = replacesService.ReplaceDateWebToApi(dateTo, true);

            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<Recibo>>>(
                 $"{apiRoute}ListRecibosPeriodoFuncionario/{cpf}/{dateFrom}/{dateTo}"));
        }
    }
}