using Newtonsoft.Json;
using TcUnip.Model.Calendario;

namespace TcUnip.Model.Json
{
    public class AgendaJS
    {
        [JsonProperty(PropertyName = "dateTime")]
        public string DateTime { get; set; }
        [JsonProperty(PropertyName = "valor")]
        public decimal Valor { get; set; }
        [JsonProperty(PropertyName = "modalidade")]
        public string Modalidade { get; set; }
        [JsonProperty(PropertyName = "paciente")]
        public PessoaJS Paciente { get; set; }
        [JsonProperty(PropertyName = "funcionario")]
        public PessoaJS Funcionario { get; set; }

        public AgendaJS ConvertToJS(Agenda agenda)
        {
            return new AgendaJS
            {
                Paciente = new PessoaJS
                {
                    Cpf = agenda.Paciente.Cpf
                },
                Funcionario = new PessoaJS
                {
                    Cpf = agenda.Funcionario.Cpf
                },
                DateTime = agenda.DateTimeService,
                Valor = agenda.Valor,
                Modalidade = agenda.Modalidade
            };
        }
    }
}
