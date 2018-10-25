using System.Web;
using System.Web.Mvc;

namespace TcUnip.Web.Areas.Funcionario
{
    public class FuncionarioAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Funcionario";
            }
        }  

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Funcionario_default",
                "Funcionario/{controller}/{action}/{id}",
                new { Area = "Funcionario", Controller = "Funcionario", action = "Listagem", id = UrlParameter.Optional },
                new[] { "TcUnip.Web.Areas.Funcionario.Controllers" }
            );
        }
    }
}