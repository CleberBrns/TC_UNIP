using System.ComponentModel.DataAnnotations;

namespace TcUnip.Model.Cadastro
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "O {0} não pode conter mais que {1} caracteres.")]
        [Required(ErrorMessage = "Requerido!")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [StringLength(14, ErrorMessage = "O {0} não pode conter mais que {1} caracteres.")]
        [Required(ErrorMessage = "Requerido!")]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [StringLength(100, ErrorMessage = "O {0} não pode conter mais que {1} caracteres.")]
        [Required(ErrorMessage = "Requerido!")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Requerido!")]
        public bool Ativo { get; set; }

        [Required(ErrorMessage = "Requerido!")]
        public bool Excluido { get; set; }

        public int? IdFuncionario { get; set; }
        public FuncionarioModel Funcionario { get; set; }

        [Required(ErrorMessage = "Requerido!")]
        public int IdTipoPerfil { get; set; }

        public TipoPerfilModel TipoPerfil { get; set; }
    }
}
