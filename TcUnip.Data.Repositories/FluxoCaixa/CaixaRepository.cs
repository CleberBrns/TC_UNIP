using AutoMapper;
using TcUnip.Data.Contract.FluxoCaixa;
using TcUnip.Data.Entity.Modelagem.FluxoCaixa;
using TcUnip.Model.FluxoCaixa;

namespace TcUnip.Data.Repositories.FluxoCaixa
{
    public class CaixaRepository : RepositoryBase<CaixaModel, Caixa>, ICaixaRepository
    {
        public CaixaRepository(IMapper mapper) : base(mapper) { }        
    }
}
