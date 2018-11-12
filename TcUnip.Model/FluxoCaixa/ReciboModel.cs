using System;

namespace TcUnip.Model.FluxoCaixa
{
    public class ReciboModel
    {
        public int IdSessao { get; set; }
        public string Paciente { get; set; }
        public DateTime Data { get; set; }
        public string Profissional { get; set; }
        public string Valor { get; set; }
    }
}
