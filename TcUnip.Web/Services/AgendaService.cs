﻿using System;
using System.Collections.Generic;
using TcUnip.Web.Models.Local;
using TcUnip.Web.Models.Servico;
using TcUnip.Web.API;
using TcUnip.Web.Contracts.Service;
using TcUnip.Web.Session;
using System.Linq;

namespace TcUnip.Web.Services
{
    public class AgendaService : IServiceAgenda
    {
        readonly AgendaAPI service = new AgendaAPI();
        readonly AgendaSession session = new AgendaSession();
        readonly string sessionAgendaPeriodos = Constants.ConstSessions.listAgendaPeriodos;
        readonly string sessionAgendaDoDia = Constants.ConstSessions.listAgendaDoDia;
        readonly string sessionConsultas = Constants.ConstSessions.listConsultas;

        public ResultService<Agenda> Get(string id)
        {
            var result = new ResultService<Agenda>();

            var retorno = service.Get(id);
            result.Value = retorno;

            if (string.IsNullOrEmpty(result.Value.Modalidade))
            {
                result.Status = false;
                result.Message = "A Consulta não existe mais na base de dados do serviço!";
            }                

            return result;
        }

        public ResultService<List<Agenda>> ListAgendaDoDia(bool getFromSession)
        {
            var list = new List<Agenda>();
            var result = new ResultService<List<Agenda>>();

            if (getFromSession)
            {
                var retornoSession = session.GetListFromSession(sessionAgendaDoDia);

                if (retornoSession.Item2)
                    list = retornoSession.Item1;
                else                
                    list = GetAgendaDoDia();                
            }
            else            
                list = GetAgendaDoDia();              

            list = ConfiguraAgendaService(list);
            result.Value = list;

            return result;
        }

        public ResultService<List<Agenda>> ListAgendaPeriodo(string dateFrom, string dateTo, bool getFromSession)
        {
            var result = new ResultService<List<Agenda>>();
            var retorno = GetAgendaPeriodo(dateFrom.Trim(), dateTo.Trim(), 
                                           getFromSession ? sessionAgendaPeriodos : string.Empty);

            var list = ConfiguraAgendaService(retorno);
            result.Value = list;

            return result;
        }

        public ResultService<Funcionario> ConsultasPeriodoFuncionario(string cpf, DateTime dateFrom, DateTime dateTo)
        {
            var result = new ResultService<Funcionario>();
            var retorno = service.ConsultasPeriodoFuncionario(cpf, dateFrom.ToShortDateString(), dateTo.ToShortDateString());           

            if (string.IsNullOrEmpty(retorno.Cpf))
            {
                result.Message = "Sem agenda!";
                result.Status = false;
            }                
            else                
                retorno.Consultas = ConfiguraConsultaService(retorno.Consultas);

            result.Value = retorno;

            return result;
        }

        public ResultService<Paciente> ConsultasPeriodoPaciente(string cpf, DateTime dateFrom, DateTime dateTo)
        {
            var result = new ResultService<Paciente>();
            var retorno = service.ConsultasPeriodoPaciente(cpf, dateFrom.ToShortDateString(), dateTo.ToShortDateString());

            if (!string.IsNullOrEmpty(retorno.Cpf))
            {
                result.Message = "Sem agenda!";
                result.Status = false;
            }                
            else
                retorno.Consultas = ConfiguraConsultaService(retorno.Consultas);

            result.Value = retorno;

            return result;
        }

        public ResultService<bool> Save(Agenda model)
        {
            var result = new ResultService<bool>();

            if (model.Valor < 100)
            {
                result.Message = "O valor mínimo da sessão é de R$100!";
                result.Status = false;
            }
            else
            {
                if (model.Id == 0)
                {
                    model.Data = model.CombinaDataHora(model.Data, model.Horario);
                    model.DateTimeService = model.ToMilliseconds(model.Data);

                    var retorno = service.Save(model);
                    result.Value = retorno;

                    if (result.Value)
                        result.Message = "Consulta salva com sucesso!";
                    else
                    {
                        result.Message = "Falha ao salvar a Consulta!";
                        result.Status = false;
                    }
                }
                else
                {
                    var retorno = service.Update(model);
                    result.Value = retorno;

                    if (result.Value)
                        result.Message = "Consulta atualizada com sucesso!";
                    else
                    {
                        result.Message = "Falha ao atualizar a Consulta!";
                        result.Status = false;
                    }
                }
            }

            return result;
        }

        public ResultService<bool> Delete(string id)
        {
            var result = new ResultService<bool>();

            var retorno = service.Delete(id);
            result.Value = retorno;

            if (result.Value)
                result.Message = "Consulta excluída com sucesso!";
            else
            {
                result.Message = "Falha ao excluir a Consulta!";
                result.Status = false;
            }                

            return result;
        }

        #region Métodos Privados

        private List<Agenda> GetAgendaDoDia()
        {
            return GetAgendaPeriodo(DateTime.Now.ToShortDateString(), DateTime.Now.ToShortDateString(), string.Empty);
        }

        private List<Agenda> GetAgendaPeriodo(string dataDe, string dataAte, string sessionAgenda)
        {
            var list = new List<Agenda>();

            var getFromSession = !string.IsNullOrEmpty(sessionAgenda);

            if (getFromSession)
            {
                var retornoSession = session.GetListFromSession(sessionAgenda);

                if (retornoSession.Item2)
                    list = retornoSession.Item1;
                else
                    list = service.ListAgendasPeriodo(dataDe, dataAte);

                if (!string.IsNullOrEmpty(sessionAgenda))
                    if (list.Count > 0)
                        session.AddListToSession(list, sessionAgenda);
            }
            else            
                list = service.ListAgendasPeriodo(dataDe, dataAte);            

            return list;
        }

        /// <summary>
        /// Configura a DataHora(salvo com long) retornado da service, 
        /// para alimentar os campos Data e Horario com os respectivos dados
        /// </summary>
        /// <param name="listRetorno"></param>
        /// <returns></returns>
        private List<Agenda> ConfiguraAgendaService(List<Agenda> listRetorno)
        {
            var listaConfigurada = new List<Agenda>();

            listaConfigurada = listRetorno.Select(r =>
                                       new Agenda
                                       {
                                           Id = r.Id,
                                           Paciente = r.Paciente,
                                           Funcionario = r.Funcionario,
                                           Data = r.FromMilliseconds(r.DateTimeService),
                                           Horario = r.FromMilliseconds(r.DateTimeService).ToShortTimeString(),
                                           Modalidade = r.Modalidade,
                                           Valor = r.Valor,
                                           DateTimeService = r.DateTimeService
                                       })
                                       .ToList();

            return listaConfigurada.OrderBy(l => l.Data).ToList();
        }

        /// <summary>
        /// Configura a DataHora(salvo com long) retornado da service, 
        /// para alimentar os campos Data e Horario com os respectivos dados
        /// </summary>
        /// <param name="listRetorno"></param>
        /// <returns></returns>
        private List<Consulta> ConfiguraConsultaService(List<Consulta> listRetorno)
        {
            var listaConfigurada = new List<Consulta>();

            listaConfigurada = listRetorno.Select(r =>
                                       new Consulta
                                       {
                                           Id = r.Id,
                                           Nome = r.Nome,
                                           Cpf = r.Cpf,                                           
                                           Data = r.FromMilliseconds(r.DateTimeService),
                                           Horario = r.FromMilliseconds(r.DateTimeService).ToShortTimeString(),
                                           Modalidade = r.Modalidade,
                                           Valor = r.Valor,
                                           DateTimeService = r.DateTimeService
                                       })
                                       .ToList();

            return listaConfigurada;
        }

        #endregion
    }
}