using System;
using System.Collections.Generic;
using TcUnip.Model.Calendario;
using TcUnip.Model.Common;
using TcUnip.Model.Pessoa;

namespace TcUnip.Service.Contract.Calendario
{
    public interface IAgendaService
    {
        Result<Agenda> Get(string id);
        Result<List<Agenda>> ListAgendaPeriodo(string dateFrom, string dateTo, bool getFromSession);
        Result<List<Agenda>> ListAgendaDoDia(bool getFromSession);
        Result<Paciente> ConsultasPeriodoPaciente(string cpf, string dateFrom, string dateTo);
        Result<Funcionario> ConsultasPeriodoFuncionario(string cpf, string dateFrom, string dateTo);
        Result<bool> Save(Agenda model);
        Result<bool> Delete(string id);
    }
}
