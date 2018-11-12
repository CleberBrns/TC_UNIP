namespace TcUnip.Model.Cadastro
{
    public class PacienteModel
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public bool Excluido { get; set; }
        
        public int IdPessoa { get; set; }
        public PessoaModel Pessoa { get; set; }
    }
}
