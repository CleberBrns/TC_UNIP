using System.Collections.Generic;
using TcUnip.Model.Cadastro;

namespace TcUnip.Data.Contract.Cadastro
{
    public interface IUsuarioRepository : IRepositoryBase<UsuarioModel>
    {
        UsuarioModel GetById(int id);
        UsuarioModel GetByEmail(string email);
        List<UsuarioModel> ListUsuarios();
    }
}
