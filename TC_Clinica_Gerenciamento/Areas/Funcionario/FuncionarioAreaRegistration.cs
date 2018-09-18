using System.Web;
using System.Web.Mvc;

namespace TCC_Unip.Areas.Funcionario
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
                new[] { "TCC_Unip.Areas.Funcionario.Controllers" }
            );
        }
    }
}