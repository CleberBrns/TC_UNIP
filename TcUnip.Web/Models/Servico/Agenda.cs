using Newtonsoft.Json;
using System;

namespace TCC_Unip.Models.Servico
{
    public class Agenda
    {
        public int Id { get; set; }
        [JsonProperty(PropertyName = "paciente")]
        public Paciente Paciente { get; set; }
        [JsonProperty(PropertyName = "funcionario")]
        public Funcionario Funcionario { get; set; }
        [JsonProperty(PropertyName = "dateTime")]
        public string DateTimeService { get; set; }
        public DateTime Data { get; set; }        
        public string Horario { get; set; }
        [JsonProperty(PropertyName = "valor")]
        public decimal Valor { get; set; }
        [JsonProperty(PropertyName = "modalidade")]
        public string Modalidade { get; set; }

        public Agenda GetModelDefault()
        {
            var modelFunc = new Funcionario();
            var funcionario = modelFunc.GetModelDefault();

            var modelPaciente = new Paciente();
            var paciente = modelPaciente.GetModelDefault();

            return new Agenda
            {
                DateTimeService = string.Empty,
                Modalidade = string.Empty,
                Funcionario = funcionario,
                Paciente = paciente
            };
        }

        public string ToMilliseconds(DateTime data)
        {
            DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            var dataMS = (long)(data - UnixEpoch).TotalMilliseconds;
            return dataMS.ToString();
        }

        public DateTime FromMilliseconds(string dateService)
        {
            DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var dataRetornoMS = UnixEpoch.AddMilliseconds(Convert.ToInt64(dateService));

            return dataRetornoMS;
        }

        public DateTime CombinaDataHora(DateTime data, string hora)
        {
            return DateTime.Parse(data.ToString("dd/MM/yyyy") + " " + hora);
        }
    }
}