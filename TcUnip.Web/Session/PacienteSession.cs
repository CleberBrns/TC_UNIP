using System;
using System.Linq;
using TcUnip.Web.Contracts.Session;
using TcUnip.Web.Models.Servico;
using TcUnip.Web.SessionBase;

namespace TcUnip.Web.Session
{
    public class PacienteSession : SessionBase<Paciente>, ISessionPaciente
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
