using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TCC_Unip.Models.Local;
using TCC_Unip.Models.Servico;
using TCC_Unip.Services;
using TCC_Unip.Util;

namespace TCC_Unip.Areas.Agenda.Controllers
{
    public class AgendaController : Controller
    {
        Constants.Constants constants = new Constants.Constants();
        Mensagens mensagens = new Mensagens();
        AgendaService _agendaService = new AgendaService();
        PacienteService _pacienteService = new PacienteService();
        FuncionarioService _funcionarioService = new FuncionarioService();

        public ActionResult Listagem(bool getFromSession)
        {
            if ((Models.Servico.Usuario)Session[Constants.ConstSessions.usuario] == null)
                return RedirectToAction("Login", "Login", new { area = "" });

            return PartialView("_Listagem");
        }

        public ActionResult ModalCadastrar()
        {
            ViewBag.ListPacientes = GetListPacientes();            
            ViewBag.ListProfissionais = GetListFuncionarios();
            ViewBag.ListModalidades = GetListModalidades();
            ViewBag.ListHorarios = GetListHorarios();
           
            return PartialView("_Gerenciar");
        }

        private List<Models.Servico.Paciente> GetListPacientes()
        {
            return _pacienteService.List().value;
        }

        private List<Models.Servico.Funcionario> GetListFuncionarios()
        {
            return _funcionarioService.List().value;
        }

        private List<DataSelectControl> GetListModalidades()
        {            
            return constants.ListModalidades();
        }

        private List<DataSelectControl> GetListHorarios()
        {
            return constants.ListHorariosConsultas();
        }       
    }
}