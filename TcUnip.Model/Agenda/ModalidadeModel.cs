using System.Collections.Generic;
using TcUnip.Model.Cadastro;

namespace TcUnip.Model.Agenda
{
    public class ModalidadeModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<FuncionarioModel> Funcionarios { get; set; }
    }
}
