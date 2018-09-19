using System;
using System.Collections.Generic;
using TCC_Unip.Models.Local;
using TCC_Unip.Models.Servico;

namespace TCC_Unip.Contracts
{
    interface IAgendaService
    {        
        ResultService<List<Agenda>> List();
        ResultService<bool> Save(Agenda model);
        ResultService<bool> Delete(int id);
        ResultService<List<Agenda>> GetAgenda(DateTime inicio, DateTime fim);
        ResultService<Paciente> GetAgendaPaciente(DateTime inicio, DateTime fim);
        ResultService<Funcionario> GetAgendaFuncionario(DateTime inicio, DateTime fim);

    }
}
