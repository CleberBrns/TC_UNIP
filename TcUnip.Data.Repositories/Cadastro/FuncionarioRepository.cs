using AutoMapper;
using TcUnip.Data.Contract.Cadastro;
using TcUnip.Data.Entity.Modelagem.Cadastro;
using TcUnip.Model.Cadastro;

namespace TcUnip.Data.Repositories.Cadastro
{
    public class FuncionarioRepository : RepositoryBase<FuncionarioModel, Funcionario>, IFuncionarioRepository
    {
        public FuncionarioRepository(IMapper mapper) : base(mapper) { }
    }
}
