using System.Collections.Generic;
using TcUnip.Model.Common;
using TcUnip.Model.FluxoCaixa;

namespace TcUnip.Data.Contract.FluxoCaixa
{
    public interface ICaixaRepository : IRepositoryBase<CaixaModel>
    {
        List<CaixaModel> ListCaixaPeriodo(PesquisaModel pesquisaModel);
        CaixaModel GetById(int id);
    }
}
