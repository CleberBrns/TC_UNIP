using System.Collections.Generic;
using TcUnip.Model.Agenda;
using TcUnip.Model.Common;

namespace TcUnip.Data.Contract.Agenda
{
    public interface ISessaoRepository : IRepositoryBase<SessaoModel>
    {
        SessaoModel GetById(int id);
        List<SessaoModel> ListAgendaPeriodo(PesquisaModel pesquisaModel);
        List<SessaoModel> ListAgendaPeriodoFuncionario(PesquisaModel pesquisaModel);
        List<SessaoModel> ListAgendaPeriodoPaciente(PesquisaModel pesquisaModel);
    }
}
