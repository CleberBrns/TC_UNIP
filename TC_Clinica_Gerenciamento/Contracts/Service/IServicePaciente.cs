using System.Collections.Generic;
using TCC_Unip.Models.Local;
using TCC_Unip.Models.Servico;

namespace TCC_Unip.Contracts.Service
{
    public interface IServicePaciente
    {
        ResultService<Paciente> Get(string cpf);
        ResultService<List<Paciente>> List();
        ResultService<bool> Save(Paciente model);        
        ResultService<bool> Delete(string cpf);
    }
}
