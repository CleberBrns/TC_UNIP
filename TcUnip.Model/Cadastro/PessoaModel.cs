using System.ComponentModel.DataAnnotations;

namespace TcUnip.Model.Cadastro
{
    public class PessoaModel
    {
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "O {0} não pode conter mais que {1} caracteres.")]
        [Required(ErrorMessage = "Requerido!")]
        public string Nome { get; set; }

        [StringLength(14, ErrorMessage = "O {0} não pode conter mais que {1} caracteres.")]
        [Required(ErrorMessage = "Requerido!")]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [StringLength(100, ErrorMessage = "O {0} não pode conter mais que {1} caracteres.")]
        [Required(ErrorMessage = "Requerido!")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [StringLength(20, ErrorMessage = "O {0} não pode conter mais que {1} caracteres.")]
        [Required(ErrorMessage = "Requerido!")]
        public string Telefone { get; set; }

        [StringLength(500, ErrorMessage = "O {0} não pode conter mais que {1} caracteres.")]
        [Required(ErrorMessage = "Requerido!")]
        [Display(Name = "Endereço")]
        public string Logradouro { get; set; }

        [StringLength(9, ErrorMessage = "O {0} não pode conter mais que {1} caracteres.")]
        [Required(ErrorMessage = "Requerido!")]
        public string Cep { get; set; }

        [StringLength(500, ErrorMessage = "O {0} não pode conter mais que {1} caracteres.")]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "Requerido!")]
        public bool Ativo { get; set; }

        [Required(ErrorMessage = "Requerido!")]
        public bool Excluido { get; set; }
    }
}
