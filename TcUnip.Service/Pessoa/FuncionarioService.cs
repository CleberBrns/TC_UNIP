using System.Collections.Generic;
using System;
using System.Linq;
using TcUnip.Service.Contract.Pessoa;
using TcUnip.ServiceApi.Pessoa;
using TcUnip.Session.Pessoa;
using TcUnip.Model.Pessoa;
using TcUnip.Model.Common;

namespace TcUnip.Service.Pessoa
{
    public class FuncionarioService : IFuncionarioService
    {
        readonly FuncionarioApi serviceApi = new FuncionarioApi();
        readonly PacienteApi servicePacApi = new PacienteApi();
        readonly FuncionarioSN session = new FuncionarioSN();
        readonly string sessionName = Constants.ConstSessions.listFuncionarios;

        public Result<Funcionario> Get(string cpf)
        {
            var result = new Result<Funcionario>();

            var retornoSession = session.GetFromListSession(cpf, sessionName);

            if (retornoSession.Item2)
                result.Value = retornoSession.Item1;
            else
                result.Value = serviceApi.Get(cpf);

            if (string.IsNullOrEmpty(result.Value.Cpf))
            {
                result.Message = "O Funcionário não existe mais na base de dados do serviço!";
                result.Status = false;
            }

            return result;
        }

        public Result<List<Funcionario>> List(bool getFromSession)
        {
            var result = new Result<List<Funcionario>>();

            if (getFromSession)
            {
                var retornoSession = session.GetListFromSession(sessionName);
                /*Verifica se existia dados na session e se a mesma era válida.
                  Caso a mesma seja válida é passado para o retorno da pesquisa, mesmo que esteja vazia.
                  Caso não esteja criada, a busca é feita no serviço.*/
                if (retornoSession.Item2)
                    result.Value = retornoSession.Item1;
                else
                    result.Value = GetFromService();
            }
            else
                result.Value = GetFromService();

            return result;
        }

        public Result<List<Funcionario>> ListProfissionais(bool getFromSession)
        {
            var result = new Result<List<Funcionario>>();
            var list = new List<Funcionario>();

            if (getFromSession)
            {                
                var retornoSession = session.GetListFromSession(sessionName);
                /*Verifica se existia dados na session e se a mesma era válida.
                  Caso a mesma seja válida é passado para o retorno da pesquisa, mesmo que esteja vazia.
                  Caso não esteja criada, a busca é feita no serviço.*/
                if (retornoSession.Item2)
                    list = retornoSession.Item1;
                else
                    list = GetFromService();
            }
            else
                list = GetFromService();

            /*Lista somente os Funcionários com Modalidades cadastradas, que são os Profissionais*/
            if (list.Count > 0)            
                list = list.Where(l => l.Modalidades.Length > 0).ToList();            

            result.Value = list;

            return result;
        }

        public Result<bool> Save(Funcionario model)
        {
            string msg = string.Empty;
            var result = new Result<bool>();

            var cpfExisteEmOutrosCadastros = ValidaExistenciaCPF(model);

            if (cpfExisteEmOutrosCadastros.Item2)
            {
                result = cpfExisteEmOutrosCadastros.Item1;
            }
            else
            {
                var registroExistente = serviceApi.Get(model.Id);

                if (string.IsNullOrEmpty(registroExistente.Nome))
                {
                    var retorno = serviceApi.Save(model);
                    result.Value = retorno;

                    if (result.Value)
                        msg = "Funcionário salvo com sucesso!";
                    else
                    {
                        msg = "Falha ao salvar o Funcionário!";
                        result.Status = false;
                    }
                }
                else
                {
                    var retorno = serviceApi.Update(model);
                    result.Value = retorno;

                    if (result.Value)
                        msg = "Funcionário atualizado com sucesso!";
                    else
                    {
                        msg = "Falha ao atualizar o Funcionário!";
                        result.Status = false;
                    }
                }

                result.Message = msg;
            }            

            return result;
        }       

        public Result<bool> Delete(string cpf)
        {
            var result = new Result<bool>();

            var retorno = serviceApi.Delete(cpf);
            result.Value = retorno;

            if (result.Value)
                result.Message = "Funcionário excluído com sucesso!";
            else
            {
                result.Message = "Falha ao excluir o Funcionário!";
                result.Status = false;
            }                

            return result;
        }

        #region Métodos Privados

        private Tuple<Result<bool>, bool> ValidaExistenciaCPF(Funcionario model)
        {
            var result = new Result<bool>();

            var cpfExistente = false;
            var funcionario = serviceApi.Get(model.Cpf);
            var paciente = servicePacApi.Get(model.Cpf);

            if (!string.IsNullOrEmpty(paciente.Cpf))
            {
                result.Message = "Cpf vinculado a um Paciente. Não é permitido sua utilização";
                result.Status = false;
                cpfExistente = true;
            }
            else if (!string.IsNullOrEmpty(funcionario.Cpf))
            {
                if (model.Nome != funcionario.Nome)
                {
                    result.Message = "Cpf vinculado a outro Funcionário. Não é permitido sua utilização";
                    result.Status = false;
                    cpfExistente = true;
                }
            }

            return new Tuple<Result<bool>, bool>(result, cpfExistente);
        }

        private List<Funcionario> GetFromService()
        {
            var list = serviceApi.List();
            session.AddListToSession(list, sessionName);
            return list;
        }

        #endregion

    }
}