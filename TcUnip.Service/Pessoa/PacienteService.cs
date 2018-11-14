using System;
using System.Collections.Generic;
using TcUnip.Service.Contract.Pessoa;
//using TcUnip.ServiceApi.Pessoa;
using TcUnip.Model.Common;
using TcUnip.Model.Pessoa;

namespace TcUnip.Service.Pessoa
{
    public class PacienteService// : IPacienteService
    {
        //readonly PacienteApi serviceApi = new PacienteApi();
        //readonly FuncionarioApi serviceFuncApi = new FuncionarioApi();
        //ReplacesService replacesService = new ReplacesService();

        //public Result<Paciente> Get(string cpf)
        //{
        //    cpf = replacesService.ReplaceCpfEmailWebToApi(cpf, false);

        //    var result = new Result<Paciente>();
        //    result.Value = serviceApi.Get(cpf);

        //    if (string.IsNullOrEmpty(result.Value.Cpf))
        //    {
        //        result.Message = "O paciente não existe mais na base de dados do serviço!";
        //        result.Status = false;
        //    }                

        //    return result;
        //}

        //public Result<List<Paciente>> List()
        //{
        //    var result = new Result<List<Paciente>>();
        //    result.Value = GetFromService();

        //    return result;
        //}

        //public Result<bool> Salva(Paciente model)
        //{
        //    string msg = string.Empty;
        //    string msgErro = string.Empty;

        //    var result = new Result<bool>();

        //    var cpfExisteEmOutrosCadastros = ValidaExistenciaCPF(model);

        //    if (cpfExisteEmOutrosCadastros.Item2)
        //    {
        //        result = cpfExisteEmOutrosCadastros.Item1;
        //    }
        //    else
        //    {
        //        var registroExistente = !string.IsNullOrEmpty(model.Id) ? serviceApi.Get(model.Id) : new Paciente();

        //        if (string.IsNullOrEmpty(registroExistente.Nome))
        //        {
        //            var retorno = serviceApi.Save(model);
        //            result.Value = retorno;

        //            if (result.Value)
        //                msg = "Paciente salvo com sucesso!";
        //            else
        //            {
        //                msg = "Falha ao salvar o paciente!";
        //                result.Status = false;
        //            }
        //        }
        //        else
        //        {
        //            var retorno = serviceApi.Update(model, model.Cpf);
        //            result.Value = retorno;

        //            if (result.Value)
        //                msg = "Paciente atualizado com sucesso!";
        //            else
        //            {
        //                msg = "Falha ao atualizar o paciente!";
        //                result.Status = false;
        //            }                        
        //        }

        //        result.Message = msg;
        //    }          

        //    return result;
        //}

        //public Result<bool> Exclui(string cpf)
        //{
        //    cpf = replacesService.ReplaceCpfEmailWebToApi(cpf, false);
        //    var result = new Result<bool>();

        //    var retorno = serviceApi.Delete(cpf);
        //    result.Value = retorno;

        //    if (result.Value)
        //        result.Message = "Paciente excluído com sucesso!";
        //    else
        //    {
        //        result.Message = "Falha ao excluir o paciente!";
        //        result.Status = false;
        //    }

        //    return result;
        //}

        //#region Métodos Privados

        //private Tuple<Result<bool>, bool> ValidaExistenciaCPF(Paciente model)
        //{
        //    var result = new Result<bool>();

        //    var cpfExistente = false;
        //    var funcionario = serviceFuncApi.Get(model.Cpf);
        //    var paciente = serviceApi.Get(model.Cpf);

        //    if (!string.IsNullOrEmpty(funcionario.Cpf))
        //    {
        //        result.Message = "Cpf vinculado a um Funcionário. Não é permitido sua utilização";
        //        result.Status = false;
        //        cpfExistente = true;
        //    }
        //    else if (!string.IsNullOrEmpty(paciente.Cpf))
        //    {
        //        if (model.Nome != paciente.Nome)
        //        {
        //            result.Message = "Cpf vinculado a outro Paciente. Não é permitido sua utilização";
        //            result.Status = false;
        //            cpfExistente = true;
        //        }
        //    }

        //    return new Tuple<Result<bool>, bool>(result, cpfExistente);
        //}


        //private List<Paciente> GetFromService()
        //{
        //    var list = serviceApi.List();
        //    return list;
        //}

        //#endregion
    }
}