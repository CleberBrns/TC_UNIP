using System.Web;
using System.Web.Mvc;

namespace TcUnip.Web.Areas.Paciente
{
    public class PacienteAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Paciente";
            }
        }  

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Paciente_default",
                "Paciente/{controller}/{action}/{id}",
                new { Area = "Paciente", Controller = "Paciente", action = "Listagem", id = UrlParameter.Optional },
                new[] { "TcUnip.Web.Areas.Paciente.Controllers" }
            );
        }
    }
}