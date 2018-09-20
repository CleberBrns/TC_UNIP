using System;
using System.Collections.Generic;
using System.Linq;
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
            ViewBag.ListHorarios = GetListHorarios();
           
            return PartialView("_Gerenciar");
        }

        public ActionResult GetModalidadesProfissional(string cpf)
        {
            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var resultService = new ResultService<Models.Servico.Funcionario>();

                resultService = _funcionarioService.Get(cpf);
                if (!resultService.status)
                    resultService.errorMessage = "Erro!";

                msgExibicao = resultService.message;
                msgAnalise = resultService.errorMessage;

                var listModalidades = GetListModalidades();
                if (resultService.status)
                {
                    var modalidadesProf = resultService.value.Modalidades;
                    listModalidades.Where(l => modalidadesProf.Contains(l.Value)).ToList();
                }

                return Json(listModalidades, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                msgExibicao = Constants.Constants.msgFalhaAoListar;
                msgAnalise = ex.ToString();
            }

            var mensagensRetorno = mensagens.ConfiguraMensagemRetorno(msgExibicao, msgAnalise);
            return Json(new { mensagensRetorno }, JsonRequestBehavior.AllowGet);
        }

        #region Métods Privados   

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

        #endregion
    }
}