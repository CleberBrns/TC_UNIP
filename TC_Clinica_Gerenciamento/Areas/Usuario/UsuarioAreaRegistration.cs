using System.Web;
using System.Web.Mvc;

namespace TC_Clinica_Gerenciamento.Areas.Usuario
{
    public class UsuarioAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Usuario";
            }
        }  

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Usuario_default",
                "Usuario/{controller}/{action}/{id}",
                new { Area = "Usuario", Controller = "Usuario", action = "Listagem", id = UrlParameter.Optional },
                new[] { "TC_Clinica_Gerenciamento.Areas.Usuario.Controllers" }
            );
        }
    }
}