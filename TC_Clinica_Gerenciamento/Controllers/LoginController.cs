using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCC_Unip.Models.Local;
using TCC_Unip.Models.Servico;
using TCC_Unip.Services;
using TCC_Unip.Util;

namespace TCC_Unip.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            if ((Usuario)Session[Constants.ConstSessions.usuario] != null)
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
            resultService.status = false;

            try
            {
                if (!string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.Senha))
                {
                    if (model.Email.Trim() == usuarioMaster.Email && model.Senha.Trim() == usuarioMaster.Senha)
                        resultService.status = true;
                    else
                        resultService = _service.Auth(model);

                    if (resultService.status)
                        Session[Constants.ConstSessions.usuario] = model;

                    msgExibicao = resultService.message;
                    msgAnalise = resultService.errorMessage;
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
            if ((Usuario)Session[Constants.ConstSessions.usuario] != null)
                Session[Constants.ConstSessions.usuario] = null;

            return RedirectToAction("Index", "Home");
        }
    }
}