using System.Web.Mvc;

namespace TC_Clinica_Gerenciamento.Areas.Inicio.Controllers
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