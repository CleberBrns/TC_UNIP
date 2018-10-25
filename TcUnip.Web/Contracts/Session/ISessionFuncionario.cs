using System;
using TcUnip.Web.Models.Servico;

namespace TcUnip.Web.Contracts.Session
{
    public interface ISessionFuncionario : ISessionBase<Funcionario>
    {
        Tuple<Funcionario, bool> GetFromListSession(string cpf, string session);
    }
}
