using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TcUnip.Model.Cadastro
{
    public class FuncionarioModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Requerido!")]
        public bool Ativo { get; set; }

        [Required(ErrorMessage = "Requerido!")]
        public bool Excluido { get; set; }

        [Required(ErrorMessage = "Requerido!")]
        public int IdPessoa { get; set; }
        public PessoaModel Pessoa { get; set; }

        public List<ModalidadeFuncionarioModel> Modalidades { get; set; }
        public string[] IdsModalidades { get; set; }
    }
}
