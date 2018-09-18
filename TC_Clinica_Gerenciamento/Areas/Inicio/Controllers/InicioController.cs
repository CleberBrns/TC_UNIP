using System.Web.Mvc;

namespace TCC_Unip.Areas.Inicio.Controllers
{
    public class InicioController : Controller
    {
        // GET: Inicio/Inicio
        public ActionResult Index()
        {
            if ((Models.Servico.Usuario)Session[Constants.ConstSessions.usuario] == null)
                return RedirectToAction("Login", "Login", new { area = "" });

            return View();
        }
    }
}