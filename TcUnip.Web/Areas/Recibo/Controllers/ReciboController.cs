using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TcUnip.Model.Common;
using TcUnip.Model.FluxoCaixa;
using TcUnip.Web.Controllers;
using TcUnip.Web.Models.Proxy.Contract;
using TcUnip.Web.Util;

namespace TcUnip.Web.Areas.Recibo.Controllers
{
    public class ReciboController : BaseController
    {
        readonly IAgendaProxy _agendaProxy;
        readonly IFluxoCaixaProxy _fluxoCaixaProxy;
        readonly Mensagens mensagens = new Mensagens();        

        public ReciboController(IFluxoCaixaProxy fluxoCaixaProxy, IAgendaProxy agendaProxy)
        {
            this._fluxoCaixaProxy = fluxoCaixaProxy;
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
                var resultService = _fluxoCaixaProxy.ListRecibosDoDia();

                var list = FiltraListaPorPerfil(resultService.Value);

                msgExibicao = resultService.Message;
                msgAnalise = !resultService.Status ? "Falha!" : string.Empty;

                return PartialView("_GridRecibos", list);
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

        public ActionResult ListaRecibosPorDatas(string dataInicio, string dataFim)
        {
            ViewBag.Usuario = GetUsuarioSession().Item1;

            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var dadosPesquisa = new PesquisaModel {
                    DataIncio = Convert.ToDateTime(dataInicio),
                    DataFim = Convert.ToDateTime(dataFim)
                };

                var resultService = _fluxoCaixaProxy.ListRecibosPeriodo(dadosPesquisa);

                var list = FiltraListaPorPerfil(resultService.Value);

                msgExibicao = resultService.Message;
                msgAnalise = !resultService.Status ? "Falha!" : string.Empty;

                return PartialView("_GridRecibos", list);
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

        public ActionResult ModalVisualizar(int id)
        {
            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            ViewBag.Usuario = GetUsuarioSession().Item1;

            try
            {
                var resultService = _fluxoCaixaProxy.GetRecibo(id);

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
                var msgsRetornos = ErrosService.GetMensagemService(ex, HttpContext.Response);
                msgExibicao = msgsRetornos.Item1;
                msgAnalise = !string.IsNullOrEmpty(msgsRetornos.Item2) ? msgsRetornos.Item2 : Constants.Constants.msgFalhaAoCarregar;
            }

            var mensagensRetorno = mensagens.ConfiguraMensagemRetorno(msgExibicao, msgAnalise);
            return Json(new { mensagensRetorno }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ModalVisualizarImpressao(int id)
        {
            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            ViewBag.Usuario = GetUsuarioSession().Item1;

            try
            {
                var resultService = _fluxoCaixaProxy.GetRecibo(id);                

                if (resultService.Status)
                {
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
                var msgsRetornos = ErrosService.GetMensagemService(ex, HttpContext.Response);
                msgExibicao = msgsRetornos.Item1;
                msgAnalise = !string.IsNullOrEmpty(msgsRetornos.Item2) ? msgsRetornos.Item2 : Constants.Constants.msgFalhaAoCarregar;
            }

            var mensagensRetorno = mensagens.ConfiguraMensagemRetorno(msgExibicao, msgAnalise);
            return Json(new { mensagensRetorno }, JsonRequestBehavior.AllowGet);
        }

        #region Métodos Privados

        private List<ReciboModel> FiltraListaPorPerfil(List<ReciboModel> listRecibos)
        {
            if (listRecibos.Count > 0 &&
                Constants.ConstPermissoes.profissional.Equals(GetUsuarioSession().Item1.TipoPerfil.Permissao))
            {
                listRecibos = 
                    listRecibos.Where(l => l.Profissional.Equals(GetUsuarioSession().Item1.Funcionario.Pessoa.Nome))
                               .ToList();
            }

            return listRecibos;
        }

        #endregion

    }
}