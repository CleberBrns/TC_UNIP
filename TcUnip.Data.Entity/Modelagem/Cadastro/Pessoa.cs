namespace TcUnip.Data.Entity.Modelagem.Cadastro
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Rua { get; set; }
        public string Cep { get; set; }
        public string Complemento { get; set; }
        public bool Ativo { get; set; }
        public bool Excluido { get; set; }
    }
}
