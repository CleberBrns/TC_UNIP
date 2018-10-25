using System.Collections.Generic;
using TCC_Unip.Models.Local;
using TCC_Unip.Models.Servico;
using TCC_Unip.API;
using TCC_Unip.Contracts.Service;
using TCC_Unip.Session;
using System;

namespace TCC_Unip.Services
{
    public class PacienteService : IServicePaciente
    {
        readonly PacienteAPI serviceApi = new PacienteAPI();
        readonly FuncionarioAPI serviceFuncApi = new FuncionarioAPI();
        readonly PacienteSession session = new PacienteSession();
        readonly string sessionName = Constants.ConstSessions.listPacientes;

        public ResultService<Paciente> Get(string cpf)
        {
            var result = new ResultService<Paciente>();

            var retornoSession = session.GetFromListSession(cpf, sessionName);

            if (retornoSession.Item2 && !string.IsNullOrEmpty(retornoSession.Item1.Cpf))
                result.Value = retornoSession.Item1;
            else
                result.Value = serviceApi.Get(cpf);

            if (string.IsNullOrEmpty(result.Value.Cpf))
            {
                result.Message = "O paciente não existe mais na base de dados do serviço!";
                result.Status = false;
            }                

            return result;
        }

        public ResultService<List<Paciente>> List(bool getFromSession)
        {
            var result = new ResultService<List<Paciente>>();

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

        public ResultService<bool> Save(Paciente model)
        {
            string msg = string.Empty;
            string msgErro = string.Empty;

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
                        msg = "Paciente salvo com sucesso!";
                    else
                    {
                        msg = "Falha ao salvar o paciente!";
                        result.Status = false;
                    }
                }
                else
                {
                    var retorno = serviceApi.Update(model);
                    result.Value = retorno;

                    if (result.Value)
                        msg = "Paciente atualizado com sucesso!";
                    else
                    {
                        msg = "Falha ao atualizar o paciente!";
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
                result.Message = "Paciente excluído com sucesso!";
            else
            {
                result.Message = "Falha ao excluir o paciente!";
                result.Status = false;
            }

            return result;
        }

        #region Métodos Privados

        private Tuple<ResultService<bool>, bool> ValidaExistenciaCPF(Paciente model)
        {
            var result = new ResultService<bool>();

            var cpfExistente = false;
            var funcionario = serviceFuncApi.Get(model.Cpf);
            var paciente = serviceApi.Get(model.Cpf);

            if (!string.IsNullOrEmpty(funcionario.Cpf))
            {
                result.Message = "Cpf vinculado a um Funcionário. Não é permitido sua utilização";
                result.Status = false;
                cpfExistente = true;
            }
            else if (!string.IsNullOrEmpty(paciente.Cpf))
            {
                if (model.Nome != paciente.Nome)
                {
                    result.Message = "Cpf vinculado a outro Paciente. Não é permitido sua utilização";
                    result.Status = false;
                    cpfExistente = true;
                }
            }

            return new Tuple<ResultService<bool>, bool>(result, cpfExistente);
        }


        private List<Paciente> GetFromService()
        {
            var list = serviceApi.List();
            session.AddListToSession(list, sessionName);
            return list;
        }

        #endregion
    }
}