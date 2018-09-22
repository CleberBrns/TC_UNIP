using System;
using System.Collections.Generic;
using TCC_Unip.Models.Local;
using TCC_Unip.Models.Servico;
using TCC_Unip.ServiceAPI;

namespace TCC_Unip.Services
{
    public class AgendaService : Contracts.IServiceAgenda
    {
        AgendaServiceAPI service = new AgendaServiceAPI();

        public ResultService<Agenda> Get(string id)
        {
            var result = new ResultService<Agenda>();

            var retorno = service.Get(id);
            result.value = retorno;

            if (string.IsNullOrEmpty(result.value.Modalidade))
                result.errorMessage = "A Consulta não existe mais na base de dados do serviço!";

            return result;
        }

        public ResultService<List<Agenda>> ListAgendasPeriodo(DateTime dateFrom, DateTime dateTo)
        {
            var result = new ResultService<List<Agenda>>();

            var retorno = service.ListAgendasPeriodo(dateFrom.Date.ToString(), dateTo.Date.ToString());
            result.value = retorno;

            return result;
        }

        public ResultService<Funcionario> ConsultasPeriodoFuncionario(string cpf, DateTime dateFrom, DateTime dateTo)
        {
            var result = new ResultService<Funcionario>();

            var retorno = service.ConsultasPeriodoFuncionario(cpf, dateFrom.ToShortDateString(), dateTo.ToShortDateString());
            result.value = retorno;

            if (string.IsNullOrEmpty(retorno.Cpf))
                result.errorMessage = "Sem agenda!";

            return result;
        }

        public ResultService<Paciente> ConsultasPeriodoPaciente(string cpf, DateTime dateFrom, DateTime dateTo)
        {
            var result = new ResultService<Paciente>();

            var retorno = service.ConsultasPeriodoPaciente(cpf, dateFrom.Date.ToString(), dateTo.Date.ToString());
            result.value = retorno;

            return result;
        }

        public ResultService<bool> Save(Agenda model)
        {
            var result = new ResultService<bool>();

            if (model.Id == 0)
            {
                model.Data = model.CombinaDataHora(model.Data, model.Horario);
                model.DateTimeService = model.ToMilliseconds(model.Data);                

                var retorno = service.Save(model);
                result.value = retorno;

                if (result.value)
                    result.message = "Consulta salva com sucesso!";
                else
                    result.errorMessage = "Falha ao salvar a Consulta!";
            }
            else
            {
                var retorno = service.Update(model);
                result.value = retorno;

                if (result.value)
                    result.message = "Consulta atualizada com sucesso!";
                else
                    result.errorMessage = "Falha ao atualizar a Consulta!";
            }

            return result;
        }

        public ResultService<bool> Delete(string id)
        {
            var result = new ResultService<bool>();

            var retorno = service.Delete(id);
            result.value = retorno;

            if (result.value)
                result.message = "Consulta excluída com sucesso!";
            else
                result.errorMessage = "Falha ao excluir a Consulta!";

            return result;
        }
    }
}