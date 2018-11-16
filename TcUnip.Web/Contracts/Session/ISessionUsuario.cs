using System;
using TcUnip.Model.Cadastro;

namespace TcUnip.Web.Contracts.Session
{
    public interface ISessionUsuario : ISessionBase<UsuarioModel>
    {        
        Tuple<UsuarioModel, bool> GetFromListSession(string email, string sessionName);
    }
}
