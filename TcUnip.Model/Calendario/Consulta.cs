using Newtonsoft.Json;
using System;

namespace TcUnip.Model.Calendario
{
    public class Consulta
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "nome")]
        public string Nome { get; set; }
        [JsonProperty(PropertyName = "cpf")]
        public string Cpf { get; set; }
        [JsonProperty(PropertyName = "dateTime")]
        public string DateTimeService { get; set; }
        public DateTime Data { get; set; }
        [JsonProperty(PropertyName = "valor")]
        public decimal Valor { get; set; }
        [JsonProperty(PropertyName = "modalidade")]
        public string Modalidade { get; set; }
        public string Horario { get; set; }
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
    }
}
