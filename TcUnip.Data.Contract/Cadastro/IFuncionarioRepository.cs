using System.Collections.Generic;
using TcUnip.Model.Cadastro;

namespace TcUnip.Data.Contract.Cadastro
{
    public interface IFuncionarioRepository : IRepositoryBase<FuncionarioModel>
    {
        FuncionarioModel GetById(int id);
        FuncionarioModel GetByCpf(string cpf);
        List<FuncionarioModel> ListFuncionarios();
    }
}
