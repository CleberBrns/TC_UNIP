using System.Web.Mvc;
using TCC_Unip.Util;

namespace TCC_Unip.Areas.Agenda.Controllers
{
    public class AgendaController : Controller
    {
        Mensagens mensagens = new Mensagens();
        public ActionResult Listagem()
        {
            if ((Models.Servico.Usuario)Session[Constants.ConstSessions.usuario] == null)
                return RedirectToAction("Login", "Login", new { area = "" });

            return View();
        }
    }
}