using Newtonsoft.Json;
using System.Collections.Generic;

namespace TCC_Unip.Models.Servico
{
    public class Funcionario
    {
        [JsonProperty(PropertyName = "nome")]
        public string Nome { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "cpf")]
        public string Cpf { get; set; }
        [JsonProperty(PropertyName = "telefone")]
        public string Telefone { get; set; }
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
        [JsonProperty(PropertyName = "modalidade")]
        public string[] Modalidade { get; set; }
        [JsonProperty(PropertyName = "endereco")]
        public Endereco Endereco { get; set; }        

        public Funcionario GetModelDefault()
        {
            var model = new Endereco();
            var endereco = model.GetModelDefault();

            return new Funcionario
            {
                Cpf = string.Empty,
                Email = string.Empty,
                Nome = string.Empty,
                Telefone = string.Empty,
                Status = string.Empty,
                Modalidade = new string[] { },
                Endereco = endereco
            };
        }

    }
}