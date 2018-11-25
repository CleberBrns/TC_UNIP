using System.ComponentModel.DataAnnotations;
using TcUnip.Model.Common;

namespace TcUnip.Model.Cadastro
{
    public class ModalidadeFuncionarioModel
    {
        [Required(ErrorMessage = "Requerido!")]
        public int IdModalidade { get; set; }
        public ModalidadeModel Modalidade { get; set; }

        [Required(ErrorMessage = "Requerido!")]
        public int IdFuncionario { get; set; }
        public FuncionarioModel Funcionario { get; set; }
    }
}
