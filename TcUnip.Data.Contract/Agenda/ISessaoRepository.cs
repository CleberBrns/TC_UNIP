using System.Collections.Generic;
using TcUnip.Model.Agenda;
using TcUnip.Model.Common;

namespace TcUnip.Data.Contract.Agenda
{
    public interface ISessaoRepository : IRepositoryBase<SessaoModel>
    {
        SessaoModel GetById(int id);
        List<SessaoModel> ListSessoesPeriodo(PesquisaModel pesquisaModel);
        List<SessaoModel> ListSessoesPeriodoFuncionario(PesquisaModel pesquisaModel);
        List<SessaoModel> ListSessoesPeriodoCpfPaciente(PesquisaModel pesquisaModel);
        List<SessaoModel> ListSessoesPeriodoIdPaciente(PesquisaModel pesquisaModel);
    }
}
