using System.Web;
using System.Web.Mvc;

namespace TcUnip.Web.Areas.Caixa
{
    public class CaixaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Caixa";
            }
        }  

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Caixa_default",
                "Caixa/{controller}/{action}/{id}",
                new { Area = "Caixa", Controller = "Caixa", action = "Index", id = UrlParameter.Optional },
                new[] { "TcUnip.Web.Areas.Caixa.Controllers" }
            );
        }
    }
}