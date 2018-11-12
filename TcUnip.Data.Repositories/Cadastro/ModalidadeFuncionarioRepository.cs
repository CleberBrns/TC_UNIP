using AutoMapper;
using TcUnip.Data.Contract.Cadastro;
using TcUnip.Data.Entity.Modelagem.Cadastro;
using TcUnip.Model.Cadastro;

namespace TcUnip.Data.Repositories.Cadastro
{
    public class ModalidadeFuncionarioRepository : 
        RepositoryBase<ModalidadeFuncionarioModel, ModalidadeFuncionario>, IModalidadeFuncionarioRepository
    {
        public ModalidadeFuncionarioRepository(IMapper mapper) : base(mapper) { }
    }
}
