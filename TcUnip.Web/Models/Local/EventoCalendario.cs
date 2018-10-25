using System;

namespace TcUnip.Web.Models.Local
{
    public class EventoCalendario
    {
        public int IdConsulta { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string DiaDaSemana { get; set; }
        public DateTime ComecaEm { get; set; }
        public DateTime TerminaEm { get; set; }
        public string CorEvento { get; set; }
        public bool EhODiaTodo { get; set; }
    }
}