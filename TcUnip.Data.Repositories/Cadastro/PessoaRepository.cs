using AutoMapper;
using TcUnip.Data.Contract.Cadastro;
using TcUnip.Data.Entity.Modelagem.Cadastro;
using TcUnip.Model.Cadastro;

namespace TcUnip.Data.Repositories.Cadastro
{
    public class PessoaRepository : RepositoryBase<PessoaModel, Pessoa>, IPessoaRepository
    {
        public PessoaRepository(IMapper mapper) : base(mapper) { }
    }
}
