using AutoMapper;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TcUnip.Data.Contract.Cadastro;
using TcUnip.Data.Entity;
using TcUnip.Data.Entity.Modelagem.Cadastro;
using TcUnip.Model.Cadastro;

namespace TcUnip.Data.Repositories.Cadastro
{
    public class PacienteRepository : RepositoryBase<PacienteModel, Paciente>, IPacienteRepository
    {
        public PacienteRepository(IMapper mapper) : base(mapper) { }

        public PacienteModel GetById(int id)
        {
            using (var context = new TcUnipContext())
            {
                return Mapper.Map<PacienteModel>(
                    context.Paciente.Where(x => x.Id == id && !x.Excluido)
                                    .Include(x => x.Pessoa)
                                    .FirstOrDefault()
                );
            }
        }

        public PacienteModel GetByCpf(string cpf)
        {
            using (var context = new TcUnipContext())
            {
                return Mapper.Map<PacienteModel>(
                    context.Paciente.Where(x => x.Pessoa.Cpf == cpf)
                                    .Include(x => x.Pessoa)
                                    .FirstOrDefault()
                );
            }
        }

        public List<PacienteModel> ListPacientes()
        {
            using (var context = new TcUnipContext())
            {
                return Mapper.Map<List<PacienteModel>>(
                    context.Paciente.Where(x => !x.Excluido)
                                   .Include(x => x.Pessoa)
                                   .AsNoTracking()
                                   .ToList()
                    );
            }
        }
    }
}
