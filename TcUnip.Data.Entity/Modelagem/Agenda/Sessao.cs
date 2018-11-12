using System;
using System.ComponentModel.DataAnnotations.Schema;
using TcUnip.Data.Entity.Modelagem.Cadastro;

namespace TcUnip.Data.Entity.Modelagem.Agenda
{
    public class Sessao
    {
        public int Id { get; set; }
        [Column(TypeName = "Money")]
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public bool Excluido { get; set; }

        [ForeignKey("Paciente")]
        public int IdPaciente { get; set; }
        public virtual Paciente Paciente { get; set; }

        [ForeignKey("Funcionario")]
        public int IdFuncioario { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        [ForeignKey("Modalidade")]
        public int IdModalidade { get; set; }
        public virtual Modalidade Modalidade { get; set; }
    }
}
