using System;
using System.Linq;
using TcUnip.Model.Pessoa;
using TcUnip.Session.Common;
using TcUnip.Session.Contract;

namespace TcUnip.Session.Pessoa
{
    public class PacienteSN : SessionBase<Paciente>, IPacienteSN
    {
        public Tuple<Paciente, bool> GetFromListSession(string cpf, string sessionName)
        {
            var sessionValida = false;
            var model = new Paciente();

            var retornolistFromSession = this.GetListFromSession(sessionName);

            if (retornolistFromSession.Item2 && retornolistFromSession.Item1.Count > 0)
            {
                sessionValida = true;
                model = retornolistFromSession.Item1.Where(l => l.Cpf.Equals(cpf)).FirstOrDefault();
            }

            return new Tuple<Paciente, bool>(model, sessionValida);
        }
    }
}
