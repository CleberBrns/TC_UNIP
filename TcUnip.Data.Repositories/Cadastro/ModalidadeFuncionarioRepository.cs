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

        public void ExcluiLista(List<ModalidadeFuncionarioModel> modalidadeFuncionarios)
        {
            using (var context = new TcUnipContext())
            {
                foreach (var item in modalidadeFuncionarios)
                {
                    Excluir(item.IdFuncionario, item.IdModalidade, context);
                }
            }
        }

        private void Excluir(int idFuncionario, int idModalidade, TcUnipContext context)
        {

            context.ModalidadeFuncionario.Remove(
                    context.ModalidadeFuncionario.Where(x => x.IdFuncionario == idFuncionario &&
                                                             x.IdModalidade == idModalidade)
                                                             .FirstOrDefault()
                    );

            context.SaveChanges();
        }

    }
}
