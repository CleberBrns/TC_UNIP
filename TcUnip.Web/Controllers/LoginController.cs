﻿using System;
using System.Web.Mvc;
using TcUnip.Model.Cadastro;
using TcUnip.Model.Common;
using TcUnip.Web.Models.Proxy.Contract;
using TcUnip.Web.Session;
using TcUnip.Web.Util;

namespace TcUnip.Web.Controllers
{
    public class LoginController : BaseController
    {
        ICadastroProxy _UsuarioModelProxy;        
        readonly UsuarioSession session = new UsuarioSession();
        readonly string sessionName = Constants.ConstSessions.usuario;

        public LoginController(ICadastroProxy UsuarioModelProxy)
        {
            this._UsuarioModelProxy = UsuarioModelProxy;
        }

        public ActionResult Login()
        {            
            if (GetUsuarioSession().Item2)
                return RedirectToAction("Index", "Inicio");            

            return View();
        }

        [HttpGet]
        public ActionResult Autenticar(UsuarioModel model)
        {            
            var mensagens = new Mensagens();
            var constPermissao = new Constants.ConstPermissoes();            

            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            var resultService = new Result<UsuarioModel>();
            resultService.Status = false;

            try
            {
                resultService = _UsuarioModelProxy.AutenticaUsuario(model);
                model = resultService.Value;

                if (resultService.Status)
                    session.AddModelToSession(model, sessionName);

                msgExibicao = resultService.Message;
                msgAnalise = !resultService.Status ? "Falha" : string.Empty;

            }
            catch (Exception ex)
            {
                var msgsRetornos = ErrosService.GetMensagemService(ex, HttpContext.Response);
                msgExibicao = msgsRetornos.Item1;
                msgAnalise = !string.IsNullOrEmpty(msgsRetornos.Item2) ? msgsRetornos.Item2 : Constants.Constants.msgFalhaPadrao;
            }

            var mensagensRetorno = mensagens.ConfiguraMensagemRetorno(msgExibicao, msgAnalise);
            return Json(new { mensagensRetorno }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            //Remove todas as sessões criadas
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }
    }
}