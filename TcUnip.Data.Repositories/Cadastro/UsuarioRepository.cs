using AutoMapper;
using TcUnip.Data.Contract.Cadastro;
using TcUnip.Data.Entity.Modelagem.Cadastro;
using TcUnip.Model.Cadastro;

namespace TcUnip.Data.Repositories.Cadastro
{
    public class UsuarioRepository : RepositoryBase<UsuarioModel, Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IMapper mapper) : base(mapper) { }
    }
}
