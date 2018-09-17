using System.Collections.Generic;
using TC_Clinica_Gerenciamento.Models.Local;
using TC_Clinica_Gerenciamento.Models.Servico;
using TC_Clinica_Gerenciamento.ServicesAPI;

namespace TC_Clinica_Gerenciamento.Services
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

        public ResultService<List<Usuario>> List()
        {
            var result = new ResultService<List<Usuario>>();

            var retorno = service.List();
            result.value = retorno;

            return result;
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
                result.message = "Usuário Inválido ou Senha Incorreta";

            return result;
        }

        #region Métodos Privados

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