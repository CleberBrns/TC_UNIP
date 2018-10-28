using System;
using System.Collections.Generic;
using TcUnip.Model.Common;
using TcUnip.Model.Pessoa;
using TcUnip.Service.Contract.Pessoa;
using TcUnip.ServiceApi.Pessoa;

namespace TcUnip.Service.Pessoa
{
    public class UsuarioService : IUsuarioService
    {
        readonly UsuarioApi serviceApi = new UsuarioApi();

        public Result<Usuario> Get(string email)
        {
            var result = new Result<Usuario>();
            result.Value = serviceApi.Get(email);

            if (string.IsNullOrEmpty(result.Value.Email))
            {
                result.Message = "O Usuário não existe mais na base de dados do serviço!";
                result.Status = false;
            }
                
            return result;
        }

        public Result<List<Usuario>> List()
        {
            var result = new Result<List<Usuario>>();
            result.Value = serviceApi.List();

            return result;
        }

        public Result<bool> Salva(Usuario model)
        {
            var result = new Result<bool>();

            var emailExisteEmOutroUsuario = ValidaExistenciaEmail(model);

            if (emailExisteEmOutroUsuario.Item2)
            {
                result = emailExisteEmOutroUsuario.Item1;
            }
            else
            {
                var registroExistente = serviceApi.Get(model.Email);
                if (string.IsNullOrEmpty(registroExistente.Email))
                {
                    var retorno = serviceApi.Save(model);
                    result.Value = retorno;

                    if (result.Value)
                        result.Message = "Usuário salvo com sucesso!";
                    else
                    {
                        result.Message = "Falha ao salvar o Usuário!";
                        result.Status = false;
                    }
                }
                else
                {
                    var retorno = serviceApi.Update(model, model.Email);
                    result.Value = retorno;

                    if (result.Value)
                        result.Message = "Usuário atualizado com sucesso!";
                    else
                    {
                        result.Message = "Falha ao atualizar o Usuário!";
                        result.Status = false;
                    }
                }
            }

            return result;
        }

        public Result<bool> Excliu(string cpf)
        {
            var result = new Result<bool>();

            var retorno = serviceApi.Delete(cpf);
            result.Value = retorno;

            if (result.Value)
                result.Message = "Usuário excluído com sucesso!";
            else
            {
                result.Message = "Falha ao excluir o Usuário!";
                result.Status = false;
            }
                
            return result;
        }

        public Result<Usuario> Autentica(Usuario model)
        {
            var result = new Result<Usuario>();

            var retorno = serviceApi.Auth(model);

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

        #region Métodos Privados

        private Tuple<Result<bool>, bool> ValidaExistenciaEmail(Usuario model)
        {
            var result = new Result<bool>();

            var emailExistente = false;            
            var usuario = serviceApi.Get(model.Email);

            if (!string.IsNullOrEmpty(model.Email))
            {
                if (model.Cpf != model.Cpf)
                {
                    result.Message = "E-mail vinculado a outro Usuário. Não é permitido sua utilização";
                    result.Status = false;
                    emailExistente = true;
                }
            }

            return new Tuple<Result<bool>, bool>(result, emailExistente);
        }

        #endregion
    }
}