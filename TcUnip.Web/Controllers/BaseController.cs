using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TCC_Unip.Models.Servico;
using TCC_Unip.Session;

namespace TCC_Unip.Controllers
{
    public class BaseController : Controller
    {
        public Tuple<Usuario, bool> GetUsuarioSession()
        {
            UsuarioSession sessionUsuario = new UsuarioSession();
            return sessionUsuario.GetModelFromSession(Constants.ConstSessions.usuario);
        }

        public void ValidaAutorizaoAcessoUsuario(string permissaoAcesso)
        {
            var userInfo = GetUsuarioSession();
            if (userInfo.Item2)
            {
                if (!permissaoAcesso.Contains(userInfo.Item1.Permissoes.FirstOrDefault()))
                    BadRequestCustomizado((int)HttpStatusCode.Unauthorized);
            }
            else
                BadRequestCustomizado((int)HttpStatusCode.RequestTimeout);
        }

        public void BadRequestCustomizado(int statusCode)
        {
            HttpContext.Response.Clear();
            HttpContext.Response.TrySkipIisCustomErrors = true;

            HttpContext.Response.StatusCode = statusCode;
        }
    }
}