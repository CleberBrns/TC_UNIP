using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TcUnip.Model.Common;
using TcUnip.Web.Controllers;
using TcUnip.Web.Models.Proxy.Contract;
using TcUnip.Web.Util;

namespace TcUnip.Web.Areas.Recibo.Controllers
{
    public class ReciboController : BaseController
    {
        readonly IAgendaProxy _agendaProxy;
        readonly IReciboProxy _reciboProxy;
        readonly Mensagens mensagens = new Mensagens();
        readonly ReplacesService replacesService = new ReplacesService();

        public ReciboController(IReciboProxy reciboProxy, IAgendaProxy agendaProxy)
        {
            this._reciboProxy = reciboProxy;
            this._agendaProxy = agendaProxy;
        }

        public ActionResult Index()
        {
            var userInfo = GetUsuarioSession();
            if (!userInfo.Item2)
                return RedirectToAction("Login", "Login", new { area = "" });

            ViewBag.Usuario = userInfo.Item1;

            return PartialView("_Index");
        }

        public ActionResult ListaRecibosDoDia()
        {
            ViewBag.Usuario = GetUsuarioSession().Item1;

            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var resultService = _reciboProxy.ListRecibosDoDia();

                var list = FiltraListaPorPerfil(resultService.Value);

                msgExibicao = resultService.Message;
                msgAnalise = !resultService.Status ? "Falha!" : string.Empty;

                return PartialView("_GridRecibos", list);
            }
            catch (Exception ex)
            {
                msgExibicao = Constants.Constants.msgFalhaAoListar;
                msgAnalise = ex.ToString();
            }

            var mensagensRetorno = mensagens.ConfiguraMensagemRetorno(msgExibicao, msgAnalise);
            return Json(new { mensagensRetorno }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListaRecibosPorDatas(string dataInicio, string dataFim)
        {
            ViewBag.Usuario = GetUsuarioSession().Item1;

            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var resultService = _reciboProxy.ListRecibosPeriodo(dataInicio, dataFim);

                var list = FiltraListaPorPerfil(resultService.Value);

                msgExibicao = resultService.Message;
                msgAnalise = !resultService.Status ? "Falha!" : string.Empty;

                return PartialView("_GridRecibos", list);
            }
            catch (Exception ex)
            {
                msgExibicao = Constants.Constants.msgFalhaAoListar;
                msgAnalise = ex.ToString();
            }

            var mensagensRetorno = mensagens.ConfiguraMensagemRetorno(msgExibicao, msgAnalise);
            return Json(new { mensagensRetorno }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ModalVisualizar(string id)
        {
            ValidaAutorizaoAcessoUsuario(Constants.ConstPermissoes.gerenciamento);

            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            ViewBag.Usuario = GetUsuarioSession().Item1;

            try
            {
                var resultService = _reciboProxy.Get(id);

                if (resultService.Status)
                {
                    return PartialView("_Visualizar", resultService.Value);
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

        public ActionResult ModalVisualizarImpressao(string id)
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
                    var constants = new Constants.Constants();

                    ViewBag.Modalidade = 
                        constants.ListModalidades().Where(m => m.Value.Equals(resultService.Value.Modalidade))
                        .Select(m => m.Name).FirstOrDefault();

                    return PartialView("_Impressao", resultService.Value);
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

        #region Métodos Privados

        private List<Model.Contabil.Recibo> FiltraListaPorPerfil(List<Model.Contabil.Recibo> listRecibos)
        {
            if (listRecibos.Count > 0 &&
                Constants.ConstPermissoes.profissional.Equals(GetUsuarioSession().Item1.Permissoes.FirstOrDefault()))
            {
                listRecibos = 
                    listRecibos.Where(l => l.Profissional.Equals(GetUsuarioSession().Item1.Funcionario.Nome))
                               .ToList();
            }

            return listRecibos;
        }

        #endregion

    }
}