using System;
using TcUnip.Model.Pessoa;

namespace TcUnip.Session.Contract
{
    public interface IUsuarioSN : IBaseSession<Usuario>
    {
        Tuple<Usuario, bool> GetFromListSession(string email, string sessionName);
    }
}
