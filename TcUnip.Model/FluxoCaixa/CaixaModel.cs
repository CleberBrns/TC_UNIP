using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcUnip.Model.FluxoCaixa
{
    public class CaixaModel
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public string DebitoCadastro { get; set; }
        public string CreditoCadastro { get; set; }
        public decimal Debito { get; set; }
        public decimal Credito { get; set; }
        public decimal Saldo { get; set; }
    }
}
