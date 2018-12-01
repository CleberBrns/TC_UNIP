using System;
using System.ComponentModel.DataAnnotations.Schema;
using TcUnip.Data.Entity.Modelagem.Agenda;

namespace TcUnip.Data.Entity.Modelagem.FluxoCaixa
{
    public class Caixa
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        [Column(TypeName = "Money")]
        public decimal Debito { get; set; }
        [Column(TypeName = "Money")]
        public decimal Credito { get; set; }

        [ForeignKey("Sessao")]
        public int? IdSessao { get; set; }
        public virtual Sessao Sessao { get; set; }
    }
}
