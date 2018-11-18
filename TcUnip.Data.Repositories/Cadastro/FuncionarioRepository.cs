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
                var funcionario = context.Funcionario.Where(x => x.Id == id && !x.Excluido && x.Ativo)
                                     .Include(x => x.Pessoa)
                                     .Include(x => x.Modalidades)
                                     .FirstOrDefault();

                if (funcionario != null)
                    funcionario.Modalidades.ForEach(x => x.Funcionario = null);                

                return Mapper.Map<FuncionarioModel>(funcionario);
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
                var list = context.Funcionario.Where(x => !x.Excluido && x.Ativo)
                                   .Include(x => x.Pessoa)
                                   .AsNoTracking()
                                   .ToList();

                if (list.Count > 0)
                {
                    var idsFuncionario = list.Select(x => x.Id).ToArray();

                    var listModalidades = context.ModalidadeFuncionario.Include(x => x.Modalidade)
                                                                       .Where(x => idsFuncionario.Contains(x.IdFuncionario))
                                                                       .ToList();                    

                    list.ForEach(x => x.Modalidades =
                                      listModalidades.Where(l => l.IdFuncionario == x.Id)
                                                     .ToList());
                }
                    

                return Mapper.Map<List<FuncionarioModel>>(list);
            }
        }
    }
}
