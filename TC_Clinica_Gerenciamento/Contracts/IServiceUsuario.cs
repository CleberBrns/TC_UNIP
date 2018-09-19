using System.Collections.Generic;
using TCC_Unip.Models.Local;
using TCC_Unip.Models.Servico;

namespace TCC_Unip.Contracts
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
