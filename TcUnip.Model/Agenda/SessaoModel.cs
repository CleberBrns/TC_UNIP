using System;
using TcUnip.Model.Cadastro;

namespace TcUnip.Model.Agenda
{
    public class SessaoModel
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public bool Excluido { get; set; }

        public int IdPaciente { get; set; }
        public PacienteModel Paciente { get; set; }

        public int IdFuncioario { get; set; }
        public virtual FuncionarioModel Funcionario { get; set; }

        public int IdModalidade { get; set; }
        public virtual ModalidadeModel Modalidade { get; set; }

        public int IdStatus { get; set; }
        public virtual StatusSessaoModel StatusSessao { get; set; }
    }
}
