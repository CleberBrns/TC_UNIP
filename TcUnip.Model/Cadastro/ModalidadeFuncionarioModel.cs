using TcUnip.Model.Common;

namespace TcUnip.Model.Cadastro
{
    public class ModalidadeFuncionarioModel
    {
        public int IdModalidade { get; set; }
        public ModalidadeModel Modalidade { get; set; }
        public int IdFuncionario { get; set; }
        public FuncionarioModel Funcionario { get; set; }
    }
}
