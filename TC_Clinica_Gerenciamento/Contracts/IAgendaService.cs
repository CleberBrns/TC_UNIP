using System;
using System.Collections.Generic;
using TCC_Unip.Models.Local;
using TCC_Unip.Models.Servico;

namespace TCC_Unip.Contracts
{
    interface IAgendaService
    {
        ResultService<Agenda> Get(string id);
        ResultService<List<Agenda>> ListAgendasPeriodo(DateTime dateFrom, DateTime dateTo);
        ResultService<Paciente> ConsultasPeriodoPaciente(string cpf, DateTime dateFrom, DateTime dateTo);
        ResultService<Funcionario> ConsultasPeriodoFuncionario(string cpf, DateTime dateFrom, DateTime dateTo);
        ResultService<bool> Save(Agenda model);
        ResultService<bool> Delete(string id);

    }
}
