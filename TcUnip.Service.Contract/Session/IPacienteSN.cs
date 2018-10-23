using System;
using TcUnip.Model.Pessoa;

namespace TcUnip.Session.Contract
{
    public interface IPacienteSN : IBaseSession<Paciente>
    {
        Tuple<Paciente, bool> GetFromListSession(string cpf, string session);
    }
}
