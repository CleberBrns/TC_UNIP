using System.Collections.Generic;
using TcUnip.Data.Contract.Cadastro;
using TcUnip.Model.Cadastro;
using TcUnip.Model.Common;
using TcUnip.Service.Contract.Cadastro;

namespace TcUnip.Service.Cadastro
{
    public class CadastroService : ICadastroService
    {
        #region Propriedades e Construtor

        readonly IUsuarioRepository _usuarioRepository;
        readonly IPacienteRepository _pacienteRepository;
        readonly IFuncionarioRepository _funcionarioRepository;

        public CadastroService(IUsuarioRepository usuarioRepository, IPacienteRepository pacienteRepository, 
                               IFuncionarioRepository funcionarioRepository)
        {
            this._usuarioRepository = usuarioRepository;
            this._pacienteRepository = pacienteRepository;
            this._funcionarioRepository = funcionarioRepository;            
        }

        #endregion

        #region Usuario

        public Result<UsuarioModel> GetUsuario(int id)
        {
            var result = new Result<UsuarioModel>();
            result.Value = _usuarioRepository.SelecionarUm(u => u.Id);

            if (string.IsNullOrEmpty(result.Value.Email))
            {
                result.Message = "O Usuário não existe mais na base de dados do serviço!";
                result.Status = false;
            }

            return result;
        }

        public Result<List<UsuarioModel>> ListUsuario()
        {
            var result = new Result<List<UsuarioModel>>();
            result.Value = _usuarioRepository.Lista();

            return result;
        }

        public Result<UsuarioModel> AutenticaUsuario(UsuarioModel model)
        {
            var result = new Result<UsuarioModel>();

            var retorno = _usuarioRepository.SelecionarUm(u => u.Id == model.Id);

            if (retorno.Item2)
            {
                result.Value = retorno.Item1;
                if (string.IsNullOrEmpty(result.Value.Email))
                {
                    result.Message = "Falha ao recuperar o retorno da API!";
                    result.Status = false;
                }
            }
            else
            {
                result.Message = "Usuário Inválido ou Senha Incorreta";
                result.Status = false;
            }

            return result;
        }

        public Result<bool> SalvaUsuario(UsuarioModel model)
        {
            throw new System.NotImplementedException();
        }

        public Result<bool> ExcliuUsuario(int id)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Paciente

        public Result<PacienteModel> GetPaciente(int id)
        {
            throw new System.NotImplementedException();
        }

        public Result<List<PacienteModel>> ListPaciente()
        {
            throw new System.NotImplementedException();
        }

        public Result<bool> SalvaPaciente(PacienteModel model)
        {
            throw new System.NotImplementedException();
        }


        public Result<bool> ExcluiPaciente(int id)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Funcionario

        public Result<FuncionarioModel> GetFuncionario(int id)
        {
            throw new System.NotImplementedException();
        }

        public Result<List<FuncionarioModel>> ListFuncionario()
        {
            throw new System.NotImplementedException();
        }

        public Result<List<FuncionarioModel>> ListProfissionais()
        {
            throw new System.NotImplementedException();
        }

        public Result<bool> SalvaFuncionario(FuncionarioModel model)
        {
            throw new System.NotImplementedException();
        }


        public Result<bool> ExcluiFuncionario(int id)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}
