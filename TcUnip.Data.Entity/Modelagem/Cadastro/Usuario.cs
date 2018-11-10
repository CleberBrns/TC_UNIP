using System.ComponentModel.DataAnnotations.Schema;

namespace TcUnip.Data.Entity.Modelagem.Cadastro
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Senha { get; set; }
        public string Permissao { get; set; }
        public bool Ativo { get; set; }
        public bool Excluido { get; set; }

        [ForeignKey("Funcionario")]
        public int IdFuncionario { get; set; }
        public virtual Funcionario Funcionario { get; set; }
    }
}
