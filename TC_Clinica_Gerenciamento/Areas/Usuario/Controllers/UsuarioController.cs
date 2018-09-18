using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TCC_Unip.Models.Local;
using TCC_Unip.Models.Servico;
using TCC_Unip.Services;
using TCC_Unip.Util;

namespace TCC_Unip.Areas.Usuario.Controllers
{
    public class UsuarioController : Controller
    {        
        Mensagens mensagens = new Mensagens();
        UsuarioService _service = new UsuarioService();
        FuncionarioService _serviceFuncionario = new FuncionarioService();

        public ActionResult Listagem(bool getFromSession)
        {
            if ((Models.Servico.Usuario)Session[Constants.ConstSessions.usuario] == null)
                return RedirectToAction("Login", "Login", new { area = "" });

            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var list = new List<Models.Servico.Usuario>();
                var resultService = new ResultService<List<Models.Servico.Usuario>>();

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

                Session[Constants.ConstSessions.listUsuarios] = list;

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
            ViewBag.ListPerfil = GetListPerfil();
            ViewBag.ListFuncionarios = GetListFuncionarios();

            var model = new Models.Servico.Usuario();
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
                ViewBag.ListPerfil = GetListPerfil();
                ViewBag.ListFuncionarios = GetListFuncionarios();

                var usuario = GetFromSession(id);

                if (usuario != null)
                {
                    return PartialView("_Gerenciar", usuario);
                }
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
        public ActionResult Salvar(Models.Servico.Usuario model)
        {
            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                model.Permissoes = NormalizaPermissoes(model.Permissoes);

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

        private Models.Servico.Usuario GetFromSession(string id)
        {
            var listSession = GetListFromSession().Item1;

            if (listSession.Count > 0)
                return listSession.Where(l => l.Email == id).FirstOrDefault();

            return null;
        }

        private Tuple<List<Models.Servico.Usuario>, bool> GetListFromSession()
        {
            var list = new List<Models.Servico.Usuario>();
            var sessaoValida = false;

            if ((List<Models.Servico.Usuario>)Session[Constants.ConstSessions.listUsuarios] != null)
            {
                list = (List<Models.Servico.Usuario>)Session[Constants.ConstSessions.listUsuarios];
                sessaoValida = true;
            }

            return new Tuple<List<Models.Servico.Usuario>, bool>(list, sessaoValida);
        }

        private List<Models.Servico.Usuario> ConfiguraListaExibicao(List<Models.Servico.Usuario> list)
        {
            //Seleciona somente os itens há serem exibidos para melhor performance
            if (list != null && list.Count > 0)
            {
                var modelFuncionario = new Models.Servico.Funcionario();
                var newFuncionario = modelFuncionario.GetModelDefault();

                list.ForEach(l =>
                {
                    if (l.Funcionario == null)
                        l.Funcionario = newFuncionario;
                });

                list = list.Select(l =>
                new Models.Servico.Usuario
                {
                    Cpf = l.Cpf,
                    Email = l.Email,
                    Permissoes = l.Permissoes,
                    Status = l.Status,
                    Funcionario = new Models.Servico.Funcionario
                    {
                        Nome = l.Funcionario.Nome
                    }
                }).ToList();
            }

            return list;
        }

        private ResultService<List<Models.Servico.Usuario>> GetListFromService()
        {
            var resultService = _service.List();

            if (!resultService.status)
                resultService.errorMessage = "Erro!";

            return resultService;
        }

        private string[] NormalizaPermissoes(string[] permissoes)
        {
            if (permissoes.Length > 0)
                permissoes = new HashSet<string>(permissoes).ToArray();
            if (permissoes.Length > 0)
                permissoes = permissoes.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            if (permissoes.Length > 0)
                permissoes = permissoes.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            return permissoes;
        }

        private List<Models.Servico.Funcionario> GetListFuncionarios()
        {
            var listExibicao =
                _serviceFuncionario.List().value.Select(l => new Models.Servico.Funcionario
                {
                    Nome = l.Nome,
                    Email = l.Email,
                    Cpf = l.Cpf,
                    Modalidade = l.Modalidade
                }).ToList();

            return listExibicao;
        }

        private List<DataSelectControl> GetListStatus()
        {
            var constants = new Constants.Constants();
            return constants.ListStatus();
        }

        private List<DataSelectControl> GetListPerfil()
        {
            var constants = new Constants.Constants();
            return constants.ListPermissoesPerfil();
        }

        #endregion
    }
}