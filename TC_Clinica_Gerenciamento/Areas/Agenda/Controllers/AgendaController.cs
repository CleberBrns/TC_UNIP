using System;
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

        public JsonResult GetCalendarioAgendaMesAtual(bool getFromSession)
        {
            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var agendaCalendario = GetAgendaMesAtualCalendario(getFromSession);

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

        public ActionResult ModalCadastrar()
        {
            ViewBag.Usuario = session.GetModelFromSession(sessionName).Item1;

            ViewBag.ListPacientes = GetListPacientes();            
            ViewBag.ListProfissionais = GetListProfissionais();            
            ViewBag.ListHorarios = GetListHorarios();
           
            return PartialView("_Gerenciar");
        }
        
        public ActionResult ModalVisualizar(EventoCalendario evento)
        {
            ViewBag.Usuario = session.GetModelFromSession(sessionName).Item1;
            return PartialView("_Visualizar", evento);
        }

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

        #region Métods Privados

        private List<EventoCalendario> GetAgendaMesAtualCalendario(bool getFromSession)
        {
            var incioMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var fimMes = incioMes.AddMonths(1).AddDays(-1);

            var listAgendaDoMes = _agendaService.ListAgendaPeriodo(incioMes.ToShortDateString(), 
                                                                   fimMes.ToShortDateString(),
                                                                   getFromSession).value;

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