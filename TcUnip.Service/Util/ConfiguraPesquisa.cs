using System;
using TcUnip.Model.Common;

namespace TcUnip.Service.Util
{
    public class ConfiguraPesquisa
    {
        public PesquisaModel GetPesquisaDoDia()
        {
            return ConfiguraDatasPesquisa(new PesquisaModel {
                DataIncio = DateTime.Now,
                DataFim = DateTime.Now
            });
        }

        public PesquisaModel ConfiguraDatasPesquisa(PesquisaModel pesquisaModel)
        {
            var dataInicio = pesquisaModel.DataIncio;
            pesquisaModel.DataIncio = new DateTime(dataInicio.Year, dataInicio.Month, dataInicio.Day, 0, 00, 00);

            var dataFim = pesquisaModel.DataFim;
            pesquisaModel.DataFim = new DateTime(dataFim.Year, dataFim.Month, dataFim.Day, 23, 59, 59);

            return pesquisaModel;
        }
    }
}
