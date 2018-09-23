using System.Web.Mvc;
using TCC_Unip.Session;

namespace TCC_Unip.Areas.Inicio.Controllers
{
    public class InicioController : Controller
    {
        readonly UsuarioSession session = new UsuarioSession();
        readonly string sessionName = Constants.ConstSessions.usuario;

        public ActionResult Index()
        {
            if (!session.GetModelFromSession(sessionName).Item2)
                return RedirectToAction("Login", "Login", new { area = "" });

            ViewBag.Usuario = session.GetModelFromSession(sessionName).Item1;

            return View();
        }
    }
}