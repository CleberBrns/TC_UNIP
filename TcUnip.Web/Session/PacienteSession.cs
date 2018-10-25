using System;
using System.Linq;
using TCC_Unip.Contracts.Session;
using TCC_Unip.Models.Servico;
using TCC_Unip.SessionBase;

namespace TCC_Unip.Session
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
