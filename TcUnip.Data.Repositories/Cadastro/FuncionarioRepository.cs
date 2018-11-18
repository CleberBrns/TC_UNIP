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
    public class FuncionarioRepository : RepositoryBase<FuncionarioModel, Funcionario>, IFuncionarioRepository
    {
        public FuncionarioRepository(IMapper mapper) : base(mapper) { }

        public FuncionarioModel GetById(int id)
        {
            using (var context = new TcUnipContext())
            {
                return Mapper.Map<FuncionarioModel>(
                    context.Funcionario.Where(x => x.Id == id && !x.Excluido && x.Ativo)
                                    .Include(x => x.Pessoa)
                                    .Include(x => x.Modalidades)                                    
                                    .FirstOrDefault()
                );
            }
        }

        public FuncionarioModel GetByCpf(string cpf)
        {
            using (var context = new TcUnipContext())
            {
                return Mapper.Map<FuncionarioModel>(
                    context.Funcionario.Where(x => x.Pessoa.Cpf == cpf)
                                    .Include(x => x.Pessoa)
                                    .FirstOrDefault()
                );
            }
        }

        public List<FuncionarioModel> ListFuncionarios()
        {
            using (var context = new TcUnipContext())
            {
                return Mapper.Map<List<FuncionarioModel>>(
                    context.Funcionario.Where(x => !x.Excluido && x.Ativo)
                                   .Include(x => x.Pessoa)
                                   .Include(x => x.Modalidades)
                                   .AsNoTracking()
                                   .ToList()
                    );
            }
        }

        public List<FuncionarioModel> ListProfissionais()
        {
            using (var context = new TcUnipContext())
            {
                return Mapper.Map<List<FuncionarioModel>>(
                    context.Funcionario.Where(x => !x.Excluido && x.Ativo && x.Modalidades.Count > 0)
                                   .Include(x => x.Pessoa)
                                   .Include(x => x.Modalidades)
                                   .AsNoTracking()
                                   .ToList()
                    );
            }
        }
    }
}
