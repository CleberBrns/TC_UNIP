using System.ComponentModel.DataAnnotations.Schema;

namespace TcUnip.Data.Entity.Modelagem.Cadastro
{
    public class Paciente
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public bool Excluido { get; set; }

        [ForeignKey("Pessoa")]
        public int IdPessoa { get; set; }
        public virtual Pessoa Pessoa { get; set; }
    }
}
