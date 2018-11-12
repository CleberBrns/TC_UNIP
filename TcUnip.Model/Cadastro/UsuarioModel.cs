namespace TcUnip.Model.Cadastro
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
        public bool Excluido { get; set; }

        public int? IdFuncionario { get; set; }
        public FuncionarioModel Funcionario { get; set; }

        public int IdTipoPerfil { get; set; }
        public TipoPerfilModel TipoPerfil { get; set; }
    }
}
