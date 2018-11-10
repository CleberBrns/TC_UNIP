using AutoMapper;
using TcUnip.Data.Contract.Cadastro;
using TcUnip.Data.Entity.Modelagem.Cadastro;
using TcUnip.Model.Cadastro;

namespace TcUnip.Data.Repositories.Cadastro
{
    public class PacienteRepository : RepositoryBase<PacienteModel, Paciente>, IPacienteRepository
    {
        public PacienteRepository(IMapper mapper) : base(mapper) { }
    }
}
