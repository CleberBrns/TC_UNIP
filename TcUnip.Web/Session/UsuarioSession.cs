using System;
using System.Linq;
using TcUnip.Model.Cadastro;
using TcUnip.Web.Contracts.Session;
using TcUnip.Web.SessionBase;

namespace TcUnip.Web.Session
{
    public class UsuarioSession : SessionBase<UsuarioModel>, ISessionUsuario
    {
        public Tuple<UsuarioModel, bool> GetFromListSession(string email, string sessionName)
        {
            var sessionValida = false;
            var model = new UsuarioModel();

            var retornolistFromSession = this.GetListFromSession(sessionName);

            if (retornolistFromSession.Item2 && retornolistFromSession.Item1.Count > 0)
            {
                sessionValida = true;
                model = retornolistFromSession.Item1.Where(l => l.Email.Equals(email)).FirstOrDefault();
            }

            return new Tuple<UsuarioModel, bool>(model, sessionValida);
        }

    }
}
