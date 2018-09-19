using System.Web.Mvc;
using TCC_Unip.Models.Servico;

namespace TCC_Unip.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if ((Usuario)Session[Constants.ConstSessions.usuario] == null)
                return RedirectToAction("Login", "Login");

            return View();
        }
    }
}