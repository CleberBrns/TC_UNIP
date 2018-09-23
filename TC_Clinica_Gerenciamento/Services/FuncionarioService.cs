using System.Collections.Generic;
using TCC_Unip.Models.Local;
using TCC_Unip.Models.Servico;
using TCC_Unip.API;
using TCC_Unip.Contracts.Service;
using TCC_Unip.Session;
using System;

namespace TCC_Unip.Services
{
    public class FuncionarioService : IServiceFuncionario
    {
        readonly FuncionarioAPI service = new FuncionarioAPI();
        readonly FuncionarioSession session = new FuncionarioSession();
        readonly string sessionName = Constants.ConstSessions.listFuncionarios;

        public ResultService<Funcionario> Get(string cpf)
        {
            var result = new ResultService<Funcionario>();

            var retornoSession = session.GetFromListSession(cpf, sessionName);

            if (retornoSession.Item2 && !string.IsNullOrEmpty(retornoSession.Item1.Cpf))
                result.value = retornoSession.Item1;
            else
                result.value = service.Get(cpf);

            if (string.IsNullOrEmpty(result.value.Cpf))
                result.message = "O Funcionário não existe mais na base de dados do serviço!";

            return result;
        }

        public ResultService<List<Funcionario>> List(bool getFromSession)
        {
            var result = new ResultService<List<Funcionario>>();

            if (getFromSession)
            {
                var retornoSession = session.GetListFromSession(sessionName);
                /*Verifica se existia dados na session e se a mesma era válida.
                  Caso a mesma seja válida é passado para o retorno da pesquisa, mesmo que esteja vazia.
                  Caso não esteja criada, a busca é feita no serviço.*/
                if (retornoSession.Item2)
                    result.value = retornoSession.Item1;
                else
                    result.value = GetFromService();
            }
            else
                result.value = GetFromService();

            return result;
        }

        public ResultService<bool> Save(Funcionario model)
        {
            string msg = string.Empty;
            string msgErro = string.Empty;

            var result = new ResultService<bool>();

            var registroExistente = service.Get(model.Cpf);

            if (string.IsNullOrEmpty(registroExistente.Nome))
            {
                var retorno = service.Save(model);
                result.value = retorno;

                if (result.value)
                    msg = "Funcionário salvo com sucesso!";
                else
                    msg = "Falha ao salvar o Funcionário!";
            }
            else
            {
                var retorno = service.Update(model);
                result.value = retorno;

                if (result.value)
                    msg = "Funcionário atualizado com sucesso!";
                else
                    msg = "Falha ao atualizar o Funcionário!";
            }

            result.message = msg;
            result.errorMessage = msgErro;

            return result;
        }       

        public ResultService<bool> Delete(string cpf)
        {
            var result = new ResultService<bool>();

            var retorno = service.Delete(cpf);
            result.value = retorno;

            if (result.value)
                result.message = "Funcionário excluído com sucesso!";
            else
                result.message = "Falha ao excluir o Funcionário!";

            return result;
        }

        #region Métodos Privados

        private List<Funcionario> GetFromService()
        {
            var list = service.List();
            session.AddListToSession(list, sessionName);
            return list;
        }

        #endregion

    }
}