using System.Collections.Generic;
using TcUnip.Model.Cadastro;

namespace TcUnip.Data.Contract.Cadastro
{
    public interface IPacienteRepository : IRepositoryBase<PacienteModel>
    {
        PacienteModel GetById(int id);
        PacienteModel GetByCpf(string cpf);
        List<PacienteModel> ListPacientes();
    }
}
