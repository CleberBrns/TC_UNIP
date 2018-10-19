using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TCC_Unip.Controllers;
using TCC_Unip.Models.Local;
using TCC_Unip.Services;
using TCC_Unip.Session;
using TCC_Unip.Util;

namespace TCC_Unip.Areas.Usuario.Controllers
{
    public class UsuarioController : BaseController
    {
        readonly Mensagens mensagens = new Mensagens();
        readonly UsuarioService _service = new UsuarioService();
        readonly FuncionarioService _serviceFuncionario = new FuncionarioService();

        public ActionResult Listagem(bool getFromSession)
        {
            var userInfo = GetUsuarioSession();

            if (!userInfo.Item2)
                return RedirectToAction("Login", "Login", new { area = "" });

            ViewBag.Usuario = userInfo.Item1;            

            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var list = new List<Models.Servico.Usuario>();               

                var resultService = _service.List(getFromSession);
                list = resultService.Value;

                msgExibicao = resultService.Message;
                msgAnalise = !resultService.Status ? "Falha!" : string.Empty;

                list = ConfiguraListaExibicao(list);

                //Não lista os usuários com perfil Administracao, quando o usuário logado não for um Administrador
                if (!Constants.ConstPermissoes.administracao.Contains(userInfo.Item1.Permissoes.FirstOrDefault()))
                {
                    if (list.Count > 0)
                    {
                        list = list.Where(l =>
                            l.Permissoes.FirstOrDefault() != Constants.ConstPermissoes.administracao)
                                   .ToList();
                    }
                }

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
            ViewBag.Usuario = GetUsuarioSession().Item1;
            ViewBag.ListStatus = GetListStatus();
            ViewBag.ListPerfil = GetListPerfil(true);
            ViewBag.ListFuncionarios = GetListFuncionarios();

            var model = new Models.Servico.Usuario();
            var defaultObj = model.GetModelDefault();
            return PartialView("_Gerenciar", defaultObj);
        }

        [HttpGet]
        public ActionResult ModalEditar(string id)
        {
            ViewBag.Usuario = GetUsuarioSession().Item1;
            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                ViewBag.ListStatus = GetListStatus();
                ViewBag.ListPerfil = GetListPerfil(true);
                ViewBag.ListFuncionarios = GetListFuncionarios();

                var resultService = _service.Get(id);
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

                msgExibicao = resultService.Message;
                msgAnalise = !resultService.Status ? "Falha" : string.Empty;
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

                msgExibicao = resultService.Message;
                msgAnalise = !resultService.Status ? "Falha" : string.Empty;
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

        private List<Models.Servico.Usuario> ConfiguraListaExibicao(List<Models.Servico.Usuario> list)
        {
            //Seleciona somente os itens há serem exibidos para melhor performance
            if (list != null && list.Count > 0)
            {
                var listPerfil = GetListPerfil();
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
                    PermissaoExibicao = listPerfil.Where(lp => lp.Value == l.Permissoes.FirstOrDefault())
                                           .Select(p => p.Name)
                                           .FirstOrDefault(),
                    Permissoes = listPerfil.Where(lp => lp.Value == l.Permissoes.FirstOrDefault())
                                           .Select(p => p.Value)
                                           .ToArray(),
                    Status = l.Status,
                    Funcionario = new Models.Servico.Funcionario
                    {
                        Nome = l.Funcionario.Nome
                    }
                }).ToList();
            }

            return list;
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
            var listFuncionarios = _serviceFuncionario.List(true).Value;

            if (listFuncionarios.Count > 0)
            {
                var emailsUsuariosCadastrados = GetEmailsFuncionariosCadastrados();

                if (emailsUsuariosCadastrados.Length > 0)
                {
                    //Remove os funcionários que já foram cadastrados
                    listFuncionarios = listFuncionarios.Where(l => !emailsUsuariosCadastrados.Contains(l.Email))
                                                       .ToList();
                }

                listFuncionarios = listFuncionarios.Select(l => new Models.Servico.Funcionario
                {
                    Nome = l.Nome,
                    Email = l.Email,
                    Cpf = l.Cpf,
                    Modalidades = l.Modalidades
                }).ToList();
            }

            return listFuncionarios;
        }

        private string[] GetEmailsFuncionariosCadastrados()
        {
            var emailsUsuariosCadastrados = new string[] { };
            var resultService = _service.List(true);
            if (resultService.Status)            
                emailsUsuariosCadastrados = resultService.Value.Select(x => x.Email).ToArray();            

            return emailsUsuariosCadastrados;
        }

        private List<DataSelectControl> GetListStatus()
        {
            var constants = new Constants.Constants();
            return constants.ListStatus();
        }

        private List<DataSelectControl> GetListPerfil(bool filtroPerfil = false)
        {
            var constants = new Constants.Constants();
            var listPerfil = constants.ListPermissoesPerfil();

            if (filtroPerfil)
            {
                var usuario = GetUsuarioSession().Item1;
                //Não lista o Perfil Administrador caso o usuário não seja um Administrador
                if (!usuario.Permissoes.FirstOrDefault().Equals(Constants.ConstPermissoes.administracao))
                    listPerfil = listPerfil.Where(l => l.Value != Constants.ConstPermissoes.administracao).ToList();
            }

            return listPerfil;            
        }

        #endregion
    }
}