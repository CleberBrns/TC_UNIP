using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TCC_Unip.Models.Local;
using TCC_Unip.Services;
using TCC_Unip.Session;
using TCC_Unip.Util;

namespace TCC_Unip.Areas.Paciente.Controllers
{
    public class PacienteController : Controller
    {
        readonly Mensagens mensagens = new Mensagens();
        readonly PacienteService _service = new PacienteService();

        readonly UsuarioSession session = new UsuarioSession();
        readonly string sessionName = Constants.ConstSessions.usuario;

        public ActionResult Listagem(bool getFromSession)
        {
            if (!session.GetModelFromSession(sessionName).Item2)
                return RedirectToAction("Login", "Login", new { area = "" });

            ViewBag.Usuario = session.GetModelFromSession(sessionName).Item1;

            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var list = new List<Models.Servico.Paciente>();               

                var resultService = _service.List(getFromSession);
                list = resultService.value;               

                msgExibicao = resultService.message;
                msgAnalise = resultService.errorMessage;               

                list = ConfiguraListaExibicao(list);
                return PartialView("_Listagem", list);
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
            ViewBag.ListStatus = GetListStatus();

            var model = new Models.Servico.Paciente();
            var defaultObj = model.GetModelDefault();
            return PartialView("_Gerenciar", defaultObj);
        }

        [HttpGet]
        public ActionResult ModalEditar(string id)
        {
            ViewBag.Usuario = session.GetModelFromSession(sessionName).Item1;

            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                ViewBag.ListStatus = GetListStatus();

                var resultService = _service.Get(id);

                if (resultService.status)
                    return PartialView("_Gerenciar", resultService.value);
                else
                {
                    msgExibicao = resultService.message;
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

        [HttpPost]
        public ActionResult Salvar(Models.Servico.Paciente model)
        {
            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {                
                var resultService = _service.Save(model);

                msgExibicao = resultService.message;
                msgAnalise = resultService.value ? resultService.errorMessage : "Falha";
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
        public ActionResult Excluir(string id)
        {
            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {                
                var resultService = _service.Delete(id);

                msgExibicao = resultService.message;
                msgAnalise = resultService.value ? resultService.errorMessage : "Falha";
            }
            catch (Exception ex)
            {
                msgExibicao = Constants.Constants.msgFalhaAoExcluir;
                msgAnalise = ex.Message;               
            }

            var mensagensRetorno = mensagens.ConfiguraMensagemRetorno(msgExibicao, msgAnalise);
            return Json(new { mensagensRetorno }, JsonRequestBehavior.AllowGet);
        }

        #region Métodos Privados

        private List<Models.Servico.Paciente> ConfiguraListaExibicao(List<Models.Servico.Paciente> list)
        {            
            //Seleciona somente os itens há serem exibidos para melhor performance
            if (list != null && list.Count > 0)
            {
                list = list.Select(l =>
                       new Models.Servico.Paciente
                        {
                            Nome = l.Nome,
                            Cpf = l.Cpf,
                            Status = l.Status,
                            Email = l.Email
                        }).ToList();
            }

            return list;
        }

        private List<DataSelectControl> GetListStatus()
        {
            var constants = new Constants.Constants();
            return constants.ListStatus();
        }

        #endregion        
    }
}