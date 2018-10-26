using System;
using System.Collections.Generic;
using TcUnip.Model.Calendario;
using TcUnip.Model.Common;
using TcUnip.Model.Pessoa;

namespace TcUnip.Service.Contract.Calendario
{
    public interface IAgendaService
    {
        Result<Agenda> GetAgenda(string id);
        Result<List<Agenda>> ListAgendaPeriodo(string dateFrom, string dateTo);
        Result<List<Agenda>> ListAgendaDoDia();
        Result<Paciente> ConsultasPeriodoPaciente(string cpf, string dateFrom, string dateTo);
        Result<Funcionario> ConsultasPeriodoFuncionario(string cpf, string dateFrom, string dateTo);
        Result<bool> SalvaAgenda(Agenda model);
        Result<bool> ExcluiAgenda(string id);
    }
}
