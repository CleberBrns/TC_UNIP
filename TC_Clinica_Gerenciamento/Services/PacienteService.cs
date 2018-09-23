using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using TCC_Unip.Models.Local;
using TCC_Unip.Models.Servico;
using TCC_Unip.API;
using TCC_Unip.Contracts.Service;

namespace TCC_Unip.Services
{
    public class PacienteService : IServicePaciente
    {
        PacienteAPI service = new PacienteAPI();

        public ResultService<Paciente> Get(string cpf)
        {
            var result = new ResultService<Paciente>();

            var retorno = service.Get(cpf);
            result.value = retorno;

            if (string.IsNullOrEmpty(result.value.Cpf))
                result.message = "O paciente não existe mais na base de dados do serviço!";

            return result;
        }

        public ResultService<List<Paciente>> List()
        {
            var result = new ResultService<List<Paciente>>();

            var retorno = service.List();
            result.value = retorno;

            return result;
        }

        public ResultService<bool> Save(Paciente model)
        {
            string msg = string.Empty;
            string msgErro = string.Empty;

            var result = new ResultService<bool>();

            var registroExistente = service.Get(model.Cpf);

            if (string.IsNullOrEmpty(registroExistente.Nome))
            {
                var retorno = service.Save(model);
                result.value = retorno;

                if (result.value)
                    msg = "Paciente salvo com sucesso!";
                else
                    msg = "Falha ao salvar o paciente!";
            }
            else
            {
                var retorno = service.Update(model);
                result.value = retorno;

                if (result.value)
                    msg = "Paciente atualizado com sucesso!";
                else
                    msg = "Falha ao atualizar o paciente!";
            }

            result.message = msg;
            result.errorMessage = msgErro;

            return result;
        }
        

        public ResultService<bool> Delete(string cpf)
        {
            var result = new ResultService<bool>();

            var retorno = service.Delete(cpf);
            result.value = retorno;

            if (result.value)
                result.message = "Paciente excluído com sucesso!";
            else
                result.message = "Falha ao excluir o paciente!";

            return result;
        }

    }
}