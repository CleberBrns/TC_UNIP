using System.Collections.Generic;
using TcUnip.Web.Models.Local;
using TcUnip.Web.Models.Servico;
using TcUnip.Web.API;
using TcUnip.Web.Contracts.Service;
using TcUnip.Web.Session;
using System;
using System.Linq;

namespace TcUnip.Web.Services
{
    public class FuncionarioService : IServiceFuncionario
    {
        readonly FuncionarioAPI serviceApi = new FuncionarioAPI();
        readonly PacienteAPI servicePacApi = new PacienteAPI();
        readonly FuncionarioSession session = new FuncionarioSession();
        readonly string sessionName = Constants.ConstSessions.listFuncionarios;

        public ResultService<Funcionario> Get(string cpf)
        {
            var result = new ResultService<Funcionario>();

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
                    result.Value = retornoSession.Item1;
                else
                    result.Value = GetFromService();
            }
            else
                result.Value = GetFromService();

            return result;
        }

        public ResultService<List<Funcionario>> ListProfissionais(bool getFromSession)
        {
            var result = new ResultService<List<Funcionario>>();
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

        public ResultService<bool> Save(Funcionario model)
        {
            string msg = string.Empty;
            var result = new ResultService<bool>();

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

        public ResultService<bool> Delete(string cpf)
        {
            var result = new ResultService<bool>();

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

        private Tuple<ResultService<bool>, bool> ValidaExistenciaCPF(Funcionario model)
        {
            var result = new ResultService<bool>();

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

            return new Tuple<ResultService<bool>, bool>(result, cpfExistente);
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