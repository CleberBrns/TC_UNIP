using AutoMapper;
using TcUnip.Data.Contract.Common;
using TcUnip.Data.Entity.Modelagem.Common;
using TcUnip.Model.Common;

namespace TcUnip.Data.Repositories.Common
{
    public class ModalidadeRepository : RepositoryBase<ModalidadeModel, Modalidade>, IModalidadeRepository
    {
        public ModalidadeRepository(IMapper mapper) : base(mapper) { }
    }
}
