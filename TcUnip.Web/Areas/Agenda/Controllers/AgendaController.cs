using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;
using TcUnip.Model.Common;
using TcUnip.Web.Controllers;
using TcUnip.Web.Models.Local;
using TcUnip.Web.Models.Proxy.Contract;
using TcUnip.Web.Util;

namespace TcUnip.Web.Areas.Agenda.Controllers
{
    public class AgendaController : BaseController
    {
        readonly IAgendaProxy _agendaProxy;
        readonly IPacienteProxy _pacienteProxy;
        readonly IFuncionarioProxy _funcionarioProxy;

        readonly Constants.Constants constants = new Constants.Constants();
        readonly Mensagens mensagens = new Mensagens();
        readonly ReplacesService replacesService = new ReplacesService();

        public AgendaController(IAgendaProxy agendaProxy, IPacienteProxy pacienteProxy, IFuncionarioProxy funcionarioProxy)
        {
            this._agendaProxy = agendaProxy;
            this._pacienteProxy = pacienteProxy;
            this._funcionarioProxy = funcionarioProxy;
        }

        #region Grid        

        public ActionResult Listagem()
        {
            var userInfo = GetUsuarioSession();
            if (!userInfo.Item2)
                return RedirectToAction("Login", "Login", new { area = "" });
          
            ViewBag.Usuario = userInfo.Item1;

            return PartialView("_Index");
        }

        public ActionResult ListaAgendaDoDia()
        {
            ViewBag.Usuario = GetUsuarioSession().Item1;

            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var resultService = _agendaProxy.ListAgendaDoDia();

                var list = FiltraListaPorPerfil(resultService.Value);

                msgExibicao = resultService.Message;
                msgAnalise = !resultService.Status ? "Falha!" : string.Empty;

                return PartialView("_GridConsultas", list);
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
            ViewBag.Usuario = GetUsuarioSession().Item1;

            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var resultService = _agendaProxy.ListAgendaPeriodo(dataInicio, dataFim);

                var list = FiltraListaPorPerfil(resultService.Value);

                msgExibicao = resultService.Message;
                msgAnalise = !resultService.Status ? "Falha!" : string.Empty;

                return PartialView("_GridConsultas", list);
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

        public JsonResult GetAgendaCalendarioPorMes(string dataSelecionada)
        {
            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var data = Convert.ToDateTime(dataSelecionada).Date;

                var inicioMes = new DateTime(data.Year, data.Month, 1);
                var dataFim = inicioMes.AddMonths(1).AddDays(-1).ToShortDateString();

                var agendaCalendario = GetAgendaCalendarioPorDatas(inicioMes.ToShortDateString(), dataFim);
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

        public JsonResult GetAgendaCalendarioMesAtual()
        {
            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var inicioMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var fimMes = inicioMes.AddMonths(1).AddDays(-1);

                var agendaCalendario = GetAgendaCalendarioPorDatas(inicioMes.ToShortDateString(), 
                                                                   fimMes.ToShortDateString());

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
                var resultService = _funcionarioProxy.Get(cpf);
                if (!resultService.Status)
                    msgAnalise = "Erro!";

                msgExibicao = resultService.Message;                

                var listModalidades = GetListModalidades();
                if (resultService.Status)
                {
                    var modalidadesProf = resultService.Value.Modalidades;
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

        public ActionResult Salvar(Model.Calendario.Agenda model)
        {
            ValidaAutorizaoAcessoUsuario(Constants.ConstPermissoes.gerenciamento);

            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var resultService = _agendaProxy.Salva(model);

                msgExibicao = resultService.Message;
                msgAnalise = !resultService.Status ? "Falha!" : string.Empty;
            }
            catch (Exception ex)
            {
                var msgsRetornos = ErrosService.GetMensagemService(ex, HttpContext.Response);
                msgExibicao = msgsRetornos.Item1;
                msgAnalise = !string.IsNullOrEmpty(msgsRetornos.Item2) ? msgsRetornos.Item2 : Constants.Constants.msgFalhaAoSalvar;
            }

            var mensagensRetorno = mensagens.ConfiguraMensagemRetorno(msgExibicao, msgAnalise);
            return Json(new { mensagensRetorno }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult ModalCadastrar(string data = null)
        {
            ValidaAutorizaoAcessoUsuario(Constants.ConstPermissoes.gerenciamento);

            ViewBag.Usuario = GetUsuarioSession().Item1;
            ViewBag.ListPacientes = GetListPacientes();            
            ViewBag.ListProfissionais = GetListProfissionais();            
            ViewBag.ListHorarios = GetListHorarios();

            var model = new Model.Calendario.Agenda();
            model = model.GetModelDefault();

            if (!string.IsNullOrEmpty(data))
                model.Data = Convert.ToDateTime(data);
            else
                model.Data = ConfiguraDataExibir();
           
            return PartialView("_Gerenciar", model);
        }

        public ActionResult ModalEditar(string id)
        {
            ValidaAutorizaoAcessoUsuario(Constants.ConstPermissoes.gerenciamento);

            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            ViewBag.Usuario = GetUsuarioSession().Item1;         

            try
            {
                var resultService = _agendaProxy.Get(id);

                if (resultService.Status)
                {
                    var model = resultService.Value;
                    ViewBag.ListPacientes = GetListPacientes();
                    ViewBag.ListProfissionais = GetListProfissionais();
                    ViewBag.ListHorarios = GetListHorarios();
                    ViewBag.ListModalidades = GetListModalidadesProfissional(model.Funcionario.Cpf);

                    model.Data = replacesService.FromMilliseconds(model.DateTimeService);
                    model.Horario = model.Data.ToShortTimeString();

                    return PartialView("_Gerenciar", model);
                }
                else
                {
                    msgExibicao = resultService.Message;
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

        public ActionResult ModalVisualizar(EventoCalendario evento)
        {
            evento.DiaDaSemana = GetDiaDaSemana(evento.ComecaEm);
            ViewBag.Usuario = GetUsuarioSession().Item1;
            return PartialView("_Visualizar", evento);
        }

        private string GetDiaDaSemana(DateTime comecaEm)
        {
            var culture = new System.Globalization.CultureInfo("pt-BR");
            return culture.TextInfo.ToTitleCase(culture.DateTimeFormat.GetDayName(comecaEm.DayOfWeek));           
        }

        [HttpPost]
        public ActionResult Excluir(string id)
        {
            ValidaAutorizaoAcessoUsuario(Constants.ConstPermissoes.gerenciamento);

            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var resultService = _agendaProxy.Exclui(id);

                msgExibicao = resultService.Message;
                msgAnalise = !resultService.Status ? "Falha" : string.Empty;
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

        /// <summary>
        /// O acesso do Profissional lista apenas as consultas marcadas com o mesmo
        /// </summary>
        /// <param name="listAgenda"></param>
        /// <returns></returns>
        private List<Model.Calendario.Agenda> FiltraListaPorPerfil(List<Model.Calendario.Agenda> listAgenda)
        {
            if (listAgenda.Count > 0 && 
                Constants.ConstPermissoes.profissional.Equals(GetUsuarioSession().Item1.Permissoes.FirstOrDefault()))
            {
                listAgenda = listAgenda.Where(l => l.Funcionario.Cpf.Equals(GetUsuarioSession().Item1.Cpf)).ToList();
            }

            return listAgenda;
        }

        private List<DataSelectControl> GetListModalidadesProfissional(string cpf)
        {
            var resultService = _funcionarioProxy.Get(cpf);

            var listModalidades = new List<DataSelectControl>();
            if (resultService.Status)
            {
                listModalidades = GetListModalidades();

                var modalidadesProf = resultService.Value.Modalidades;
                listModalidades = listModalidades.Where(l => modalidadesProf.Contains(l.Value)).ToList();
            }

            return listModalidades;
        }

        private DateTime ConfiguraDataExibir()
        {
            var data = DateTime.Now.Date;

            //Ajuste para listar a Segunda-Feira, caso o dia atual seja em um final de semana
            if (data.DayOfWeek == DayOfWeek.Saturday)
                data = data.AddDays(2);
            else if (data.DayOfWeek == DayOfWeek.Sunday)
                data = data.AddDays(1);

            return data;
        }

        private List<EventoCalendario> GetAgendaCalendarioPorDatas(string dataInicio, string dataFim)
        {
            var resultService = _agendaProxy.ListAgendaPeriodo(dataInicio, dataFim);
            var listAgendaDoMes = FiltraListaPorPerfil(resultService.Value);

            var list = listAgendaDoMes.Select(l =>
                                       new EventoCalendario
                                       {
                                           IdConsulta = l.Id,
                                           Titulo = l.Modalidade,
                                           Descricao = GetDescricaoEvento(l),
                                           ComecaEm = replacesService.FromMilliseconds(l.DateTimeService),
                                           TerminaEm = replacesService.FromMilliseconds(l.DateTimeService).AddHours(1),
                                           CorEvento = GetCorEvento(replacesService.FromMilliseconds(l.DateTimeService))
                                       }).ToList();

            return list.OrderBy(l => l.ComecaEm).ToList();
        }

        private string GetTituloEvento(Model.Calendario.Agenda agenda)
        {
            var paciente = agenda.Paciente.Nome.Substring(0, Math.Min(8, agenda.Paciente.Nome.Length));
            var profissional = agenda.Funcionario.Nome.Substring(0, Math.Min(8, agenda.Funcionario.Nome.Length));
            var titulo = agenda.Modalidade + ", " + paciente + ", " + profissional;

            return titulo;
        }

        private string GetDescricaoEvento(Model.Calendario.Agenda agenda)
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

        private List<Model.Pessoa.Paciente> GetListPacientes()
        {
            return _pacienteProxy.List().Value;
        }

        private List<Model.Pessoa.Funcionario> GetListProfissionais()
        {
            return _funcionarioProxy.ListProfissionais().Value;
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