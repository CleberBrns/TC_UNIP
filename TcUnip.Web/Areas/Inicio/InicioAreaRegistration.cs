using System.Web;
using System.Web.Mvc;

namespace TcUnip.Web.Areas.Inicio
{
    public class InicioAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Inicio";
            }
        }  

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Inicio_default",
                "Inicio/{controller}/{action}/{id}",
                new { Area = "Inicio", Controller = "Inicio", action = "Index", id = UrlParameter.Optional },
                new[] { "TcUnip.Web.Areas.Inicio.Controllers" }
            );
        }
    }
}