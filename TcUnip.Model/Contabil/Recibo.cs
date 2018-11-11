using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcUnip.Model.Contabil
{
    public class Recibo
    {
        public int IdAgenda { get; set; }
        public string Paciente { get; set; }
        public DateTime Data { get; set; }
        public string Profissional { get; set; }
        public string Valor { get; set; }
    }
}
