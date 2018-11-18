using System.Collections.Generic;
using TcUnip.Model.Cadastro;

namespace TcUnip.Data.Contract.Cadastro
{
    public interface IModalidadeFuncionarioRepository : IRepositoryBase<ModalidadeFuncionarioModel>
    {
        List<ModalidadeFuncionarioModel> ListModalidadesFuncionario(int idFuncionario);
        void ExcluiLista(List<ModalidadeFuncionarioModel> modalidadeFuncionarios);
    }
}
