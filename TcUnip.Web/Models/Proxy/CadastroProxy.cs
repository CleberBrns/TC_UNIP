using Nito.AsyncEx;
using System.Collections.Generic;
using TcUnip.Model.Cadastro;
using TcUnip.Model.Common;
using TcUnip.Web.Models.Proxy.Contract;
using TcUnip.Web.WebApiClient;

namespace TcUnip.Web.Models.Proxy
{
    public class CadastroProxy : ICadastroProxy
    {
        IWebApiClient _apiClient;
        readonly string apiRoute = "api/Cadastro/";

        public CadastroProxy(IWebApiClient apiClient)
        {
            this._apiClient = apiClient;
            this._apiClient.baseUri = System.Configuration.ConfigurationManager.AppSettings["tcUnipApi"];
        }

        #region Usuário
        public Result<UsuarioModel> GetUsuario(int id)
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<UsuarioModel>>(
                 $"{apiRoute}GetUsuario/{id}"));
        }

        public Result<UsuarioModel> AutenticaUsuario(UsuarioModel model)
        {
            return AsyncContext.Run(() =>
            _apiClient.PostWithReturnAsync<UsuarioModel, Result<UsuarioModel>>($"{apiRoute}AutenticaUsuario", model));
        }

        public Result<List<UsuarioModel>> ListUsuarios()
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<UsuarioModel>>>($"{apiRoute}ListUsuario"));
        }

        public Result<bool> SalvaUsuario(UsuarioModel model)
        {
            return AsyncContext.Run(() =>
            _apiClient.PostWithReturnAsync<UsuarioModel, Result<bool>>($"{apiRoute}SalvaUsuario", model));
        }

        public Result<bool> ExcluiUsuario(int id)
        {
            return AsyncContext.Run((() => _apiClient.DeleteAsync<Result<bool>>($"{apiRoute}ExcluiUsuario/{id}")));
        }

        #endregion

        #region Paciente

        public Result<PacienteModel> GetPaciente(int id)
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<PacienteModel>>(
                 $"{apiRoute}GetPaciente/{id}"));
        }

        public Result<List<PacienteModel>> ListPaciente()
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<PacienteModel>>>($"{apiRoute}ListPaciente"));
        }

        public Result<bool> SalvaPaciente(PacienteModel model)
        {
            return AsyncContext.Run(() =>
            _apiClient.PostWithReturnAsync<PacienteModel, Result<bool>>($"{apiRoute}SalvaPaciente", model));
        }

        public Result<bool> ExcluiPaciente(int id)
        {
            return AsyncContext.Run((() => _apiClient.DeleteAsync<Result<bool>>($"{apiRoute}ExcluiPaciente/{id}")));
        }

        #endregion

        #region Funcionário

        public Result<FuncionarioModel> GetFuncionario(int id)
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<FuncionarioModel>>(
                 $"{apiRoute}GetFuncionario/{id}"));
        }

        public Result<List<FuncionarioModel>> ListFuncionario()
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<FuncionarioModel>>>($"{apiRoute}ListFuncionario"));
        }

        public Result<List<FuncionarioModel>> ListProfissionais()
        {
            return AsyncContext.Run(() => _apiClient.GetAsync<Result<List<FuncionarioModel>>>($"{apiRoute}ListProfissionais"));
        }

        public Result<bool> SalvaFuncionario(FuncionarioModel model)
        {
            return AsyncContext.Run(() =>
            _apiClient.PostWithReturnAsync<FuncionarioModel, Result<bool>>($"{apiRoute}SalvaFuncionario", model));
        }

        public Result<bool> ExcluiFuncionario(int id)
        {
            return AsyncContext.Run((() => _apiClient.DeleteAsync<Result<bool>>($"{apiRoute}ExcluiFuncionario/{id}")));
        }

        #endregion
    }
}