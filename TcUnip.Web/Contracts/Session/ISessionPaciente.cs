using System;
using TCC_Unip.Models.Servico;

namespace TCC_Unip.Contracts.Session
{
    public interface ISessionPaciente : ISessionBase<Paciente>
    {
        Tuple<Paciente, bool> GetFromListSession(string cpf, string session);
    }
}
