using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TcUnip.Model.Common;
using TcUnip.Model.FluxoCaixa;
using TcUnip.Web.Controllers;
using TcUnip.Web.Models.Proxy.Contract;
using TcUnip.Web.Util;

namespace TcUnip.Web.Areas.Caixa.Controllers
{
    public class CaixaController : BaseController
    {
        readonly IFluxoCaixaProxy _fluxoCaixaProxy;
        readonly Mensagens mensagens = new Mensagens();

        public CaixaController(IFluxoCaixaProxy fluxoCaixaProxy)
        {
            this._fluxoCaixaProxy = fluxoCaixaProxy;
        }

        public ActionResult Index()
        {
            var userInfo = GetUsuarioSession();
            if (!userInfo.Item2)
                return RedirectToAction("Login", "Login", new { area = "" });

            ViewBag.Usuario = userInfo.Item1;

            return PartialView("_Index");
        }

        public ActionResult ListaCaixaDoDia()
        {
            ValidaAutorizaoAcessoUsuario(Constants.ConstPermissoes.gerenciamento);
            ViewBag.Usuario = GetUsuarioSession().Item1;

            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var resultService = _fluxoCaixaProxy.ListCaixaDoDia();

                var list = resultService.Value;

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

        public ActionResult ListaCaixaPorDatas(string dataInicio, string dataFim)
        {
            ValidaAutorizaoAcessoUsuario(Constants.ConstPermissoes.gerenciamento);
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

                var resultService = _fluxoCaixaProxy.ListCaixaPeriodo(dadosPesquisa);

                var list = resultService.Value;

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

        public ActionResult ModalCadastrar()
        {
            ValidaAutorizaoAcessoUsuario(Constants.ConstPermissoes.gerenciamento);

            ViewBag.Usuario = GetUsuarioSession().Item1;          

            //var model = new Model.Contabil.Caixa();
            //var defaultObj = model.GetModelDefault();
            return PartialView("_Gerenciar", new CaixaModel {
                Data = DateTime.Now
            });
        }

        [HttpGet]
        public ActionResult ModalEditar(int id)
        {
            ValidaAutorizaoAcessoUsuario(Constants.ConstPermissoes.gerenciamento);

            ViewBag.Usuario = GetUsuarioSession().Item1;

            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var resultService = _fluxoCaixaProxy.GetCaixa(id);

                if (resultService.Status)
                    return PartialView("_Gerenciar", resultService.Value);
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

        public ActionResult Salvar(CaixaModel model)
        {
            ValidaAutorizaoAcessoUsuario(Constants.ConstPermissoes.gerenciamento);

            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                model.Data = ConfiguraData(model.Data);

                var resultService = _fluxoCaixaProxy.SalvaCaixa(model);

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

        private DateTime ConfiguraData(DateTime data)
        {
            if (data.Date.ToShortTimeString() == "00:00")            
                return data.Date.Add(TimeSpan.Parse(DateTime.Now.ToShortTimeString()));            

            return data;
        }
    }
}