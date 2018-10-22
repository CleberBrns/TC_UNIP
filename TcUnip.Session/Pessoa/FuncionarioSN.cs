using System;
using System.Linq;
using TcUnip.Model.Pessoa;
using TcUnip.Session.Common;
using TcUnip.Session.Contract;

namespace TcUnip.Session.Pessoa
{
    public class FuncionarioSN : SessionBase<Funcionario>, IFuncionarioSN
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
