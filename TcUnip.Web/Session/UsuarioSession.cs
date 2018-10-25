using System;
using System.Linq;
using TcUnip.Web.Contracts.Session;
using TcUnip.Web.Models.Servico;
using TcUnip.Web.SessionBase;

namespace TcUnip.Web.Session
{
    public class UsuarioSession : SessionBase<Usuario>, ISessionUsuario
    {
        public Tuple<Usuario, bool> GetFromListSession(string email, string sessionName)
        {
            var sessionValida = false;
            var model = new Usuario();

            var retornolistFromSession = this.GetListFromSession(sessionName);

            if (retornolistFromSession.Item2 && retornolistFromSession.Item1.Count > 0)
            {
                sessionValida = true;
                model = retornolistFromSession.Item1.Where(l => l.Email.Equals(email)).FirstOrDefault();
            }

            return new Tuple<Usuario, bool>(model, sessionValida);
        }

    }
}
