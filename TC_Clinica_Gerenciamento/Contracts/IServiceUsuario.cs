using System.Collections.Generic;
using TC_Clinica_Gerenciamento.Models.Local;
using TC_Clinica_Gerenciamento.Models.Servico;

namespace TC_Clinica_Gerenciamento.Contracts
{
    public interface IServiceUsuario
    {
        ResultService<Usuario> Get(string email);
        ResultService<List<Usuario>> List();
        ResultService<bool> Save(Usuario model);        
        ResultService<bool> Delete(string email);
        ResultService<Usuario> Auth(Usuario model);
    }
}
