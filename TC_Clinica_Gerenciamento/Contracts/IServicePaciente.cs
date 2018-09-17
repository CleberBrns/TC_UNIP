using System.Collections.Generic;
using TC_Clinica_Gerenciamento.Models.Local;
using TC_Clinica_Gerenciamento.Models.Servico;

namespace TC_Clinica_Gerenciamento.Contracts
{
    public interface IServicePaciente
    {
        ResultService<Paciente> Get(string cpf);
        ResultService<List<Paciente>> List();
        ResultService<bool> Save(Paciente model);        
        ResultService<bool> Delete(string cpf);
    }
}
