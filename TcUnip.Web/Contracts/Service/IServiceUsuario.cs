using System.Collections.Generic;
using TcUnip.Web.Models.Local;
using TcUnip.Web.Models.Servico;

namespace TcUnip.Web.Contracts.Service
{
    public interface IServiceUsuario
    {
        ResultService<Usuario> Get(string email);
        ResultService<List<Usuario>> List(bool getFromSession);
        ResultService<bool> Save(Usuario model);        
        ResultService<bool> Delete(string email);
        ResultService<Usuario> Auth(Usuario model);        
    }
}
