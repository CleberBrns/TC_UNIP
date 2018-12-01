using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcUnip.Model.Agenda;

namespace TcUnip.Model.FluxoCaixa
{
    public class CaixaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Requerido!")]
        public DateTime Data { get; set; }

        [StringLength(100, ErrorMessage = "O {0} não pode conter mais que {1} caracteres.")]
        [Required(ErrorMessage = "Requerido!")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public string DebitoCadastro { get; set; }
        public string CreditoCadastro { get; set; }
        public decimal Debito { get; set; }
        public decimal Credito { get; set; }
        public decimal Saldo { get; set; }

        public int IdSessao { get; set; }
        public SessaoModel Sessao { get; set; } 
    }
}
