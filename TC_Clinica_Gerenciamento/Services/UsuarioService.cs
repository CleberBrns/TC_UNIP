using System;
using System.Collections.Generic;
using System.Web;
using TCC_Unip.Models.Local;
using TCC_Unip.Models.Servico;
using TCC_Unip.ServicesAPI;

namespace TCC_Unip.Services
{
    public class UsuarioService : Contracts.IServiceUsuario
    {
        UsuarioServiceApi service = new UsuarioServiceApi();

        public ResultService<Usuario> Get(string email)
        {
            var result = new ResultService<Usuario>();

            var retorno = service.Get(email);
            result.value = retorno;

            if (string.IsNullOrEmpty(result.value.Email))
                result.errorMessage = "O Usuário não existe mais na base de dados do serviço!";

            return result;
        }

        public ResultService<List<Usuario>> List(bool getFromSession)
        {
            var result = new ResultService<List<Usuario>>();

            if (getFromSession)
            {
                result.value = GetListFromSession();

                if (result.value == null)
                    result.value = GetFromService();
            }
            else
            {
                var retorno = service.List();
                result.value = retorno;               
            }           

            return result;
        }

        private List<Usuario> GetFromService()
        {
            throw new NotImplementedException();
        }

        public ResultService<bool> Save(Usuario model)
        {
            var result = new ResultService<bool>();

            var registroExistente = service.Get(model.Email);
            if (string.IsNullOrEmpty(registroExistente.Email))
                result = SalvaUsuario(model);
            else
            {
                var retorno = service.Update(model);
                result.value = retorno;

                if (result.value)
                    result.message = "Usuário atualizado com sucesso!";
                else
                    result.errorMessage = "Falha ao atualizar o Usuário!";
            }

            return result;
        }

        public ResultService<bool> Delete(string cpf)
        {
            var result = new ResultService<bool>();

            var retorno = service.Delete(cpf);
            result.value = retorno;

            if (result.value)
                result.message = "Usuário excluído com sucesso!";
            else
                result.errorMessage = "Falha ao excluir o Usuário!";

            return result;
        }

        public ResultService<Usuario> Auth(Usuario model)
        {
            var result = new ResultService<Usuario>();

            var retorno = service.Auth(model);
            result.value = retorno;

            if (string.IsNullOrEmpty(result.value.Email))
                result.errorMessage = "Usuário Inválido ou Senha Incorreta";

            return result;
        }

        #region Métodos Privados

        private void AddListToSession(List<Usuario> list)
        {
            HttpContext.Current.Session[Constants.ConstSessions.listUsuarios] = list;
        }

        private List<Usuario> GetListFromSession()
        {
            if ((List<Usuario>)HttpContext.Current.Session[Constants.ConstSessions.listUsuarios] != null)
                return (List<Usuario>)HttpContext.Current.Session[Constants.ConstSessions.listUsuarios];

            return null;
        }

        private ResultService<bool> SalvaUsuario(Usuario model)
        {
            var result = new ResultService<bool>();
            var retorno = service.Save(model);
            result.value = retorno;

            if (result.value)
                result.message = "Usuário salvo com sucesso!";
            else
                result.errorMessage = "Falha ao salvar o Usuário!";

            return result;
        }

        #endregion

    }
}