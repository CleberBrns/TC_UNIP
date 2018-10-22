using Newtonsoft.Json;

namespace TcUnip.Model.Pessoa
{
    public class Usuario
    {
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "senha")]
        public string Senha { get; set; }
        [JsonProperty(PropertyName = "cpf")]
        public string Cpf { get; set; }
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
        [JsonProperty(PropertyName = "permissoes")]
        public string[] Permissoes { get; set; }
        public string PermissaoExibicao { get; set; }
        [JsonProperty(PropertyName = "funcionario")]
        public Funcionario Funcionario { get; set; }

        public Usuario GetModelDefault()
        {
            var model = new Funcionario();
            var funcionario = model.GetModelDefault();

            return new Usuario
            {
                Cpf = string.Empty,
                Email = string.Empty,
                Senha = string.Empty,
                Status = string.Empty,
                Permissoes = new string[] { },
                Funcionario = funcionario
            };
        }
    }
}
