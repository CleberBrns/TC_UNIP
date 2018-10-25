using System;
using System.Linq;
using TcUnip.Web.Contracts.Session;
using TcUnip.Web.Models.Servico;
using TcUnip.Web.SessionBase;

namespace TcUnip.Web.Session
{
    public class FuncionarioSession : SessionBase<Funcionario>, ISessionFuncionario
    {
        public Tuple<Funcionario, bool> GetFromListSession(string cpf, string sessionName)
        {
            var sessionValida = false;
            var model = new Funcionario();

            var retornolistFromSession = this.GetListFromSession(sessionName);

            if (retornolistFromSession.Item2 && retornolistFromSession.Item1.Count > 0)
            {                
                model = retornolistFromSession.Item1.Where(l => l.Cpf.Equals(cpf)).FirstOrDefault();
                sessionValida = model != null;
            }

            return new Tuple<Funcionario, bool>(model, sessionValida);
        }
    }
}
