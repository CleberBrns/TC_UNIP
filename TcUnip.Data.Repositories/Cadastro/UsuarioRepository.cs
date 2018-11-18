using AutoMapper;
using System.Collections.Generic;
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

        public UsuarioModel GetById(int id)
        {
            using (var context = new TcUnipContext())
            {
                return Mapper.Map<UsuarioModel>(
                    context.Usuario.Where(x => x.Id == id)
                                   .Include(x => x.TipoPerfil)
                                   .Include(x => x.Funcionario.Pessoa)
                                   .FirstOrDefault()
                );
            }
        }

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

        public List<UsuarioModel> ListUsuarios()
        {
            using (var context = new TcUnipContext())
            {
                return Mapper.Map<List<UsuarioModel>>(
                    context.Usuario.Where(x => !x.Excluido)
                                   .Include(x => x.TipoPerfil)
                                   .Include(x => x.Funcionario.Pessoa)
                                   .AsNoTracking()
                                   .ToList()
                    );
            }
        }
    }
}
