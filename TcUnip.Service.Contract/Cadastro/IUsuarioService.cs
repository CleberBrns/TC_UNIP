using System.Collections.Generic;
using TcUnip.Model.Cadastro;
using TcUnip.Model.Common;

namespace TcUnip.Service.Contract.Cadastro
{
    public interface IUsuarioService
    {
        Result<UsuarioModel> Get(int id);
        Result<List<UsuarioModel>> List();
        Result<bool> Salva(UsuarioModel model);
        Result<bool> Excliu(int id);
        Result<UsuarioModel> Autentica(UsuarioModel model);
    }
}
