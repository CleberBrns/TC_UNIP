using System.Web.Mvc;
using TCC_Unip.Models.Servico;
using TCC_Unip.Session;

namespace TCC_Unip.Controllers
{
    public class HomeController : Controller
    {
        readonly UsuarioSession session = new UsuarioSession();
        readonly string sessionName = Constants.ConstSessions.usuario;

        public ActionResult Index()
        {
            if (!session.GetModelFromSession(sessionName).Item2)
                return RedirectToAction("Login", "Login");

            return View();
        }
    }
}