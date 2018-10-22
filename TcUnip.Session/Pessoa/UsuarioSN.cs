using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcUnip.Model.Pessoa;
using TcUnip.Session.Common;
using TcUnip.Session.Contract;

namespace TcUnip.Session.Pessoa
{
    public class UsuarioSN : SessionBase<Usuario>, IUsuarioSN
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
