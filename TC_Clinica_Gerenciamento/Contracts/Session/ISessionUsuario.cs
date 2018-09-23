using System;
using TCC_Unip.Models.Servico;

namespace TCC_Unip.Contracts.Session
{
    public interface ISessionUsuario : ISessionBase<Usuario>
    {        
        Tuple<Usuario, bool> GetFromListSession(string email, string sessionName);
    }
}
