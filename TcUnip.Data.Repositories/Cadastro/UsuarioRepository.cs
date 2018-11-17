using AutoMapper;
using System.Data.Entity;
using System.Linq;
using TcUnip.Data.Contract.Cadastro;
using TcUnip.Data.Entity;
using TcUnip.Data.Entity.Modelagem.Cadastro;
using TcUnip.Model.Cadastro;

namespace TcUnip.Data.Repositories.Cadastro
{
    public class UsuarioRepository : RepositoryBase<UsuarioModel, Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IMapper mapper) : base(mapper) { }

        public UsuarioModel GetByEmail(string email)
        {
            using (var context = new TcUnipContext())
            {                
                return Mapper.Map<UsuarioModel>(
                    context.Usuario.Where(x => x.Email == email)
                                   .Include(x => x.TipoPerfil)
                                   .Include(x => x.Funcionario.Pessoa)
                                   .FirstOrDefault()
                );
            }
        }
    }
}
