﻿using System;
using System.Web.Mvc;
using TcUnip.Web.Models.Local;
using TcUnip.Web.Models.Servico;
using TcUnip.Web.Services;
using TcUnip.Web.Session;
using TcUnip.Web.Util;

namespace TcUnip.Web.Controllers
{
    public class LoginController : BaseController
    {
        readonly UsuarioService service = new UsuarioService();
        readonly UsuarioSession session = new UsuarioSession();
        readonly string sessionName = Constants.ConstSessions.usuario;

        public ActionResult Login()
        {            
            if (GetUsuarioSession().Item2)
                return RedirectToAction("Index", "Inicio");            

            return View();
        }

        [HttpGet]
        public ActionResult Autenticar(Usuario model)
        {
            var _service = new UsuarioService();
            var mensagens = new Mensagens();
            var constPermissao = new Constants.ConstPermissoes();
            var usuarioMaster = constPermissao.GetUsuarioMaster();

            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            var resultService = new ResultService<Usuario>();
            resultService.Status = false;

            try
            {
                if (!string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.Senha))
                {
                    if (model.Email.Trim() == usuarioMaster.Email && model.Senha.Trim() == usuarioMaster.Senha)
                    {
                        resultService.Status = true;
                        model = usuarioMaster;
                    }
                    else
                    {
                        resultService = _service.Auth(model);
                        model = resultService.Value;
                    }

                    if (resultService.Status)
                        session.AddModelToSession(model, sessionName);                        

                    msgExibicao = resultService.Message;
                    msgAnalise = !resultService.Status ? "Falha" : string.Empty;
                }
                else
                {
                    msgExibicao = "O campo E-mail e Senha são obrigatórios!";
                    msgAnalise = Constants.Constants.msgFalhaPadrao;
                }
               
            }
            catch (Exception ex)
            {
                msgExibicao = Constants.Constants.msgFalhaAoAutenticar;
                msgAnalise = ex.Message;
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