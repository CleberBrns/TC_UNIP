using System.Collections.Generic;
using TcUnip.Web.Models.Local;
using TcUnip.Web.Models.Servico;

namespace TcUnip.Web.Contracts.Service
{
    public interface IServicePaciente
    {
        ResultService<Paciente> Get(string cpf);
        ResultService<List<Paciente>> List(bool getFromSession);
        ResultService<bool> Save(Paciente model);        
        ResultService<bool> Delete(string cpf);
    }
}
