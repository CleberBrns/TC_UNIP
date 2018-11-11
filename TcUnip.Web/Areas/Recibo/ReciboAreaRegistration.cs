using System.Web;
using System.Web.Mvc;

namespace TcUnip.Web.Areas.Recibo
{
    public class ReciboAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Recibo";
            }
        }  

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Recibo_default",
                "Recibo/{controller}/{action}/{id}",
                new { Area = "Recibo", Controller = "Recibo", action = "Index", id = UrlParameter.Optional },
                new[] { "TcUnip.Web.Areas.Recibo.Controllers" }
            );
        }
    }
}