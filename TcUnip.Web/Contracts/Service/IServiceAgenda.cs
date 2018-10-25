using System;
using System.Collections.Generic;
using TCC_Unip.Models.Local;
using TCC_Unip.Models.Servico;

namespace TCC_Unip.Contracts.Service
{
    public interface IServiceAgenda
    {
        ResultService<Agenda> Get(string id);
        ResultService<List<Agenda>> ListAgendaPeriodo(string dateFrom, string dateTo, bool getFromSession);
        ResultService<List<Agenda>> ListAgendaDoDia(bool getFromSession);
        ResultService<Paciente> ConsultasPeriodoPaciente(string cpf, DateTime dateFrom, DateTime dateTo);
        ResultService<Funcionario> ConsultasPeriodoFuncionario(string cpf, DateTime dateFrom, DateTime dateTo);
        ResultService<bool> Save(Agenda model);
        ResultService<bool> Delete(string id);

    }
}
