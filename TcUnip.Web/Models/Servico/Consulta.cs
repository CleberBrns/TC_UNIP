using System;
using Newtonsoft.Json;

namespace TcUnip.Web.Models.Servico
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

        public DateTime FromMilliseconds(string dateService)
        {
            DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var dataRetornoMS = UnixEpoch.AddMilliseconds(Convert.ToInt64(dateService));

            return dataRetornoMS;
        }
    }
}