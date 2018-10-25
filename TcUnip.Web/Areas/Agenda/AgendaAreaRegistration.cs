using System.Web;
using System.Web.Mvc;

namespace TCC_Unip.Areas.Agenda
{
    public class AgendaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Agenda";
            }
        }  

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Agenda_default",
                "Agenda/{controller}/{action}/{id}",
                new { Area = "Agenda", Controller = "Agenda", action = "Listagem", id = UrlParameter.Optional },
                new[] { "TCC_Unip.Areas.Agenda.Controllers" }
            );
        }
    }
}