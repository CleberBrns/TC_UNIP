using System.Collections.Generic;
using TcUnip.Model.Agenda;
using TcUnip.Model.Common;
using TcUnip.Model.FluxoCaixa;

namespace TcUnip.Service.Contract.FluxoCaixa
{
    public interface ICaixaService
    {
        Result<CaixaModel> Get(int id);
        Result<List<CaixaModel>> ListCaixaPeriodo(PesquisaModel pesquisaModel);
        Result<CaixaModel> ListCaixaPeriodoPaciente(PesquisaModel pesquisaModel);
        Result<CaixaModel> ListCaixaPeriodoFuncionario(PesquisaModel pesquisaModel);
    }
}
