using System;

namespace TcUnip.Model.Common
{
    public class PesquisaModel
    {
        public int IdPesquisa { get; set; }
        public string CpfPesquisa { get; set; }
        public DateTime DataIncio { get; set; }
        public DateTime DataFim { get; set; }        
    }
}
