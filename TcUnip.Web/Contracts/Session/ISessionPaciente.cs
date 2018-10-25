using System;
using TcUnip.Web.Models.Servico;

namespace TcUnip.Web.Contracts.Session
{
    public interface ISessionPaciente : ISessionBase<Paciente>
    {
        Tuple<Paciente, bool> GetFromListSession(string cpf, string session);
    }
}
