using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcUnip.Model.Agenda
{
    public class PesquisaModel
    {
        public int IdCadastro { get; set; }
        public string CpfCadastro { get; set; }
        public DateTime DataIncio { get; set; }
        public DateTime DataFim { get; set; }        
    }
}
