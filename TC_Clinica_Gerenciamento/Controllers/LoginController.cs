using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TC_Clinica_Gerenciamento.Models.Local;
using TC_Clinica_Gerenciamento.Models.Servico;
using TC_Clinica_Gerenciamento.Services;
using TC_Clinica_Gerenciamento.Util;

namespace TC_Clinica_Gerenciamento.Controllers
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
        public ActionResult Login(Usuario model)
        {
            var _service = new UsuarioService();
            var mensagens = new Mensagens();
            var constPermissao = new Constants.ConstPermissoes();
            var usuarioMaster = constPermissao.GetUsuarioMaster();

            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            var resultService = new ResultService<Usuario>();

            try
            {
                if (model.Email == usuarioMaster.Email && model.Senha == usuarioMaster.Senha)                
                    resultService.status = true;
                else
                    resultService = _service.Auth(model);

                if (resultService.status)
                    Session[Constants.ConstSessions.usuario] = model;

                msgExibicao = resultService.message;
                msgAnalise = resultService.errorMessage;
            }
            catch (Exception ex)
            {
                msgExibicao = Constants.Constants.msgFalhaAoSalvar;
                msgAnalise = ex.Message;
            }

            var mensagensRetorno = mensagens.ConfiguraMensagemRetorno(msgExibicao, msgAnalise);
            return Json(new { mensagensRetorno }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Login(string nome, string senha)
        {
            if (nome.Equals("admin") && senha.Equals("56784321"))
            {
                Session["user"] = new User() { Login = nome, Nome = "Administrador" };
                return RedirectToAction("Index", "Inicio");
            }
            return View();
        }

        public ActionResult Logout()
        {
            if (Session["user"] != null)
                Session["user"] = null;

            return RedirectToAction("Index", "Home");
        }
    }
}