using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC_Unip.Contracts.Session;
using TCC_Unip.Models.Servico;

namespace TCC_Unip.Session
{
    public class FuncionarioSession : SessionBase<Funcionario>, ISessionFuncionario
    {
        public Tuple<Funcionario, bool> GetFromListSession(string cpf, string session)
        {
            var sessionValida = false;
            var model = new Funcionario();

            var retornolistFromSession = this.GetListFromSession(session);

            if (retornolistFromSession.Item2 && retornolistFromSession.Item1.Count > 0)
            {
                sessionValida = true;
                model = retornolistFromSession.Item1.Where(l => l.Cpf.Equals(cpf)).FirstOrDefault();
            }

            return new Tuple<Funcionario, bool>(model, sessionValida);
        }
    }
}
