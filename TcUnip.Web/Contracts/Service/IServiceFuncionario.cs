using System.Collections.Generic;
using TCC_Unip.Models.Local;
using TCC_Unip.Models.Servico;

namespace TCC_Unip.Contracts.Service
{
    public interface IServiceFuncionario
    {
        ResultService<Funcionario> Get(string cpf);
        ResultService<List<Funcionario>> List(bool getFromSession);
        ResultService<bool> Save(Funcionario model);        
        ResultService<bool> Delete(string cpf);
    }
}
