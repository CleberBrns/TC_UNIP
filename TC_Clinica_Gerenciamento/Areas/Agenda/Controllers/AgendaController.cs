using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TCC_Unip.Models.Local;
using TCC_Unip.Models.Servico;
using TCC_Unip.Services;
using TCC_Unip.Session;
using TCC_Unip.Util;

namespace TCC_Unip.Areas.Agenda.Controllers
{
    public class AgendaController : Controller
    {
        readonly Constants.Constants constants = new Constants.Constants();
        readonly Mensagens mensagens = new Mensagens();
        readonly AgendaService _agendaService = new AgendaService();
        readonly PacienteService _pacienteService = new PacienteService();
        readonly FuncionarioService _funcionarioService = new FuncionarioService();

        readonly UsuarioSession session = new UsuarioSession();
        readonly string sessionName = Constants.ConstSessions.usuario;

        public ActionResult Listagem(bool getFromSession)
        {
            if (!session.GetModelFromSession(sessionName).Item2)
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
                    listModalidades = listModalidades.Where(l => modalidadesProf.Contains(l.Value)).ToList();
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

        public ActionResult VerificaHorariosDataProfissional(string cpf, string data)
        {
            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var dataBusca = Convert.ToDateTime(data);
                var consultasProfissiona = _agendaService.ConsultasPeriodoFuncionario(cpf, dataBusca, dataBusca);

                var listHorariosDisponiveis = GetListHorarios();
                 
                if (consultasProfissiona.status)                
                    if (consultasProfissiona.value.Consultas.Count > 0)
                    {
                        
                    }                

                return Json(listHorariosDisponiveis, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                msgExibicao = Constants.Constants.msgFalhaAoListarHorarios;
                msgAnalise = ex.ToString();
            }

            var mensagensRetorno = mensagens.ConfiguraMensagemRetorno(msgExibicao, msgAnalise);
            return Json(new { mensagensRetorno }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Salvar(Models.Servico.Agenda model)
        {
            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var resultService = _agendaService.Save(model);

                msgExibicao = resultService.message;
                msgAnalise = resultService.errorMessage;
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
            return _pacienteService.List(true).value;
        }

        private List<Models.Servico.Funcionario> GetListFuncionarios()
        {
            return _funcionarioService.List(true).value;
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