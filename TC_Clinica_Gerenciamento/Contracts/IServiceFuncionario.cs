using System.Collections.Generic;
using TC_Clinica_Gerenciamento.Models.Local;
using TC_Clinica_Gerenciamento.Models.Servico;

namespace TC_Clinica_Gerenciamento.Contracts
{
    public interface IServiceFuncionario
    {
        ResultService<Funcionario> Get(string cpf);
        ResultService<List<Funcionario>> List();
        ResultService<bool> Save(Funcionario model);        
        ResultService<bool> Delete(string cpf);
    }
}
