using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcUnip.Model.FluxoCaixa
{
    public class ReciboModel
    {
        public int IdAgenda { get; set; }
        public string Paciente { get; set; }
        public DateTime Data { get; set; }
        public string Profissional { get; set; }
        public string Valor { get; set; }
    }
}
