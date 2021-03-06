﻿using System;
using System.Collections.Generic;
using System.Linq;
using TcUnip.Data.Contract.Agenda;
using TcUnip.Data.Contract.FluxoCaixa;
using TcUnip.Model.Agenda;
using TcUnip.Model.Common;
using TcUnip.Service.Contract.Agenda;

namespace TcUnip.Service.Agenda
{
    public class AgendaService : IAgendaService
    {
        readonly Util.ConfiguraPesquisa configuraPesquia = new Util.ConfiguraPesquisa();
        #region Propriedades e Construtor        
        readonly ISessaoRepository _sessaoRepository;
        readonly ICaixaRepository _caixaRepository;

        public AgendaService(ISessaoRepository sessaoRepository, ICaixaRepository caixaRepository)
        {
            this._sessaoRepository = sessaoRepository;
            this._caixaRepository = caixaRepository;
        }

        #endregion

        public Result<SessaoModel> Get(int id)
        {
            var result = new Result<SessaoModel>();
            result.Value = _sessaoRepository.GetById(id);

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
            result.Value = _sessaoRepository.ListSessoesPeriodo(configuraPesquia.GetPesquisaDoDia());

            return result;
        }

        public Result<List<SessaoModel>> ListAgendaPeriodo(PesquisaModel pesquisaModel)
        {
            pesquisaModel = configuraPesquia.ConfiguraDatasPesquisa(pesquisaModel);            
            var result = new Result<List<SessaoModel>>();
            result.Value = _sessaoRepository.ListSessoesPeriodo(pesquisaModel);

            if (result.Value.Count > 0)
                result.Value = result.Value.Where(x => !x.Excluido).ToList();

            return result;
        }

        public Result<List<SessaoModel>> ListAgendaPeriodoFuncionario(PesquisaModel pesquisaModel)
        {
            pesquisaModel = configuraPesquia.ConfiguraDatasPesquisa(pesquisaModel);            
            var result = new Result<List<SessaoModel>>();
            result.Value = _sessaoRepository.ListSessoesPeriodoFuncionario(pesquisaModel);

            if (result.Value.Count > 0)
                result.Value = result.Value.Where(x => !x.Excluido).ToList();

            return result;
        }

        public Result<List<SessaoModel>> ListAgendaPeriodoPaciente(PesquisaModel pesquisaModel)
        {
            pesquisaModel = configuraPesquia.ConfiguraDatasPesquisa(pesquisaModel);            
            var result = new Result<List<SessaoModel>>();
            result.Value = _sessaoRepository.ListSessoesPeriodoCpfPaciente(pesquisaModel);

            if (result.Value.Count > 0)
                result.Value = result.Value.Where(x => !x.Excluido).ToList();

            return result;
        }

        public Result<bool> Salva(SessaoModel model)
        {
            var podeSalvar = true;
            var result = new Result<bool>();
            result.Value = false;
            result.Status = false;

            if (model.Valor < 100)
            {
                result.Message = "O valor mínimo de uma Sessão é de R$100!";
            }
            else
            {
                model.Data = Convert.ToDateTime(String.Format("{0} {1}", 
                                                model.Data.ToShortDateString(), model.Horario));


                var pesquisaModel = new PesquisaModel
                {
                    IdPesquisa = model.IdPaciente,
                    DataIncio = model.Data,
                    DataFim = model.Data.AddHours(1)   
                };

                var sessoesPaciente = _sessaoRepository.ListSessoesPeriodoIdPaciente(pesquisaModel);

                if (sessoesPaciente.Count > 0)
                {
                    if (model.Id != 0)
                        sessoesPaciente = sessoesPaciente.Where(x => x.Id != model.Id).ToList();

                    if (sessoesPaciente.Count > 0)
                    {
                        result.Message = "O Paciente já possui Sessão marcada para o dia e hora selecionados!";
                        podeSalvar = false;
                    }                    
                }

                if(podeSalvar)
                {
                    if (model.Id == 0)
                    {
                        model = _sessaoRepository.Salvar(model);
                        if (model.Id != 0)
                        {
                            model = _sessaoRepository.GetById(model.Id);
                            InsereRegistroCaixa(model);
                            result.Message = "Sessão salva com sucesso!";
                            result.Value = true;
                            result.Status = true;
                        }
                        else
                            result.Message = "Falha ao salvar a Sessão!";
                    }
                    else
                    {
                        result.Value = _sessaoRepository.Atualizar(model);
                        if (result.Value)
                        {
                            model = _sessaoRepository.GetById(model.Id);
                            AtualizaRegistroCaixa(model);

                            result.Message = "Sessão atualizada com sucesso!";
                            result.Value = true;
                            result.Status = true;
                        }
                        else
                            result.Message = "Falha ao atualizar a Sessão!";
                    }
                }               
            }

            return result;
        }

        private void AtualizaRegistroCaixa(SessaoModel model)
        {
            var registroCaixaSessao = _caixaRepository.GetByIdSessao(model.Id);

            if (registroCaixaSessao != null)
            {
                registroCaixaSessao.Credito = model.Valor;
                registroCaixaSessao.Descricao = "Sessão de " + model.Modalidade.Nome +
                                                " para o(a) paciente " + model.Paciente.Pessoa.Nome;

                _caixaRepository.Atualizar(registroCaixaSessao);
            }
            else            
                InsereRegistroCaixa(model);                  
        }

        private void InsereRegistroCaixa(SessaoModel model)
        {
            var registroCaixa =
                new Model.FluxoCaixa.CaixaModel {
                    Data = model.Data,
                    Credito = model.Valor,
                    IdSessao = model.Id,
                    Descricao = "Sessão de " + model.Modalidade.Nome +
                                " para o(a) paciente " + model.Paciente.Pessoa.Nome
                };

            _caixaRepository.Salvar(registroCaixa);
        }

        public Result<bool> Exclui(int id)
        {
            var result = new Result<bool>();
            result.Status = false;

            var modelBD = _sessaoRepository.GetById(id);
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
