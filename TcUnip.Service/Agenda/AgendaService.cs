using System;
using System.Collections.Generic;
using System.Linq;
using TcUnip.Data.Contract.Agenda;
using TcUnip.Model.Agenda;
using TcUnip.Model.Common;
using TcUnip.Service.Contract.Agenda;

namespace TcUnip.Service.Agenda
{
    public class AgendaService : IAgendaService
    {
        #region Propriedades e Construtor

        readonly ISessaoRepository _sessaoRepository;

        public AgendaService(ISessaoRepository sessaoRepository)
        {
            this._sessaoRepository = sessaoRepository;
        }

        #endregion

        public Result<SessaoModel> Get(int id)
        {
            var result = new Result<SessaoModel>();
            result.Value = _sessaoRepository.SelecionarUm(x => x.Id == id);

            if (result.Value == null)
            {
                result.Message = "A Sessão não existe mais na base de dados!";
                result.Status = false;
            }

            return result;
        }

        public Result<List<SessaoModel>> ListAgendaDoDia()
        {
            var result = new Result<List<SessaoModel>>();
            result.Value = _sessaoRepository.Lista(x => !x.Excluido && 
                                                         x.Data.Date == DateTime.Now.Date)
                                            .ToList();

            return result;
        }

        public Result<List<SessaoModel>> ListAgendaPeriodo(PesquisaModel pesquisaModel)
        {
            var result = new Result<List<SessaoModel>>();
            result.Value = _sessaoRepository.Lista(x => !x.Excluido &&
                                                         x.Data.Date >= pesquisaModel.DataIncio.Date &&
                                                         x.Data.Date <= pesquisaModel.DataFim.Date)
                                            .ToList();

            return result;
        }

        public Result<List<SessaoModel>> ConsultasPeriodoFuncionario(PesquisaModel pesquisaModel)
        {
            var result = new Result<List<SessaoModel>>();
            result.Value = _sessaoRepository.Lista(x => !x.Excluido &&
                                                         x.Data.Date >= pesquisaModel.DataIncio.Date &&
                                                         x.Data.Date <= pesquisaModel.DataFim.Date &&
                                                         x.Funcionario.Pessoa.Cpf == pesquisaModel.CpfCadastro, 
                                                         x => x.Funcionario.Pessoa)
                                            .ToList();

            return result;
        }

        public Result<List<SessaoModel>> ConsultasPeriodoPaciente(PesquisaModel pesquisaModel)
        {
            var result = new Result<List<SessaoModel>>();
            result.Value = _sessaoRepository.Lista(x => !x.Excluido &&
                                                         x.Data.Date >= pesquisaModel.DataIncio.Date &&
                                                         x.Data.Date <= pesquisaModel.DataFim.Date &&
                                                         x.Paciente.Pessoa.Cpf == pesquisaModel.CpfCadastro,
                                                         x => x.Paciente.Pessoa)
                                            .ToList();

            return result;
        }

        public Result<bool> Salva(SessaoModel model)
        {
            var result = new Result<bool>();
            result.Value = false;
            result.Status = false;

            if (model.Id != 0)
            {
                model = _sessaoRepository.Salvar(model);
                if (model.Id != 0)
                {
                    result.Message = "Sessão salvo com sucesso!";
                    result.Value = true;
                    result.Status = true;
                }
                else
                    result.Message = "Falha ao salvar o Sessão!";
            }
            else
            {
                result.Value = _sessaoRepository.Atualizar(model);

                if (result.Value)
                {
                    result.Message = "Sessão atualizado com sucesso!";
                    result.Value = true;
                    result.Status = true;
                }
                else
                    result.Message = "Falha ao atualizar o Sessão!";
            }

            return result;
        }

        public Result<bool> Exclui(int id)
        {
            var result = new Result<bool>();
            result.Status = false;

            var modelBD = _sessaoRepository.SelecionarUm(x => x.Id == id);
            modelBD.Excluido = true;

            result.Value = _sessaoRepository.Atualizar(modelBD);

            if (result.Value)
            {
                result.Message = "Sessão excluído com sucesso!";
                result.Status = true;
            }
            else
                result.Message = "Falha ao excluir o Sessão!";

            return result;
        }
    }
}
