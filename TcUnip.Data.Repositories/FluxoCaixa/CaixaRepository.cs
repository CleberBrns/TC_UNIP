using AutoMapper;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TcUnip.Data.Contract.FluxoCaixa;
using TcUnip.Data.Entity;
using TcUnip.Data.Entity.Modelagem.FluxoCaixa;
using TcUnip.Model.Common;
using TcUnip.Model.FluxoCaixa;

namespace TcUnip.Data.Repositories.FluxoCaixa
{
    public class CaixaRepository : RepositoryBase<CaixaModel, Caixa>, ICaixaRepository
    {
        public CaixaRepository(IMapper mapper) : base(mapper) { }

        public List<CaixaModel> ListCaixaPeriodo(PesquisaModel pesquisaModel)
        {
            using (var context = new TcUnipContext())
            {
                return Mapper.Map<List<CaixaModel>>(
                    context.Caixa.Where(x => x.Data >= pesquisaModel.DataIncio &&
                                             x.Data <= pesquisaModel.DataFim)
                                  .AsNoTracking()
                                  .ToList()
                    );
            }
        }
    }
}
