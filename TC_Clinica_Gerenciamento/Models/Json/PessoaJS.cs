using Newtonsoft.Json;

namespace TCC_Unip.Models.Json
{
    public class PessoaJS
    {
        [JsonProperty(PropertyName = "cpf")]
        public string Cpf { get; set; }
    }
}