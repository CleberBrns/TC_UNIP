using AutoMapper;
using TcUnip.Data.Contract.Agenda;
using TcUnip.Data.Entity.Modelagem.Agenda;
using TcUnip.Model.Agenda;

namespace TcUnip.Data.Repositories.Cadastro
{
    public class TipoPerfilRepository : RepositoryBase<ModalidadeModel, Modalidade>, IModalidadeRepository
    {
        public TipoPerfilRepository(IMapper mapper) : base(mapper) { }
    }
}
