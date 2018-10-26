using System.Collections.Generic;
using TcUnip.Model.Common;
using TcUnip.Model.Pessoa;

namespace TcUnip.Service.Contract.Pessoa
{
    public interface IPacienteService
    {
        Result<Paciente> Get(string cpf);
        Result<List<Paciente>> List();
        Result<bool> Salva(Paciente model);
        Result<bool> Exclui(string cpf);
    }
}
