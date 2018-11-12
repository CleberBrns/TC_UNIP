using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcUnip.Model.Agenda;
using TcUnip.Model.Common;
using TcUnip.Model.FluxoCaixa;

namespace TcUnip.Service.Contract.FluxoCaixa
{
    public interface IFluxoCaixa
    {
        #region Caixa

        Result<CaixaModel> GetCaixa(int id);
        Result<List<CaixaModel>> ListCaixaPeriodo(PesquisaModel pesquisaModel);
        Result<CaixaModel> ListCaixaPeriodoPaciente(PesquisaModel pesquisaModel);
        Result<CaixaModel> ListCaixaPeriodoFuncionario(PesquisaModel pesquisaModel);

        #endregion

        #region Recibo

        Result<ReciboModel> GetRecibo(string id);
        Result<List<ReciboModel>> ListRecibosPeriodo(PesquisaModel pesquisaModel);
        Result<List<ReciboModel>> ListRecibosDoDia();
        Result<List<ReciboModel>> ListRecibosPeriodoPaciente(PesquisaModel pesquisaModel);
        Result<List<ReciboModel>> ListRecibosPeriodoFuncionario(PesquisaModel pesquisaModel);

        #endregion
    }
}
