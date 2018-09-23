using System;
using System.Linq;
using TCC_Unip.Contracts.Session;
using TCC_Unip.Models.Servico;
using TCC_Unip.SessionBase;

namespace TCC_Unip.Session
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
                sessionValida = true;
                model = retornolistFromSession.Item1.Where(l => l.Cpf.Equals(cpf)).FirstOrDefault();
            }

            return new Tuple<Funcionario, bool>(model, sessionValida);
        }
    }
}
