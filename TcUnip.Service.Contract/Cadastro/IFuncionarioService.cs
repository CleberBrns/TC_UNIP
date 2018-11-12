using System.Collections.Generic;
using TcUnip.Model.Cadastro;
using TcUnip.Model.Common;

namespace TcUnip.Service.Contract.Cadastro
{
    public interface IFuncionarioService
    {
        Result<FuncionarioModel> Get(int id);
        Result<List<FuncionarioModel>> List();
        Result<List<FuncionarioModel>> ListProfissionais();
        Result<bool> Salva(FuncionarioModel model);
        Result<bool> Exclui(int id);
    }
}
