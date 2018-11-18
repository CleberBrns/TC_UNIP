using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using TcUnip.Data.Contract.Cadastro;
using TcUnip.Data.Entity;
using TcUnip.Data.Entity.Modelagem.Cadastro;
using TcUnip.Model.Cadastro;

namespace TcUnip.Data.Repositories.Cadastro
{
    public class ModalidadeFuncionarioRepository : 
        RepositoryBase<ModalidadeFuncionarioModel, ModalidadeFuncionario>, IModalidadeFuncionarioRepository
    {
        public ModalidadeFuncionarioRepository(IMapper mapper) : base(mapper) { }

        public List<ModalidadeFuncionarioModel> ListModalidadesFuncionario(int idFuncionario)
        {
            using (var context = new TcUnipContext())
            {
                return Mapper.Map<List<ModalidadeFuncionarioModel>>(
                    context.ModalidadeFuncionario.Where(x => x.IdFuncionario == idFuncionario).ToList()
                    );
            }
        }

    }
}
