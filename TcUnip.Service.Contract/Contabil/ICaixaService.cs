using System.Collections.Generic;
using TcUnip.Model.Common;
using TcUnip.Model.Contabil;

namespace TcUnip.Service.Contract.Contabil
{
    public interface ICaixaService
    {
        Result<Caixa> Get(string id);
        Result<List<Caixa>> ListCaixaPeriodo(string dateFrom, string dateTo);
        Result<List<Caixa>> ListCaixaDoDia();
        Result<bool> Salva(Caixa model);
        Result<bool> Exclui(string id);
    }
}
