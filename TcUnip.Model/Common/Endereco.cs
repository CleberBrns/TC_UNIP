using Newtonsoft.Json;

namespace TcUnip.Model.Common
{
    public class Endereco
    {
        [JsonProperty(PropertyName = "estado")]
        public string Estado { get; set; }
        [JsonProperty(PropertyName = "cidade")]
        public string Cidade { get; set; }
        [JsonProperty(PropertyName = "bairro")]
        public string Bairro { get; set; }
        [JsonProperty(PropertyName = "logradouro")]
        public string Logradouro { get; set; }
        [JsonProperty(PropertyName = "numero")]
        public string Numero { get; set; }
        [JsonProperty(PropertyName = "complemento")]
        public string Complemento { get; set; }
        [JsonProperty(PropertyName = "cep")]
        public string Cep { get; set; }

        public Endereco GetModelDefault()
        {
            return new Endereco
            {
                Bairro = string.Empty,
                Cep = string.Empty,
                Cidade = string.Empty,
                Complemento = string.Empty,
                Estado = string.Empty,
                Logradouro = string.Empty,
                Numero = string.Empty
            };
        }
    }
}
