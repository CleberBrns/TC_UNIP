using System.Collections.Generic;
using TcUnip.Model.Common;
using TcUnip.Model.Pessoa;

namespace TcUnip.Service.Contract.Pessoa
{
    public interface IUsuarioService
    {
        Result<Usuario> Get(string email);
        Result<List<Usuario>> List();
        Result<bool> Salva(Usuario model);
        Result<bool> Excliu(string email);
        Result<Usuario> Autentica(Usuario model);
    }
}
