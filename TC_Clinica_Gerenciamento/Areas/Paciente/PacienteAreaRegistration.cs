using System.Web;
using System.Web.Mvc;

namespace TC_Clinica_Gerenciamento.Areas.Paciente
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
                new[] { "TC_Clinica_Gerenciamento.Areas.Paciente.Controllers" }
            );
        }
    }
}