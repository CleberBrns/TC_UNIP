using System.ComponentModel.DataAnnotations.Schema;
using TcUnip.Data.Entity.Modelagem.Agenda;

namespace TcUnip.Data.Entity.Modelagem.Cadastro
{
    public class ModalidadeFuncionario
    {        
        [ForeignKey("Modalidade")]
        public int IdModalidade { get; set; }
        public virtual Modalidade Modalidade { get; set; }
        [ForeignKey("Funcionario")]
        public int IdFuncionario { get; set; }
        public virtual Funcionario Funcionario { get; set; }
    }
}
