using System.Collections.Generic;
using TcUnip.Web.Models.Local;
using TcUnip.Web.Models.Servico;

namespace TcUnip.Web.Contracts.Service
{
    public interface IServiceFuncionario
    {
        ResultService<Funcionario> Get(string cpf);
        ResultService<List<Funcionario>> List(bool getFromSession);
        ResultService<bool> Save(Funcionario model);        
        ResultService<bool> Delete(string cpf);
    }
}
