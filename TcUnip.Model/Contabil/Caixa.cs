using Newtonsoft.Json;
using System;

namespace TcUnip.Model.Contabil
{
    public class Caixa
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "dateTime")]
        public string DateTimeService { get; set; }
        public DateTime Data { get; set; }
        public string Horario { get; set; }
        [JsonProperty(PropertyName = "descricao")]
        public string Descricao { get; set; }
        [JsonProperty(PropertyName = "debito")]
        public decimal Debito { get; set; }
        [JsonProperty(PropertyName = "credito")]
        public decimal Credito { get; set; }
        public string Saldo { get; set; }
        
        public Caixa GetModelDefault()
        {
            return new Caixa
            {
                Id = 0,
                DateTimeService = string.Empty,
                Data = DateTime.Now,
                Horario = DateTime.Now.ToLongTimeString(),
                Descricao = string.Empty,
                Debito = 0,
                Credito = 0
            };
        }

        public string GetSaldo(decimal Credito, decimal Debito)
        {
            return (Credito - Debito).ToString();
        }
    }
}
