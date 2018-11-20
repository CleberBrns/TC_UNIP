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
            result.Value = _usuarioRepository.GetById(id);

            if (result.Value == null)
            {
                result.Message = "O Usuário não existe mais na base de dados!";
                result.Status = false;
            }

            return result;
        }

        public Result<List<UsuarioModel>> ListUsuarios()
        {
            var result = new Result<List<UsuarioModel>>();
            result.Value = _usuarioRepository.ListUsuarios();

            return result;
        }

        public Result<UsuarioModel> AutenticaUsuario(UsuarioModel model)
        {           
            var usuarioValido = false;
            var result = new Result<UsuarioModel>();
            result.Status = false;

            if (string.IsNullOrEmpty(model.Email.Trim()) || string.IsNullOrEmpty(model.Senha.Trim()))            
                result.Message = "O campo E-mail e Senha são obrigatórios!";            
            else
            {
                var retorno = _usuarioRepository.GetByEmail(model.Email);

                if (retorno != null)
                {
                    if (retorno.Excluido)
                        result.Message = "Usuário excluído!";
                    else if (!retorno.Ativo)
                        result.Message = "Usuário inativo!";
                    else if (model.Senha != retorno.Senha)
                        result.Message = "Senha inválida!";
                    else
                        usuarioValido = true;
                }
                else
                    result.Message = "Usuário inválido!";

                if (usuarioValido)
                {
                    result.Value = retorno;
                    result.Status = usuarioValido;
                }
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
                model.IdFuncionario = model.IdFuncionario.Value == 0  ? (int?)null : model.IdFuncionario;
                model.Excluido = false;
                if (model.Id == 0)
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
                        var usuarioBD = _usuarioRepository.GetById(model.Id);
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

            var modelBD = _usuarioRepository.GetById(id);
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
            var usuario = _usuarioRepository.GetByEmail(model.Email);

            if (usuario != null && usuario.Id != model.Id)
            {
                result.Message = "E-mail já utilizado na base de dados, não é permitido sua reutilização";
                result.Status = false;
                emailExistente = true;
            }

            return new Tuple<Result<bool>, bool>(result, emailExistente);
        }        

        #endregion

        #region Paciente

        public Result<PacienteModel> GetPaciente(int id)
        {
            var result = new Result<PacienteModel>();
            result.Value = _pacienteRepository.GetById(id);

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
            result.Value = _pacienteRepository.ListPacientes();

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
                model.Pessoa.Ativo = true;
                model.Excluido = false;
                if (model.Id == 0)
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

            var modelBD = _pacienteRepository.GetById(id);
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
            var usuario = _pacienteRepository.GetByCpf(model.Pessoa.Cpf);
                                                        
            if (usuario != null && usuario.Id != model.Id)
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
            result.Value = _funcionarioRepository.GetById(id);

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
            result.Value = _funcionarioRepository.ListFuncionarios();

            return result;
        }

        public Result<List<FuncionarioModel>> ListProfissionais()
        {
            var result = new Result<List<FuncionarioModel>>();
            var list = _funcionarioRepository.ListFuncionarios();

            result.Value = list.Where(x => x.Modalidades.Count > 0).ToList();

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
                model.Modalidades = ConfiguraModalidades(model.IdsModalidades, model.Id);                
                model.Pessoa.Ativo = true;
                model.Excluido = false;
                if (model.Id == 0)
                {
                    model = _funcionarioRepository.Salvar(model);
                    if (model.Id != 0)
                    {
                        AtualizaModalidadesFuncionario(model.Modalidades);
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
                        AtualizaModalidadesFuncionario(model.Modalidades);

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

            var modelBD = _funcionarioRepository.GetById(id);
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
            var usuario = _funcionarioRepository.GetByCpf(model.Pessoa.Cpf);
                                                 
            if (usuario != null && usuario.Id != model.Id)
            {
                result.Message = "CPF vinculado a outro Funcionário. Não é permitido sua utilização";
                result.Status = false;
                cpfExistente = true;
            }

            return new Tuple<Result<bool>, bool>(result, cpfExistente);
        }

        private void AtualizaModalidadesFuncionario(List<ModalidadeFuncionarioModel> listModalidades)
        {
            var idFuncionario = listModalidades.Select(x => x.IdFuncionario).FirstOrDefault();
            var idsModalidades = listModalidades.Select(x => x.IdModalidade).ToArray();

            var listBD = _modalidadeFuncionarioRepository.ListModalidadesFuncionario(idFuncionario);

            if (listBD.Count > 0)
            {
                var idsModalidadesBD = listBD.Select(x => x.IdModalidade).ToArray();

                var listExcluir = listBD.Where(x => !idsModalidades.Contains(x.IdModalidade)).ToList();
                if (listExcluir.Count > 0)
                    _modalidadeFuncionarioRepository.ExcluiLista(listExcluir);                    

                var listInserir = listModalidades.Where(x => !idsModalidadesBD.Contains(x.IdModalidade)).ToList();
                if (listInserir.Count > 0)                
                    _modalidadeFuncionarioRepository.SalvarLista(listInserir);               
            }
            else            
                _modalidadeFuncionarioRepository.SalvarLista(listModalidades);
                    
        }

        private List<ModalidadeFuncionarioModel> ConfiguraModalidades(string[] idsModalidades, int idFuncionario)
        {
            return idsModalidades.Select(x => new ModalidadeFuncionarioModel
            {
                IdModalidade = Int32.Parse(x),
                IdFuncionario = idFuncionario
            }).ToList();
        }

        #endregion
    }
}
