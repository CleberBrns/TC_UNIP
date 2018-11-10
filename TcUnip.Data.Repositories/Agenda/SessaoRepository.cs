using AutoMapper;
using TcUnip.Data.Contract.Agenda;
using TcUnip.Data.Entity.Modelagem.Agenda;
using TcUnip.Model.Agenda;

namespace TcUnip.Data.Repositories.Agenda
{
    public class SessaoRepository : RepositoryBase<SessaoModel, Sessao>, ISessaoRepository
    {
        public SessaoRepository(IMapper mapper) : base(mapper) { }
    }
}
