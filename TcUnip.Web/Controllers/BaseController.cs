using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TcUnip.Model.Cadastro;
using TcUnip.Model.Common;
using TcUnip.Web.Session;

namespace TcUnip.Web.Controllers
{
    public class BaseController : Controller
    {
        public void ValidaAutorizaoAcessoUsuario(string permissaoAcesso)
        {
            var userInfo = GetUsuarioSession();
            if (userInfo.Item2)
            {
                if (!permissaoAcesso.Contains(userInfo.Item1.TipoPerfil.Permissao))
                    BadRequestCustomizado((int)HttpStatusCode.Unauthorized);
            }
            else
                BadRequestCustomizado((int)HttpStatusCode.RequestTimeout);
        }

        public Tuple<UsuarioModel, bool> GetUsuarioSession()
        {            
            UsuarioSession sessionUsuario = new UsuarioSession();
            return sessionUsuario.GetModelFromSession(Constants.ConstSessions.usuario);
        }

        public Tuple<List<TipoPerfilModel>, bool> GetListTipoPerfilSession()
        {
            SessionTipoPerfil sessionTipoPerfil = new SessionTipoPerfil();
            return sessionTipoPerfil.GetListFromSession(Constants.ConstSessions.listTipoPerfil);
        }

        public Tuple<List<ModalidadeModel>, bool> GetListModalidadesSession()
        {
            SessionModalidades sessionModalidades = new SessionModalidades();
            return sessionModalidades.GetListFromSession(Constants.ConstSessions.listModalidades);
        }

        public void BadRequestCustomizado(int statusCode)
        {
            HttpContext.Response.Clear();
            HttpContext.Response.TrySkipIisCustomErrors = true;

            HttpContext.Response.StatusCode = statusCode;
        }
    }
}