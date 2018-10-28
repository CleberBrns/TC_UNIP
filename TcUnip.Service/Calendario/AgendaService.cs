using System;
using System.Collections.Generic;
using System.Linq;
using TcUnip.Model.Calendario;
using TcUnip.Model.Common;
using TcUnip.Model.Pessoa;
using TcUnip.Service.Contract.Calendario;
using TcUnip.ServiceApi.Calendario;

namespace TcUnip.Service.Calendario
{
    public class AgendaService : IAgendaService
    {
        readonly AgendaApi service = new AgendaApi();
        readonly ReplacesService dateTimeService = new ReplacesService();

        public Result<Agenda> Get(string id)
        {
            var result = new Result<Agenda>();

            var retorno = service.Get(id);
            result.Value = retorno;

            if (string.IsNullOrEmpty(result.Value.Modalidade))
            {
                result.Status = false;
                result.Message = "A Consulta não existe mais na base de dados do serviço!";
            }                

            return result;
        }

        public Result<List<Agenda>> ListAgendaDoDia()
        {
            var list = new List<Agenda>();
            var result = new Result<List<Agenda>>();             

            list = ConfiguraAgendaService(GetAgendaDoDia());
            result.Value = list;

            return result;
        }

        public Result<List<Agenda>> ListAgendaPeriodo(string dateFrom, string dateTo)
        {
            dateFrom = dateTimeService.ReplaceDateWebToApi(dateFrom, false);
            dateTo = dateTimeService.ReplaceDateWebToApi(dateTo, false);

            var result = new Result<List<Agenda>>();
            var retorno = GetAgendaPeriodo(dateFrom.Trim(), dateTo.Trim());

            var list = ConfiguraAgendaService(retorno);
            result.Value = list;

            return result;
        }

        public Result<Funcionario> ConsultasPeriodoFuncionario(string cpf, string dateFrom, string dateTo)
        {
            dateFrom = dateTimeService.ReplaceDateWebToApi(dateFrom, false);
            dateTo = dateTimeService.ReplaceDateWebToApi(dateTo, false);

            var result = new Result<Funcionario>();
            var retorno = service.ConsultasPeriodoFuncionario(cpf, dateFrom, dateTo);

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

        public Result<Paciente> ConsultasPeriodoPaciente(string cpf, string dateFrom, string dateTo)
        {
            dateFrom = dateTimeService.ReplaceDateWebToApi(dateFrom, false);
            dateTo = dateTimeService.ReplaceDateWebToApi(dateTo, false);

            var result = new Result<Paciente>();
            var retorno = service.ConsultasPeriodoPaciente(cpf, dateFrom, dateTo);

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

        public Result<bool> Salva(Agenda model)
        {
            var result = new Result<bool>();

            if (model.Valor < 100)
            {
                result.Message = "O valor mínimo da sessão é de R$100!";
                result.Status = false;
            }
            else
            {
                if (model.Id == 0)
                {
                    model.Data = dateTimeService.CombinaDataHora(model.Data, model.Horario);
                    model.DateTimeService = dateTimeService.ToMilliseconds(model.Data);
                    model.Status = ConstStatus.Agendamento.pendente;

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
                    var retorno = service.Update(model, model.Id.ToString());
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

        public Result<bool> Exclui(string id)
        {
            var result = new Result<bool>();

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
            return GetAgendaPeriodo(DateTime.Now.ToShortDateString(), DateTime.Now.ToShortDateString());
        }

        private List<Agenda> GetAgendaPeriodo(string dataDe, string dataAte)
        {
            dataDe = dateTimeService.ReplaceDateWebToApi(dataDe, false);
            dataAte = dateTimeService.ReplaceDateWebToApi(dataAte, false);

            return service.ListAgendasPeriodo(dataDe, dataAte);
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
                                           Data = dateTimeService.FromMilliseconds(r.DateTimeService),
                                           Horario = dateTimeService.FromMilliseconds(r.DateTimeService).ToShortTimeString(),
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
                                           Data = dateTimeService.FromMilliseconds(r.DateTimeService),
                                           Horario = dateTimeService.FromMilliseconds(r.DateTimeService).ToShortTimeString(),
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