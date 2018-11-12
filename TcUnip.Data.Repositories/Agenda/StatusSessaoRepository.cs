using AutoMapper;
using TcUnip.Data.Contract.Agenda;
using TcUnip.Data.Entity.Modelagem.Agenda;
using TcUnip.Model.Agenda;

namespace TcUnip.Data.Repositories.Agenda
{
    public class StatusSessaoRepository : RepositoryBase<StatusSessaoModel, StatusSessao>, IStatusSessaoRepository
    {
        public StatusSessaoRepository(IMapper mapper) : base(mapper) { }
    }
}
