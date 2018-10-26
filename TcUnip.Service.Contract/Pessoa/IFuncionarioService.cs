using System.Collections.Generic;
using TcUnip.Model.Common;
using TcUnip.Model.Pessoa;

namespace TcUnip.Service.Contract.Pessoa
{
    public interface IFuncionarioService
    {
        Result<Funcionario> Get(string cpf);
        Result<List<Funcionario>> List();
        Result<List<Funcionario>> ListProfissionais();
        Result<bool> Salva(Funcionario model);
        Result<bool> Exclui(string cpf);
    }
}
