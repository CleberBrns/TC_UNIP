using System;
using System.Linq;
using TCC_Unip.Contracts.Session;
using TCC_Unip.Models.Servico;

namespace TCC_Unip.Session
{
    public class SessionUsuario : SessionBase<Usuario>, ISessionUsuario
    {
        public Tuple<Usuario, bool> GetFromListSession(string email, string session)
        {
            var sessionValida = false;
            var model = new Usuario();

            var retornolistFromSession = this.GetListFromSession(session);

            if (retornolistFromSession.Item2 && retornolistFromSession.Item1.Count > 0)
            {
                sessionValida = true;
                model = retornolistFromSession.Item1.Where(l => l.Email.Equals(email)).FirstOrDefault();
            }

            return new Tuple<Usuario, bool>(model, sessionValida);
        }

    }
}
