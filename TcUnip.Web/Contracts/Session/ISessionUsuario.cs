using System;
using TcUnip.Model.Pessoa;

namespace TcUnip.Web.Contracts.Session
{
    public interface ISessionUsuario : ISessionBase<Usuario>
    {        
        Tuple<Usuario, bool> GetFromListSession(string email, string sessionName);
    }
}
