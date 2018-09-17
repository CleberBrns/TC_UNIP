using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TC_Clinica_Gerenciamento.Models.Local;
using TC_Clinica_Gerenciamento.Services;
using TC_Clinica_Gerenciamento.Util;

namespace TC_Clinica_Gerenciamento.Areas.Funcionario.Controllers
{
    public class FuncionarioController : Controller
    {
        Mensagens mensagens = new Mensagens();
        FuncionarioService _service = new FuncionarioService();

        public ActionResult Listagem(bool getFromSession)
        {
            if ((User)Session["user"] == null)
                return RedirectToAction("Login", "Login", new { area = "" });

            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var list = new List<Models.Servico.Funcionario>();
                var resultService = new ResultService<List<Models.Servico.Funcionario>>();

                if (getFromSession)
                {
                    var retornoSession = GetListFromSession();

                    if (retornoSession.Item2)
                        list = retornoSession.Item1;
                    else
                        resultService = GetListFromService();
                }
                else
                    resultService = GetListFromService();

                msgExibicao = resultService.message;
                msgAnalise = resultService.errorMessage;

                if (list.Count <= 0)
                    list = resultService.value;

                Session[Constants.ConstSessions.listFuncionarios] = list;

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
            ViewBag.ListStatus = GetListStatus();
            ViewBag.ListModalidades = GetListModalidades();

            var model = new Models.Servico.Funcionario();
            var defaultObj = model.GetModelDefault();
            return PartialView("_Gerenciar", defaultObj);
        }

        [HttpGet]
        public ActionResult ModalEditar(string id)
        {
            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                ViewBag.ListStatus = GetListStatus();
                ViewBag.ListModalidades = GetListModalidades();

                var funcionario = GetFromSession(id);

                if (funcionario != null)                
                    return PartialView("_Gerenciar", funcionario);                
                else
                {
                    var resultService = _service.Get(id);

                    if (resultService.status)
                        return PartialView("_Gerenciar", resultService.value);
                    else
                    {
                        msgExibicao = resultService.message;
                        msgAnalise = "Erro!";
                    }
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
        public ActionResult Salvar(Models.Servico.Funcionario model)
        {
            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var resultService = _service.Save(model);

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
        public ActionResult Excluir(string id)
        {
            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var resultService = _service.Delete(id);

                msgExibicao = resultService.message;
                msgAnalise = resultService.errorMessage;
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

        private Models.Servico.Funcionario GetFromSession(string id)
        {
            var listSession = GetListFromSession().Item1;

            if (listSession.Count > 0)
                return listSession.Where(l => l.Cpf == id).FirstOrDefault();

            return null;
        }

        private Tuple<List<Models.Servico.Funcionario>, bool> GetListFromSession()
        {
            var list = new List<Models.Servico.Funcionario>();
            var sessaoValida = false;

            if ((List<Models.Servico.Funcionario>)Session[Constants.ConstSessions.listFuncionarios] != null)
            {
                list = (List<Models.Servico.Funcionario>)Session[Constants.ConstSessions.listFuncionarios];
                sessaoValida = true;
            }

            return new Tuple<List<Models.Servico.Funcionario>, bool>(list, sessaoValida);
        }

        private ResultService<List<Models.Servico.Funcionario>> GetListFromService()
        {
            var resultService = _service.List();

            if (!resultService.status)
                resultService.errorMessage = "Erro!";

            return resultService;
        }

        private List<Models.Servico.Funcionario> ConfiguraListaExibicao(List<Models.Servico.Funcionario> list)
        {
            //Seleciona somente os itens há serem exibidos para melhor performance
            if (list != null && list.Count > 0)
            {
                list = list.Select(l =>
                    new Models.Servico.Funcionario
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

        private List<DataSelectControl> GetListModalidades()
        {
            var constants = new Constants.Constants();
            return constants.ListModalidades();
        }        

        #endregion
    }
}