using Newtonsoft.Json;

namespace TcUnip.Web.Models.Json
{
    public class PessoaJS
    {
        [JsonProperty(PropertyName = "cpf")]
        public string Cpf { get; set; }
    }
}