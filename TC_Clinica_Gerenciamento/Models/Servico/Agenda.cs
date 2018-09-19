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
        [JsonProperty(PropertyName = "valor")]
        public string Horario { get; set; }
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
    }
}