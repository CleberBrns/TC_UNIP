using System.Collections.Generic;
using TcUnip.Model.Cadastro;
using TcUnip.Model.Common;

namespace TcUnip.Service.Contract.Cadastro
{
    public interface IPacienteService
    {
        Result<PacienteModel> Get(int id);
        Result<List<PacienteModel>> List();
        Result<bool> Salva(PacienteModel model);
        Result<bool> Exclui(int id);
    }
}
