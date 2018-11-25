using System;
using System.ComponentModel.DataAnnotations;
using TcUnip.Model.Cadastro;
using TcUnip.Model.Common;

namespace TcUnip.Model.Agenda
{
    public class SessaoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Requerido!")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Requerido!")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Requerido!")]
        public string Horario { get; set; }

        [Required(ErrorMessage = "Requerido!")]
        public bool Excluido { get; set; }

        [Required(ErrorMessage = "Requerido!")]
        public int IdPaciente { get; set; }
        public PacienteModel Paciente { get; set; }

        [Required(ErrorMessage = "Requerido!")]
        public int IdFuncionario { get; set; }
        public virtual FuncionarioModel Funcionario { get; set; }

        [Required(ErrorMessage = "Requerido!")]
        public int IdModalidade { get; set; }
        public virtual ModalidadeModel Modalidade { get; set; }
    }
}
