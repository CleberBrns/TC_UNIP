using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcUnip.Model.Cadastro;
using TcUnip.Model.Common;

namespace TcUnip.Service.Contract.Cadastro
{
    public interface ICadastroService
    {
        #region Usuario

        Result<UsuarioModel> GetUsuario(int id);
        Result<List<UsuarioModel>> ListUsuario();
        Result<UsuarioModel> AutenticaUsuario(UsuarioModel model);
        Result<bool> SalvaUsuario(UsuarioModel model);
        Result<bool> ExcluiUsuario(int id);

        #endregion

        #region Paciente

        Result<PacienteModel> GetPaciente(int id);
        Result<List<PacienteModel>> ListPaciente();
        Result<bool> SalvaPaciente(PacienteModel model);
        Result<bool> ExcluiPaciente(int id);

        #endregion

        #region Funcionario

        Result<FuncionarioModel> GetFuncionario(int id);
        Result<List<FuncionarioModel>> ListFuncionario();
        Result<List<FuncionarioModel>> ListProfissionais();
        Result<bool> SalvaFuncionario(FuncionarioModel model);
        Result<bool> ExcluiFuncionario(int id);       

        #endregion
    }
}
