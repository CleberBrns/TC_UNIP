using System;
using System.Collections.Generic;
using System.Linq;
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

            var retornoSession = GetFromSession(email);

            if (retornoSession.Item2 && !string.IsNullOrEmpty(retornoSession.Item1.Email))
                result.value = retornoSession.Item1;
            else
                result.value = service.Get(email);

            if (string.IsNullOrEmpty(result.value.Email))
                result.errorMessage = "O Usuário não existe mais na base de dados do serviço!";

            return result;
        }

        public ResultService<List<Usuario>> List(bool getFromSession)
        {
            var result = new ResultService<List<Usuario>>();

            if (getFromSession)
            {
                var retornoSession = GetListFromSession();
                /*Verifica se existia dados na session e se a mesma era válida.
                  Caso a mesma seja válida é passado para o retorno da pesquisa, mesmo que esteja vazia.
                  Caso não esteja criada, a busca é feita no serviço.*/
                if (retornoSession.Item2)                
                    result.value = retornoSession.Item1;                
                else
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
            var list = service.List();
            AddListToSession(list);
            return list;
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

        private Tuple<Usuario, bool> GetFromSession(string email)
        {
            var sessionValida = false;
            var model = new Usuario();

            var retornolistFromSession = GetListFromSession();

            if (retornolistFromSession.Item2 && retornolistFromSession.Item1.Count > 0)
            {
                sessionValida = true;
                model = retornolistFromSession.Item1.Where(l => l.Email.Equals(email)).FirstOrDefault();
            }

            return new Tuple<Usuario, bool>(model, sessionValida);
        }

        private void AddListToSession(List<Usuario> list)
        {
            HttpContext.Current.Session[Constants.ConstSessions.listUsuarios] = list;
        }

        private Tuple<List<Usuario>, bool> GetListFromSession()
        {
            var sessionaValida = false;
            var list = new List<Usuario>();

            if ((List<Usuario>)HttpContext.Current.Session[Constants.ConstSessions.listUsuarios] != null)
            {
                sessionaValida = true;
                list = (List<Usuario>)HttpContext.Current.Session[Constants.ConstSessions.listUsuarios];
            }

            return new Tuple<List<Usuario>, bool>(list, sessionaValida);
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