using System;
using TcUnip.Model.Pessoa;

namespace TcUnip.Session.Contract
{
    public interface IFuncionarioSN : IBaseSession<Funcionario>
    {
        Tuple<Funcionario, bool> GetFromListSession(string cpf, string session);
    }
}
