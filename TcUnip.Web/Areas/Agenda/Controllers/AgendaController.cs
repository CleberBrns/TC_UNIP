using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;
using TcUnip.Model.Agenda;
using TcUnip.Model.Cadastro;
using TcUnip.Model.Common;
using TcUnip.Web.Controllers;
using TcUnip.Web.Models.Local;
using TcUnip.Web.Models.Proxy.Contract;
using TcUnip.Web.Session;
using TcUnip.Web.Util;

namespace TcUnip.Web.Areas.Agenda.Controllers
{
    public class AgendaController : BaseController
    {
        readonly IAgendaProxy _agendaProxy;
        readonly ICadastroProxy _cadastroProxy;
        readonly ICommonProxy _commonProxy;

        readonly Constants.Constants constants = new Constants.Constants();
        readonly Mensagens mensagens = new Mensagens();        

        public AgendaController(IAgendaProxy agendaProxy, ICadastroProxy _cadastroProxy, ICommonProxy commonProxy)
        {
            this._agendaProxy = agendaProxy;
            this._cadastroProxy = _cadastroProxy;
            this._commonProxy = commonProxy;
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
                var msgsRetornos = ErrosService.GetMensagemService(ex, HttpContext.Response);
                msgExibicao = msgsRetornos.Item1;
                msgAnalise = !string.IsNullOrEmpty(msgsRetornos.Item2) ? msgsRetornos.Item2 : Constants.Constants.msgFalhaAoListar;
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
                var dadosPesquisa = new PesquisaModel
                {
                    DataIncio = Convert.ToDateTime(dataInicio),
                    DataFim = Convert.ToDateTime(dataFim)
                };
                var resultService = _agendaProxy.ListAgendaPeriodo(dadosPesquisa);

                var list = FiltraListaPorPerfil(resultService.Value);

                msgExibicao = resultService.Message;
                msgAnalise = !resultService.Status ? "Falha!" : string.Empty;

                return PartialView("_GridConsultas", list);
            }
            catch (Exception ex)
            {
                var msgsRetornos = ErrosService.GetMensagemService(ex, HttpContext.Response);
                msgExibicao = msgsRetornos.Item1;
                msgAnalise = !string.IsNullOrEmpty(msgsRetornos.Item2) ? msgsRetornos.Item2 : Constants.Constants.msgFalhaAoListar;
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
                var msgsRetornos = ErrosService.GetMensagemService(ex, HttpContext.Response);
                msgExibicao = msgsRetornos.Item1;
                msgAnalise = !string.IsNullOrEmpty(msgsRetornos.Item2) ? msgsRetornos.Item2 : Constants.Constants.msgFalhaAoListar;
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
                var msgsRetornos = ErrosService.GetMensagemService(ex, HttpContext.Response);
                msgExibicao = msgsRetornos.Item1;
                msgAnalise = !string.IsNullOrEmpty(msgsRetornos.Item2) ? msgsRetornos.Item2 : Constants.Constants.msgFalhaAoListar;
            }

            var mensagensRetorno = mensagens.ConfiguraMensagemRetorno(msgExibicao, msgAnalise);
            return Json(new { mensagensRetorno }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Modal Gerenciar

        public ActionResult GetModalidadesProfissional(int id)
        {
            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var listModalidades = GetListModalidadesProfissional(id);

                return Json(listModalidades, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var msgsRetornos = ErrosService.GetMensagemService(ex, HttpContext.Response);
                msgExibicao = msgsRetornos.Item1;
                msgAnalise = !string.IsNullOrEmpty(msgsRetornos.Item2) ? msgsRetornos.Item2 : Constants.Constants.msgFalhaAoListar;
            }

            var mensagensRetorno = mensagens.ConfiguraMensagemRetorno(msgExibicao, msgAnalise);
            return Json(new { mensagensRetorno }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Salvar(SessaoModel model)
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

            var model = new SessaoModel();
            //model = model.GetModelDefault();

            if (!string.IsNullOrEmpty(data))
                model.Data = Convert.ToDateTime(data);
            else
                model.Data = ConfiguraDataExibir();
           
            return PartialView("_Gerenciar", model);
        }

        public ActionResult ModalEditar(int id)
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
                    ViewBag.ListModalidades = GetListModalidadesProfissional(model.Funcionario.Id);                    

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
                var msgsRetornos = ErrosService.GetMensagemService(ex, HttpContext.Response);
                msgExibicao = msgsRetornos.Item1;
                msgAnalise = !string.IsNullOrEmpty(msgsRetornos.Item2) ? msgsRetornos.Item2 : Constants.Constants.msgFalhaAoCarregar;
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
        public ActionResult Excluir(int id)
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
                var msgsRetornos = ErrosService.GetMensagemService(ex, HttpContext.Response);
                msgExibicao = msgsRetornos.Item1;
                msgAnalise = !string.IsNullOrEmpty(msgsRetornos.Item2) ? msgsRetornos.Item2 : Constants.Constants.msgFalhaAoExcluir;
            }

            var mensagensRetorno = mensagens.ConfiguraMensagemRetorno(msgExibicao, msgAnalise);
            return Json(new { mensagensRetorno }, JsonRequestBehavior.AllowGet);
        }

        #region Métods Privados

        /// <summary>
        /// O acesso do Profissional lista apenas as consultas marcadas com o mesmo
        /// </summary>
        /// <param name="listSessoes"></param>
        /// <returns></returns>
        private List<SessaoModel> FiltraListaPorPerfil(List<SessaoModel> listSessoes)
        {
            if (listSessoes.Count > 0 && 
                Constants.ConstPermissoes.profissional.Equals(GetUsuarioSession().Item1.TipoPerfil.Permissao))
            {
                listSessoes = listSessoes.Where(l => l.Funcionario.Pessoa.Cpf.Equals(GetUsuarioSession().Item1.Cpf)).ToList();
            }

            return listSessoes;
        }

        private List<DataSelectControl> GetListModalidadesProfissional(int id)
        {
            var resultService = _cadastroProxy.GetFuncionario(id);

            var listModalidades = GetListModalidades();
            if (resultService.Status && resultService.Value.Modalidades.Count > 0)
            {
                var idsModalidades = resultService.Value.Modalidades.Select(m => m.IdModalidade).ToArray();
                listModalidades = listModalidades.Where(l => idsModalidades.Contains(l.IntValue)).ToList();
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
            var dadosPesquisa = new PesquisaModel
            {
                DataIncio = Convert.ToDateTime(dataInicio),
                DataFim = Convert.ToDateTime(dataFim)
            };
            var resultService = _agendaProxy.ListAgendaPeriodo(dadosPesquisa);
            var listAgendaDoMes = FiltraListaPorPerfil(resultService.Value);

            var list = listAgendaDoMes.Select(l =>
                                       new EventoCalendario
                                       {
                                           IdConsulta = l.Id,
                                           Titulo = l.Modalidade.Nome,
                                           Descricao = GetDescricaoEvento(l),
                                           ComecaEm = l.Data,
                                           TerminaEm = l.Data.AddHours(1),
                                           CorEvento = GetCorEvento(l.Data)
                                       }).ToList();

            return list.OrderBy(l => l.ComecaEm).ToList();
        }

        private string GetTituloEvento(SessaoModel agenda)
        {
            var paciente = agenda.Paciente.Pessoa.Nome.Substring(0, Math.Min(8, agenda.Paciente.Pessoa.Nome.Length));
            var profissional = agenda.Funcionario.Pessoa.Nome.Substring(0, Math.Min(8, agenda.Funcionario.Pessoa.Nome.Length));
            var titulo = agenda.Modalidade + ", " + paciente + ", " + profissional;

            return titulo;
        }

        private string GetDescricaoEvento(SessaoModel agenda)
        {
            return "Sessão de " + agenda.Modalidade.Nome + ", " +
                   " agendada para o Paciente " + agenda.Paciente.Pessoa.Nome +
                   " com o Profissional " + agenda.Funcionario.Pessoa.Nome;                 
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

        private List<PacienteModel> GetListPacientes()
        {
            return _cadastroProxy.ListPaciente().Value;
        }

        private List<FuncionarioModel> GetListProfissionais()
        {
            return _cadastroProxy.ListProfissionais().Value;
        }

        private List<DataSelectControl> GetListModalidades()
        {
            var listModalidadesSelect = new List<DataSelectControl>();
            var listModalidade = new List<ModalidadeModel>();

            listModalidade = _commonProxy.ListModalidades().Value;
            listModalidadesSelect = listModalidade.Select(l => new DataSelectControl
            {
                Name = l.Nome,
                IntValue = l.Id
            }).ToList();

            return listModalidadesSelect;
        }

        private List<DataSelectControl> GetListHorarios()
        {
            return constants.ListHorariosConsultas();
        }

        #endregion
    }
}