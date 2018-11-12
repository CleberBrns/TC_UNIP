using System.Collections.Generic;
using TcUnip.Model.Common;
using TcUnip.Model.FluxoCaixa;

namespace TcUnip.Service.Contract.FluxoCaixa
{
    public interface IReciboService
    {
        Result<ReciboModel> Get(string id);
        Result<List<ReciboModel>> ListRecibosPeriodo(string dateFrom, string dateTo);
        Result<List<ReciboModel>> ListRecibosDoDia();
        Result<List<ReciboModel>> ListRecibosPeriodoPaciente(string cpf, string dateFrom, string dateTo);
        Result<List<ReciboModel>> ListRecibosPeriodoFuncionario(string cpf, string dateFrom, string dateTo);
    }
}
