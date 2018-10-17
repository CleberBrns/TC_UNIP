using System;
using System.Net;
using System.Web.Mvc;
using TCC_Unip.Models.Servico;
using TCC_Unip.Session;

namespace TCC_Unip.Controllers
{
    public class BaseOneController : Controller
    {
        public Tuple<Usuario, bool> GetUsuarioSession()
        {
            UsuarioSession sessionUsuario = new UsuarioSession();
            return sessionUsuario.GetModelFromSession(Constants.ConstSessions.usuario);
        }

        public void AutorizaAcesso(string permissaoUsuario, string permissaoAcesso)
        {
            if (!permissaoAcesso.Contains(permissaoUsuario))            
                BadRequestCustomizado((int)HttpStatusCode.Unauthorized);            
        }

        public void BadRequestCustomizado(int statusCode)
        {
            HttpContext.Response.Clear();
            HttpContext.Response.TrySkipIisCustomErrors = true;

            HttpContext.Response.StatusCode = statusCode;
        }
    }
}