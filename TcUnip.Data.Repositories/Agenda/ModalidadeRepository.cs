using AutoMapper;
using TcUnip.Data.Contract.Agenda;
using TcUnip.Data.Entity.Modelagem.Agenda;
using TcUnip.Model.Agenda;

namespace TcUnip.Data.Repositories.Agenda
{
    public class ModalidadeRepository : RepositoryBase<ModalidadeModel, Modalidade>, IModalidadeRepository
    {
        public ModalidadeRepository(IMapper mapper) : base(mapper) { }
    }
}
