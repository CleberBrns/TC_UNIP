using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcUnip.Model.Agenda;
using TcUnip.Model.Cadastro;
using TcUnip.Model.Common;

namespace TcUnip.Service.Contract.Agenda
{
    public interface IAgendaService
    {
        Result<SessaoModel> Get(int id);
        Result<List<SessaoModel>> ListAgendaPeriodo(PesquisaModel pesquisaModel);
        Result<List<SessaoModel>> ListAgendaDoDia();
        Result<List<SessaoModel>> ListAgendaPeriodoPaciente(PesquisaModel pesquisaModel);
        Result<List<SessaoModel>> ListAgendaPeriodoFuncionario(PesquisaModel pesquisaModel);
        Result<bool> Salva(SessaoModel model);
        Result<bool> Exclui(int id);
    }
}
