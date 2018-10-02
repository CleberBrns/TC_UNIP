﻿using System;
using System.Collections.Generic;
using System.Drawing;
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

        #region Grid

        public ActionResult Listagem(bool getFromSession)
        {
            if (!session.GetModelFromSession(sessionName).Item2)
                return RedirectToAction("Login", "Login", new { area = "" });

            ViewBag.Usuario = session.GetModelFromSession(sessionName).Item1;

            return PartialView("_Index");
        }

        public ActionResult ListaAgendaDoDia(bool getFromSession)
        {
            ViewBag.Usuario = session.GetModelFromSession(sessionName).Item1;

            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var resultService = _agendaService.ListAgendaDoDia(getFromSession);

                msgExibicao = resultService.message;
                msgAnalise = resultService.errorMessage;

                return PartialView("_GridConsultas", resultService.value);
            }
            catch (Exception ex)
            {
                msgExibicao = Constants.Constants.msgFalhaAoListar;
                msgAnalise = ex.ToString();
            }

            var mensagensRetorno = mensagens.ConfiguraMensagemRetorno(msgExibicao, msgAnalise);
            return Json(new { mensagensRetorno }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListaConsultasPorDatas(string dataInicio, string dataFim)
        {
            ViewBag.Usuario = session.GetModelFromSession(sessionName).Item1;

            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var resultService = _agendaService.ListAgendaPeriodo(dataInicio, dataFim, false);

                msgExibicao = resultService.message;
                msgAnalise = resultService.errorMessage;

                return PartialView("_GridConsultas", resultService.value);
            }
            catch (Exception ex)
            {
                msgExibicao = Constants.Constants.msgFalhaAoListar;
                msgAnalise = ex.ToString();
            }

            var mensagensRetorno = mensagens.ConfiguraMensagemRetorno(msgExibicao, msgAnalise);
            return Json(new { mensagensRetorno }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Calendário

        public JsonResult GetAgendaCalendarioDatas(string dataInicio, string dataFim)
        {
            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var agendaCalendario = GetAgendaCalendarioPorDatas(dataInicio, dataFim, false);
                return Json(new { agendaCalendario }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                msgExibicao = Constants.Constants.msgFalhaAoListar;
                msgAnalise = ex.ToString();
            }

            var mensagensRetorno = mensagens.ConfiguraMensagemRetorno(msgExibicao, msgAnalise);
            return Json(new { mensagensRetorno }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAgendaCalendarioMesAtual(bool getFromSession)
        {
            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var inicioMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var fimMes = inicioMes.AddMonths(1).AddDays(-1);

                var agendaCalendario = GetAgendaCalendarioPorDatas(inicioMes.ToShortDateString(), 
                                                                   fimMes.ToShortDateString(), 
                                                                   getFromSession);

                return Json(new { agendaCalendario }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                msgExibicao = Constants.Constants.msgFalhaAoListar;
                msgAnalise = ex.ToString();
            }

            var mensagensRetorno = mensagens.ConfiguraMensagemRetorno(msgExibicao, msgAnalise);
            return Json(new { mensagensRetorno }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Modal Gerenciar

        public ActionResult GetModalidadesProfissional(string cpf)
        {
            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var resultService = _funcionarioService.Get(cpf);
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
                //TO DO - Remover os horários das consultas existentes do Profissional
                //var dataBusca = Convert.ToDateTime(data);
                //var consultasProfissiona = _agendaService.ConsultasPeriodoFuncionario(cpf, dataBusca, dataBusca);

                //var listHorariosDisponiveis = GetListHorarios();

                //if (consultasProfissiona.status)                
                //    if (consultasProfissiona.value.Consultas.Count > 0)
                //    {

                //    }


                var listHorariosDisponiveis = GetListHorarios();

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

        #endregion

        public ActionResult ModalCadastrar()
        {
            ViewBag.Usuario = session.GetModelFromSession(sessionName).Item1;

            ViewBag.ListPacientes = GetListPacientes();            
            ViewBag.ListProfissionais = GetListProfissionais();            
            ViewBag.ListHorarios = GetListHorarios();

            var model = new Models.Servico.Agenda();
           
            return PartialView("_Gerenciar", model.GetModelDefault());
        }
        
        public ActionResult ModalEditar(string id)
        {
            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            ViewBag.Usuario = session.GetModelFromSession(sessionName).Item1;         

            try
            {
                var resultService = _agendaService.Get(id);

                if (resultService.status)
                {
                    var model = resultService.value;
                    ViewBag.ListPacientes = GetListPacientes();
                    ViewBag.ListProfissionais = GetListProfissionais();
                    ViewBag.ListHorarios = GetListHorarios();
                    ViewBag.ListModalidades = GetListModalidadesProfissional(model.Funcionario.Cpf);

                    model.Data = model.FromMilliseconds(model.DateTimeService);
                    model.Horario = model.Data.ToShortTimeString();

                    return PartialView("_Gerenciar", model);
                }
                else
                {
                    msgExibicao = resultService.message;
                    msgAnalise = "Erro!";
                }

            }
            catch (Exception ex)
            {
                msgExibicao = Constants.Constants.msgFalhaAoCarregar;
                msgAnalise = ex.Message;
            }

            var mensagensRetorno = mensagens.ConfiguraMensagemRetorno(msgExibicao, msgAnalise);
            return Json(new { mensagensRetorno }, JsonRequestBehavior.AllowGet);
        }

        private List<DataSelectControl> GetListModalidadesProfissional(string cpf)
        {
            var resultService = _funcionarioService.Get(cpf);

            var listModalidades = new List<DataSelectControl>();
            if (resultService.status)
            {
                listModalidades = GetListModalidades();

                var modalidadesProf = resultService.value.Modalidades;
                listModalidades = listModalidades.Where(l => modalidadesProf.Contains(l.Value)).ToList();
            }

            return listModalidades;
        }

        public ActionResult ModalVisualizar(EventoCalendario evento)
        {
            ViewBag.Usuario = session.GetModelFromSession(sessionName).Item1;
            return PartialView("_Visualizar", evento);
        }

        [HttpPost]
        public ActionResult Excluir(string id)
        {
            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var resultService = _agendaService.Delete(id);

                msgExibicao = resultService.message;
                msgAnalise = resultService.value ? resultService.errorMessage : "Falha";
            }
            catch (Exception ex)
            {
                msgExibicao = Constants.Constants.msgFalhaAoExcluir;
                msgAnalise = ex.Message;
            }

            var mensagensRetorno = mensagens.ConfiguraMensagemRetorno(msgExibicao, msgAnalise);
            return Json(new { mensagensRetorno }, JsonRequestBehavior.AllowGet);
        }

        #region Métods Privados

        private List<EventoCalendario> GetAgendaCalendarioPorDatas(string dataIncio, string dataFim, bool getFromSession)
        {
            var listAgendaDoMes = _agendaService.ListAgendaPeriodo(dataIncio,dataFim,getFromSession).value;

            var list = listAgendaDoMes.Select(l =>
                                       new EventoCalendario
                                       {
                                           IdConsulta = l.Id,
                                           Titulo = l.Modalidade,
                                           Descricao = GetDescricaoEvento(l),
                                           ComecaEm = l.FromMilliseconds(l.DateTimeService),
                                           TerminaEm = l.FromMilliseconds(l.DateTimeService).AddHours(1),
                                           CorEvento = GetCorEvento(l.FromMilliseconds(l.DateTimeService))
                                       }).ToList();

            return list;
        }

        private string GetTituloEvento(Models.Servico.Agenda agenda)
        {
            var paciente = agenda.Paciente.Nome.Substring(0, Math.Min(8, agenda.Paciente.Nome.Length));
            var profissional = agenda.Funcionario.Nome.Substring(0, Math.Min(8, agenda.Funcionario.Nome.Length));
            var titulo = agenda.Modalidade + ", " + paciente + ", " + profissional;

            return titulo;
        }

        private string GetDescricaoEvento(Models.Servico.Agenda agenda)
        {
            return "Sessão de " + agenda.Modalidade + ", " +
                   " agendada para o Paciente " + agenda.Paciente.Nome +
                   " com o Profissional " + agenda.Funcionario.Nome;                 
        }

        private string GetCorEvento(DateTime dataEvento)
        {
            var cor = Color.Green.ToString();

            var dataAtual = DateTime.Now;
            if (dataEvento.Date == dataAtual.Date)            
                cor = Color.Yellow.ToString();            
            else if (dataEvento.Date < dataAtual)            
                cor = Color.Red.ToString();

            return cor;            
        }

        private List<Models.Servico.Paciente> GetListPacientes()
        {
            return _pacienteService.List(true).value;
        }

        private List<Models.Servico.Funcionario> GetListProfissionais()
        {
            return _funcionarioService.ListProfissionais(true).value;
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