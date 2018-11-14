using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcUnip.Model.Common;
using TcUnip.Model.Contabil;
using TcUnip.Service.Contract.Contabil;
//using TcUnip.ServiceApi.Contabil;

namespace TcUnip.Service.Contabil
{
    public class CaixaService //: ICaixaService
    {
        //readonly CaixaApi service = new CaixaApi();
        //readonly ReplacesService dateTimeService = new ReplacesService();

        //public Result<Caixa> Get(string id)
        //{
        //    var result = new Result<Caixa>();

        //    var retorno = service.Get(id);
        //    result.Value = retorno;

        //    if (string.IsNullOrEmpty(result.Value.DateTimeService))
        //    {
        //        result.Status = false;
        //        result.Message = "O registro não existe mais na base de dados do serviço!";
        //    }

        //    return result;
        //}

        //public Result<List<Caixa>> ListCaixaDoDia()
        //{
        //    var list = new List<Caixa>();
        //    var result = new Result<List<Caixa>>();

        //    list = ConfiguraConsultaService(List());
        //    result.Value = list;

        //    return result;
        //}

        //public Result<List<Caixa>> ListCaixaPeriodo(string dateFrom, string dateTo)
        //{
        //    dateFrom = dateTimeService.ReplaceDateWebToApi(dateFrom, false);
        //    dateTo = dateTimeService.ReplaceDateWebToApi(dateTo, false);

        //    var result = new Result<List<Caixa>>();
        //    var retorno = GetCaixaPeriodo(dateFrom.Trim(), dateTo.Trim());

        //    var list = ConfiguraConsultaService(retorno);
        //    result.Value = list;

        //    return result;
        //}

        //public Result<bool> Salva(Caixa model)
        //{
        //    var result = new Result<bool>();
            
        //    if (model.Id == 0)
        //    {
        //        model.Data = dateTimeService.CombinaDataHora(model.Data, model.Horario);
        //        model.DateTimeService = dateTimeService.ToMilliseconds(model.Data);                

        //        var retorno = service.Save(model);
        //        result.Value = retorno;

        //        if (result.Value)
        //            result.Message = "Registro salvo com sucesso!";
        //        else
        //        {
        //            result.Message = "Falha ao salvar o registro!";
        //            result.Status = false;
        //        }
        //    }
        //    else
        //    {
        //        var retorno = service.Update(model, model.Id.ToString());
        //        result.Value = retorno;

        //        if (result.Value)
        //            result.Message = "Registro atualizado com sucesso!";
        //        else
        //        {
        //            result.Message = "Falha ao atualizar o registro!";
        //            result.Status = false;
        //        }
        //    }

        //    return result;
        //}

        //public Result<bool> Exclui(string id)
        //{
        //    var result = new Result<bool>();

        //    var retorno = service.Delete(id);
        //    result.Value = retorno;

        //    if (result.Value)
        //        result.Message = "Registro excluído com sucesso!";
        //    else
        //    {
        //        result.Message = "Falha ao excluir o registro!";
        //        result.Status = false;
        //    }

        //    return result;
        //}

        //#region Métodos Privados

        //private List<Caixa> List()
        //{
        //    return service.List();
        //}

        //private List<Caixa> GetCaixaDoDia()
        //{
        //    return GetCaixaPeriodo(DateTime.Now.ToShortDateString(), DateTime.Now.ToShortDateString());
        //}

        //private List<Caixa> GetCaixaPeriodo(string dataDe, string dataAte)
        //{
        //    dataDe = dateTimeService.ReplaceDateWebToApi(dataDe, false);
        //    dataAte = dateTimeService.ReplaceDateWebToApi(dataAte, false);

        //    return service.ListCaixaPeriodo(dataDe, dataAte);
        //}


        ///// <summary>
        ///// Configura a DataHora(salvo com long) retornado da service, 
        ///// para alimentar os campos Data e Horario com os respectivos dados
        ///// </summary>
        ///// <param name="listRetorno"></param>
        ///// <returns></returns>
        //private List<Caixa> ConfiguraConsultaService(List<Caixa> listRetorno)
        //{
        //    var listaConfigurada = new List<Caixa>();

        //    listaConfigurada = 
        //        listRetorno.Select(r => new Caixa
        //                               {
        //                                   Id = r.Id,
        //                                   Descricao = r.Descricao,
        //                                   Credito = r.Credito,
        //                                   Debito = r.Debito,
        //                                   Saldo = (r.Credito - r.Debito).ToString(),
        //                                   Data = dateTimeService.FromMilliseconds(r.DateTimeService),
        //                                   Horario = dateTimeService.FromMilliseconds(r.DateTimeService).ToShortTimeString(),
        //                                   DateTimeService = r.DateTimeService
        //                              })
        //                   .ToList();

        //    return listaConfigurada;
        //}

        //#endregion
    }
}
