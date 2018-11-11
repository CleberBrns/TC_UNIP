using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcUnip.Model.Common;
using TcUnip.Model.Contabil;

namespace TcUnip.Service.Contract.Contabil
{
    public interface IReciboService
    {
        Result<Recibo> Get(string id);
        Result<List<Recibo>> ListRecibosPeriodo(string dateFrom, string dateTo);
        Result<List<Recibo>> ListRecibosDoDia();
        Result<List<Recibo>> ListRecibosPeriodoPaciente(string cpf, string dateFrom, string dateTo);
        Result<List<Recibo>> ListRecibosPeriodoFuncionario(string cpf, string dateFrom, string dateTo);
    }
}
