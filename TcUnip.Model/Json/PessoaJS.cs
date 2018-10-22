using Newtonsoft.Json;

namespace TcUnip.Model.Json
{
    public class PessoaJS
    {
        [JsonProperty(PropertyName = "cpf")]
        public string Cpf { get; set; }
    }
}
