using System;
using System.Collections.Generic;
using TCC_Unip.Models.Local;
using TCC_Unip.Models.Servico;
using TCC_Unip.API;
using TCC_Unip.Contracts.Service;
using TCC_Unip.Session;
using System.Linq;

namespace TCC_Unip.Services
{
    public class AgendaService : IServiceAgenda
    {
        readonly AgendaAPI service = new AgendaAPI();
        readonly AgendaSession session = new AgendaSession();
        readonly string sessionAgenda = Constants.ConstSessions.listAgenda;
        readonly string sessionConsultas = Constants.ConstSessions.listConsultas;

        public ResultService<Agenda> Get(string id)
        {
            var result = new ResultService<Agenda>();

            var retorno = service.Get(id);
            result.value = retorno;

            if (string.IsNullOrEmpty(result.value.Modalidade))
                result.errorMessage = "A Consulta não existe mais na base de dados do serviço!";

            return result;
        }

        public ResultService<List<Agenda>> ListAgendaDoDia()
        {
            var result = new ResultService<List<Agenda>>();
            var retorno = service.ListAgendasPeriodo(DateTime.Now.ToShortDateString(), DateTime.Now.ToShortDateString());

            var list = ConfiguraAgendaService(retorno);
            result.value = list;

            return result;
        }

        public ResultService<List<Agenda>> ListAgendaPeriodo(DateTime dateFrom, DateTime dateTo)
        {
            var result = new ResultService<List<Agenda>>();
            var retorno = service.ListAgendasPeriodo(dateFrom.ToShortDateString(), dateTo.ToShortDateString());

            var list = ConfiguraAgendaService(retorno);
            result.value = list;

            return result;
        }

        public ResultService<Funcionario> ConsultasPeriodoFuncionario(string cpf, DateTime dateFrom, DateTime dateTo)
        {
            var result = new ResultService<Funcionario>();
            var retorno = service.ConsultasPeriodoFuncionario(cpf, dateFrom.ToShortDateString(), dateTo.ToShortDateString());           

            if (string.IsNullOrEmpty(retorno.Cpf))
                result.errorMessage = "Sem agenda!";
            else                
                retorno.Consultas = ConfiguraConsultaService(retorno.Consultas);

            result.value = retorno;

            return result;
        }

        public ResultService<Paciente> ConsultasPeriodoPaciente(string cpf, DateTime dateFrom, DateTime dateTo)
        {
            var result = new ResultService<Paciente>();
            var retorno = service.ConsultasPeriodoPaciente(cpf, dateFrom.ToShortDateString(), dateTo.ToShortDateString());

            if (!string.IsNullOrEmpty(retorno.Cpf))
                result.errorMessage = "Sem agenda!";
            else
                retorno.Consultas = ConfiguraConsultaService(retorno.Consultas);

            result.value = retorno;

            return result;
        }

        public ResultService<bool> Save(Agenda model)
        {
            var result = new ResultService<bool>();

            if (model.Id == 0)
            {
                model.Data = model.CombinaDataHora(model.Data, model.Horario);
                model.DateTimeService = model.ToMilliseconds(model.Data);                

                var retorno = service.Save(model);
                result.value = retorno;

                if (result.value)
                    result.message = "Consulta salva com sucesso!";
                else
                    result.errorMessage = "Falha ao salvar a Consulta!";
            }
            else
            {
                var retorno = service.Update(model);
                result.value = retorno;

                if (result.value)
                    result.message = "Consulta atualizada com sucesso!";
                else
                    result.errorMessage = "Falha ao atualizar a Consulta!";
            }

            return result;
        }

        public ResultService<bool> Delete(string id)
        {
            var result = new ResultService<bool>();

            var retorno = service.Delete(id);
            result.value = retorno;

            if (result.value)
                result.message = "Consulta excluída com sucesso!";
            else
                result.errorMessage = "Falha ao excluir a Consulta!";

            return result;
        }

        #region Métodos Privados

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

            return listaConfigurada;
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