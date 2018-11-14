using System;
using System.Collections.Generic;
using System.Linq;
using TcUnip.Model.Calendario;
using TcUnip.Model.Common;
using TcUnip.Model.Contabil;
using TcUnip.Service.Contract.Contabil;
//using TcUnip.ServiceApi.Calendario;

namespace TcUnip.Service.Contabil
{
    public class ReciboService// : IReciboService
    {
        //readonly AgendaApi service = new AgendaApi();
        //readonly ReplacesService dateTimeService = new ReplacesService();

        //public Result<Recibo> Get(string id)
        //{
        //    var result = new Result<Recibo>();

        //    var retorno = ConfiguraReciboFromAgenda(service.Get(id));
        //    result.Value = retorno;

        //    if (string.IsNullOrEmpty(result.Value.Paciente))
        //    {
        //        result.Status = false;
        //        result.Message = "O registro não existe mais na base de dados do serviço!";
        //    }

        //    return result;
        //}

        //public Result<List<Recibo>> ListRecibosDoDia()
        //{
        //    var list = new List<Recibo>();
        //    var result = new Result<List<Recibo>>();

        //    list = GetRecibosDoDia();
        //    result.Value = list;

        //    return result;
        //}

        //public Result<List<Recibo>> ListRecibosPeriodo(string dateFrom, string dateTo)
        //{
        //    dateFrom = dateTimeService.ReplaceDateWebToApi(dateFrom, false);
        //    dateTo = dateTimeService.ReplaceDateWebToApi(dateTo, false);

        //    var result = new Result<List<Recibo>>();
        //    var retorno = GetRecibosPeriodo(dateFrom.Trim(), dateTo.Trim());

        //    var list = GetRecibosPeriodo(dateFrom.Trim(), dateTo.Trim());
        //    result.Value = list;

        //    return result;
        //}

        //public Result<List<Recibo>> ListRecibosPeriodoFuncionario(string cpf, string dateFrom, string dateTo)
        //{
        //    dateFrom = dateTimeService.ReplaceDateWebToApi(dateFrom, false);
        //    dateTo = dateTimeService.ReplaceDateWebToApi(dateTo, false);

        //    var result = new Result<List<Recibo>>();
        //    var retorno = service.ConsultasPeriodoFuncionario(cpf, dateFrom, dateTo);

        //    var listRecibos = ConfiguraReciboFromConsultasProfissional(retorno.Consultas, retorno.Nome);
        //    result.Value = listRecibos;

        //    return result;
        //}

        //public Result<List<Recibo>> ListRecibosPeriodoPaciente(string cpf, string dateFrom, string dateTo)
        //{
        //    dateFrom = dateTimeService.ReplaceDateWebToApi(dateFrom, false);
        //    dateTo = dateTimeService.ReplaceDateWebToApi(dateTo, false);

        //    var result = new Result<List<Recibo>>();
        //    var retorno = service.ConsultasPeriodoPaciente(cpf, dateFrom, dateTo);

        //    var listRecibos = ConfiguraReciboFromConsultasPaciente(retorno.Consultas, retorno.Nome);
        //    result.Value = listRecibos;

        //    return result;
        //}

        //#region Métodos Privados

        //private List<Recibo> GetRecibosDoDia()
        //{
        //    return GetRecibosPeriodo(DateTime.Now.ToShortDateString(), DateTime.Now.ToShortDateString());
        //}

        //private List<Recibo> GetRecibosPeriodo(string dataDe, string dataAte)
        //{
        //    dataDe = dateTimeService.ReplaceDateWebToApi(dataDe, false);
        //    dataAte = dateTimeService.ReplaceDateWebToApi(dataAte, false);

        //    var listAgenda = service.ListAgendasPeriodo(dataDe, dataAte);
        //    var listRecibos = ConfiguraListaReciboFromListaAgenda(listAgenda);

        //    return listRecibos;
        //}

        ///// <summary>
        ///// Configura o Model Agenda para carregar os dados do Recibo
        ///// </summary>
        ///// <param name="agendaService"></param>
        ///// <returns></returns>
        //private Recibo ConfiguraReciboFromAgenda(Agenda agendaService)
        //{
        //    var recibo = new Recibo();
        //    recibo =  new Recibo
        //    {
        //        IdAgenda = agendaService.Id,
        //        Paciente = agendaService.Paciente.Nome,
        //        Profissional = agendaService.Funcionario.Nome,
        //        Data = dateTimeService.FromMilliseconds(agendaService.DateTimeService),
        //        Valor = agendaService.Valor.ToString()
        //    };

        //    return recibo;
        //}

        ///// <summary>
        ///// Configura a Lista de Models Agenda para carregar os dados da Lista de Models Recibo
        ///// </summary>
        ///// <param name="listRetorno"></param>
        ///// <returns></returns>
        //private List<Recibo> ConfiguraListaReciboFromListaAgenda(List<Agenda> listRetorno)
        //{
        //    var listaConfigurada = new List<Recibo>();
        //    listaConfigurada = listRetorno.Select(r =>
        //                               new Recibo
        //                               {
        //                                   IdAgenda = r.Id,
        //                                   Paciente = r.Paciente.Nome,
        //                                   Profissional = r.Funcionario.Nome,
        //                                   Data = dateTimeService.FromMilliseconds(r.DateTimeService),
        //                                   Valor = r.Valor.ToString()
        //                               })
        //                               .ToList();

        //    return listaConfigurada.OrderBy(l => l.Data).ToList();
        //}

        ///// <summary>
        ///// Configura a Lista de Models Consulta para carregar os dados da Lista de Models Recibo
        ///// </summary>
        ///// <param name="listRetorno"></param>
        ///// <returns></returns>
        //private List<Recibo> ConfiguraReciboFromConsultasProfissional(List<Consulta> listRetorno, string profissional)
        //{
        //    var listaConfigurada = new List<Recibo>();

        //    listaConfigurada = listRetorno.Select(r =>
        //                               new Recibo
        //                               {
        //                                   IdAgenda = r.Id,
        //                                   Paciente = r.Nome,
        //                                   Profissional = profissional,
        //                                   Data = dateTimeService.FromMilliseconds(r.DateTimeService),
        //                                   Valor = r.Valor.ToString()
        //                               })
        //                               .ToList();

        //    return listaConfigurada;
        //}

        ///// <summary>
        ///// Configura a Lista de Models Consulta para carregar os dados da Lista de Models Recibo
        ///// </summary>
        ///// <param name="listRetorno"></param>
        ///// <returns></returns>
        //private List<Recibo> ConfiguraReciboFromConsultasPaciente(List<Consulta> listRetorno, string paciente)
        //{
        //    var listaConfigurada = new List<Recibo>();

        //    if (!string.IsNullOrEmpty(paciente))
        //    {
        //        listaConfigurada = listRetorno.Select(r =>
        //                   new Recibo
        //                   {
        //                       IdAgenda = r.Id,
        //                       Paciente = paciente,
        //                       Profissional = r.Nome,
        //                       Data = dateTimeService.FromMilliseconds(r.DateTimeService),
        //                       Valor = r.Valor.ToString()
        //                   })
        //                   .ToList();
        //    }
        //    return listaConfigurada;
        //}

        //#endregion
    }
}
