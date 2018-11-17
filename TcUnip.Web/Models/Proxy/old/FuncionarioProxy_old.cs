﻿using Nito.AsyncEx;
using System.Collections.Generic;
using System.Configuration;
using TcUnip.Model.Common;
using TcUnip.Model.Pessoa;
using TcUnip.Web.Models.Proxy.Contract;
using TcUnip.Web.WebApiClient;

namespace TcUnip.Web.Models.Proxy
{
    public class FuncionarioProxy_old : IFuncionarioProxy_old
    {
        IWebApiClient _apiClient;
        readonly string apiRoute = "api/Pessoa/";
        ReplacesService replacesService = new ReplacesService();

        public FuncionarioProxy_old(IWebApiClient apiClient)
        {
            this._apiClient = apiClient;
            this._apiClient.baseUri = ConfigurationManager.AppSettings["tcUnipApi"];
        }

        public Result<Funcionario> Get(string cpf)
        {
            cpf = replacesService.ReplaceCpfEmailWebToApi(cpf, true);
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<Funcionario>>($"{apiRoute}GetFuncionario/{cpf}"));
        }

        public Result<List<Funcionario>> List()
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<Funcionario>>>($"{apiRoute}ListFuncionarios"));
        }

        public Result<List<Funcionario>> ListProfissionais()
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<Funcionario>>>($"{apiRoute}ListProfissionais"));
        }

        public Result<bool> Salva(Funcionario model)
        {
            return AsyncContext.Run(() => _apiClient.PostWithReturnAsync<Funcionario, Result<bool>>($"{apiRoute}SalvaFuncionario", model));
        }

        public Result<bool> Exclui(string cpf)
        {
            cpf = replacesService.ReplaceCpfEmailWebToApi(cpf, true);
            return AsyncContext.Run((() => _apiClient.DeleteAsync<Result<bool>>($"{apiRoute}ExcluiFuncionario/{cpf}")));
        }
    }
}