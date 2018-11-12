using AutoMapper;
using TcUnip.Data.Contract.Cadastro;
using TcUnip.Data.Entity.Modelagem.Cadastro;
using TcUnip.Model.Cadastro;

namespace TcUnip.Data.Repositories.Cadastro
{
    public class TipoPerfilRepository : RepositoryBase<TipoPerfilModel, TipoPerfil>, ITipoPerfilRepository
    {
        public TipoPerfilRepository(IMapper mapper) : base(mapper) { }
    }
}
