using System;
using TCC_Unip.Models.Servico;

namespace TCC_Unip.Contracts.Session
{
    public interface ISessionFuncionario : ISessionBase<Funcionario>
    {
        Tuple<Funcionario, bool> GetFromListSession(string cpf, string session);
    }
}
