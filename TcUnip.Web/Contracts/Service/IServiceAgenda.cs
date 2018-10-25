using System;
using System.Collections.Generic;
using TcUnip.Web.Models.Local;
using TcUnip.Web.Models.Servico;

namespace TcUnip.Web.Contracts.Service
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
