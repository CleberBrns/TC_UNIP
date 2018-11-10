using AutoMapper;
using TcUnip.Data.Contract.Agenda;
using TcUnip.Data.Entity.Modelagem.Agenda;
using TcUnip.Model.Agenda;

namespace TcUnip.Data.Repositories.Cadastro
{
    public class UsuarioRepository : RepositoryBase<ModalidadeModel, Modalidade>, IModalidadeRepository
    {
        public UsuarioRepository(IMapper mapper) : base(mapper) { }
    }
}
