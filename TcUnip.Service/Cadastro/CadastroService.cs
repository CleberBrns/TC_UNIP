using System;
using System.Collections.Generic;
using System.Linq;
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
        readonly IModalidadeFuncionarioRepository _modalidadeFuncionarioRepository;

        public CadastroService(IUsuarioRepository usuarioRepository, IPacienteRepository pacienteRepository, 
                               IFuncionarioRepository funcionarioRepository, IModalidadeFuncionarioRepository modalidadeFuncionarioRepository)
        {
            this._usuarioRepository = usuarioRepository;
            this._pacienteRepository = pacienteRepository;
            this._funcionarioRepository = funcionarioRepository;
            this._modalidadeFuncionarioRepository = modalidadeFuncionarioRepository;
        }

        #endregion

        #region Usuário

        public Result<UsuarioModel> GetUsuario(int id)
        {
            var result = new Result<UsuarioModel>();
            result.Value = _usuarioRepository.SelecionarUm(x => x.Id == id);

            if (result.Value == null)
            {
                result.Message = "O Usuário não existe mais na base de dados!";
                result.Status = false;
            }

            return result;
        }

        public Result<List<UsuarioModel>> ListUsuario()
        {
            var result = new Result<List<UsuarioModel>>();
            result.Value = _usuarioRepository.Lista(x => !x.Excluido).ToList();

            return result;
        }

        public Result<UsuarioModel> AutenticaUsuario(UsuarioModel model)
        {
            var result = new Result<UsuarioModel>();

            var retorno = _usuarioRepository.SelecionarUm(x => x.Senha == model.Senha && x.Email == model.Email);

            if (retorno != null)
                result.Value = retorno;
            else
            {
                result.Message = "Usuário Inválido ou Senha Incorreta";
                result.Status = false;                
            }

            return result;
        }

        public Result<bool> SalvaUsuario(UsuarioModel model)
        {
            var result = new Result<bool>();
            result.Value = false;
            result.Status = false;

            var emailExisteEmOutroUsuario = ValidaExistenciaEmail(model);

            if (emailExisteEmOutroUsuario.Item2)
            {
                result = emailExisteEmOutroUsuario.Item1;
            }
            else
            {
                if (model.Id != 0)
                {
                    model = _usuarioRepository.Salvar(model);
                    if (model.Id != 0)
                    {
                        result.Message = "Usuário salvo com sucesso!";
                        result.Value = true;
                        result.Status = true;
                    }
                    else                    
                        result.Message = "Falha ao salvar o Usuário!";
                }
                else
                {
                    if (string.IsNullOrEmpty(model.Senha))
                    {
                        var usuarioBD = _usuarioRepository.SelecionarUm(x => x.Id == model.Id);
                        model.Senha = usuarioBD.Senha;
                    }                   

                    result.Value = _usuarioRepository.Atualizar(model);

                    if (result.Value)
                    {
                        result.Message = "Usuário atualizado com sucesso!";
                        result.Value = true;
                        result.Status = true;
                    }
                    else                    
                        result.Message = "Falha ao atualizar o Usuário!";                    
                }                

            }

            return result;
        }

        public Result<bool> ExcluiUsuario(int id)
        {
            var result = new Result<bool>();
            result.Status = false;

            var modelBD = _usuarioRepository.SelecionarUm(x => x.Id == id);
            modelBD.Excluido = true;

            result.Value = _usuarioRepository.Atualizar(modelBD);

            if (result.Value)
            {
                result.Message = "Usuário excluído com sucesso!";
                result.Status = true;
            }
            else
                result.Message = "Falha ao excluir o Usuário!";

            return result;
        }

        private Tuple<Result<bool>, bool> ValidaExistenciaEmail(UsuarioModel model)
        {
            var result = new Result<bool>();

            var emailExistente = false;
            var usuario = _usuarioRepository.SelecionarUm(x => x.Email == model.Email &&
                                                               x.Id != model.Id);
            if (usuario != null)
            {
                result.Message = "E-mail vinculado a outro Usuário. Não é permitido sua utilização";
                result.Status = false;
                emailExistente = true;
            }

            return new Tuple<Result<bool>, bool>(result, emailExistente);
        }

        public bool AtualizaModalidades(List<ModalidadeFuncionarioModel> listModalidades)
        {
            bool atualizou = false;
            var idFuncionario = listModalidades.Select(x => x.IdFuncionario).FirstOrDefault();
            var modalidades = listModalidades.Select(x => x.IdModalidade).ToArray();

            var listBD = _modalidadeFuncionarioRepository.Lista(x => x.IdFuncionario == idFuncionario &&
                                                                     modalidades.Contains(x.IdModalidade))
                                                         .ToList();

            if (listBD.Count > 0)
            {
                _modalidadeFuncionarioRepository.SalvarLista(listModalidades);
            }
            else
            {
                
            }

            return atualizou;
        }

        #endregion

        #region Paciente

        public Result<PacienteModel> GetPaciente(int id)
        {
            var result = new Result<PacienteModel>();
            result.Value = _pacienteRepository.SelecionarUm(x => x.Id == id);

            if (result.Value == null)
            {
                result.Message = "O Paciente não existe mais na base de dados!";
                result.Status = false;
            }

            return result;
        }

        public Result<List<PacienteModel>> ListPaciente()
        {
            var result = new Result<List<PacienteModel>>();
            result.Value = _pacienteRepository.Lista(x => !x.Excluido).ToList();

            return result;
        }

        public Result<bool> SalvaPaciente(PacienteModel model)
        {
            var result = new Result<bool>();
            result.Value = false;
            result.Status = false;

            var cpfExisteEmOutroPaciente = ValidaExistenciaCpfPaciente(model);

            if (cpfExisteEmOutroPaciente.Item2)
            {
                result = cpfExisteEmOutroPaciente.Item1;
            }
            else
            {
                if (model.Id != 0)
                {
                    model = _pacienteRepository.Salvar(model);
                    if (model.Id != 0)
                    {
                        result.Message = "Paciente salvo com sucesso!";
                        result.Value = true;
                        result.Status = true;
                    }
                    else
                        result.Message = "Falha ao salvar o Paciente!";
                }
                else
                {
                    result.Value = _pacienteRepository.Atualizar(model);

                    if (result.Value)
                    {
                        result.Message = "Paciente atualizado com sucesso!";
                        result.Value = true;
                        result.Status = true;
                    }
                    else
                        result.Message = "Falha ao atualizar o Paciente!";
                }

            }

            return result;
        }


        public Result<bool> ExcluiPaciente(int id)
        {
            var result = new Result<bool>();
            result.Status = false;

            var modelBD = _pacienteRepository.SelecionarUm(x => x.Id == id);
            modelBD.Excluido = true;

            result.Value = _pacienteRepository.Atualizar(modelBD);

            if (result.Value)
            {
                result.Message = "Paciente excluído com sucesso!";
                result.Status = true;
            }
            else
                result.Message = "Falha ao excluir o Paciente!";

            return result;
        }

        private Tuple<Result<bool>, bool> ValidaExistenciaCpfPaciente(PacienteModel model)
        {
            var result = new Result<bool>();

            var cpfExistente = false;
            var usuario = _pacienteRepository.SelecionarUm(x => x.Pessoa.Cpf == model.Pessoa.Cpf &&
                                                               x.Id != model.Id, u => u.Pessoa);
            if (usuario != null)
            {
                result.Message = "CPF vinculado a outro Paciente. Não é permitido sua utilização";
                result.Status = false;
                cpfExistente = true;
            }

            return new Tuple<Result<bool>, bool>(result, cpfExistente);
        }

        #endregion

        #region Funcionario

        public Result<FuncionarioModel> GetFuncionario(int id)
        {
            var result = new Result<FuncionarioModel>();
            result.Value = _funcionarioRepository.SelecionarUm(x => x.Id == id);

            if (result.Value == null)
            {
                result.Message = "O Fucionário não existe mais na base de dados!";
                result.Status = false;
            }

            return result;
        }

        public Result<List<FuncionarioModel>> ListFuncionario()
        {
            var result = new Result<List<FuncionarioModel>>();
            result.Value = _funcionarioRepository.Lista(x => !x.Excluido).ToList();

            return result;
        }

        public Result<List<FuncionarioModel>> ListProfissionais()
        {
            var result = new Result<List<FuncionarioModel>>();
            result.Value = _funcionarioRepository.Lista(x => !x.Excluido).ToList();

            return result;
        }

        public Result<bool> SalvaFuncionario(FuncionarioModel model)
        {
            var result = new Result<bool>();
            result.Value = false;
            result.Status = false;

            var cpfExisteEmOutroFuncionario = ValidaExistenciaCpfFuncionario(model);

            if (cpfExisteEmOutroFuncionario.Item2)
            {
                result = cpfExisteEmOutroFuncionario.Item1;
            }
            else
            {
                if (model.Id != 0)
                {
                    model = _funcionarioRepository.Salvar(model);
                    if (model.Id != 0)
                    {
                        result.Message = "Funcionário salvo com sucesso!";
                        result.Value = true;
                        result.Status = true;
                    }
                    else
                        result.Message = "Falha ao salvar o Funcionário!";
                }
                else
                {
                    result.Value = _funcionarioRepository.Atualizar(model);

                    if (result.Value)
                    {
                        result.Message = "Funcionário atualizado com sucesso!";
                        result.Value = true;
                        result.Status = true;
                    }
                    else
                        result.Message = "Falha ao atualizar o Funcionário!";
                }

            }

            return result;
        }


        public Result<bool> ExcluiFuncionario(int id)
        {
            var result = new Result<bool>();
            result.Status = false;

            var modelBD = _funcionarioRepository.SelecionarUm(x => x.Id == id);
            modelBD.Excluido = true;

            result.Value = _funcionarioRepository.Atualizar(modelBD);

            if (result.Value)
            {
                result.Message = "Funcionário excluído com sucesso!";
                result.Status = true;
            }
            else
                result.Message = "Falha ao excluir o Funcionário!";

            return result;
        }

        private Tuple<Result<bool>, bool> ValidaExistenciaCpfFuncionario(FuncionarioModel model)
        {
            var result = new Result<bool>();

            var cpfExistente = false;
            var usuario = _funcionarioRepository.SelecionarUm(x => x.Pessoa.Cpf == model.Pessoa.Cpf &&
                                                               x.Id != model.Id, u => u.Pessoa);
            if (usuario != null)
            {
                result.Message = "CPF vinculado a outro Funcionário. Não é permitido sua utilização";
                result.Status = false;
                cpfExistente = true;
            }

            return new Tuple<Result<bool>, bool>(result, cpfExistente);
        }

        #endregion
    }
}
