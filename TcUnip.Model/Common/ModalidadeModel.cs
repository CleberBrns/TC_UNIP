using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TcUnip.Model.Cadastro;

namespace TcUnip.Model.Common
{
    public class ModalidadeModel
    {
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "O {0} não pode conter mais que {1} caracteres.")]
        [Required(ErrorMessage = "Requerido!")]
        public string Nome { get; set; }
    }
}
