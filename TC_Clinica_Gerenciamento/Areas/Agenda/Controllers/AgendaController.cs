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
        Mensagens mensagens = new Mensagens();
        AgendaService _agendaService = new AgendaService();
        UsuarioService _serviceUsuario = new UsuarioService();
        FuncionarioService _serviceFuncionario = new FuncionarioService();

        public ActionResult Listagem()
        {
            if ((Models.Servico.Usuario)Session[Constants.ConstSessions.usuario] == null)
                return RedirectToAction("Login", "Login", new { area = "" });

            return View();
        }

        public ActionResult ModalCadastrar()
        {
            ViewBag.ListPacientes = GetListPacientes();            
            ViewBag.ListProfissionais = GetListFuncionarios();
            ViewBag.ListModalidades = GetListModalidades();
            ViewBag.ListHorarios = GetListHorarios();

            var model = new Models.Servico.Agenda();
            var defaultObj = model.GetModelDefault();
            return PartialView("_Gerenciar", defaultObj);
        }
        private List<Models.Servico.Paciente> GetListPacientes()
        {
            throw new NotImplementedException();
        }
        private List<Models.Servico.Funcionario> GetListFuncionarios()
        {
            throw new NotImplementedException();
        }
        private List<DataSelectControl> GetListModalidades()
        {
            var constants = new Constants.Constants();
            return constants.ListModalidades();
        }

        private List<DataSelectControl> GetListHorarios()
        {
            return new List<DataSelectControl>();
        }       
    }
}