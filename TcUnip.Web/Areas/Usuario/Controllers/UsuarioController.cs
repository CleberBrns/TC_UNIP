using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TcUnip.Model.Cadastro;
using TcUnip.Web.Controllers;
using TcUnip.Web.Models.Local;
using TcUnip.Web.Models.Proxy.Contract;
using TcUnip.Web.Session;
using TcUnip.Web.Util;

namespace TcUnip.Web.Areas.Usuario.Controllers
{
    public class UsuarioController : BaseController
    {
        readonly ICadastroProxy _cadastroProxy;
        readonly ICommonProxy _commonProxy;
        readonly Mensagens mensagens = new Mensagens();  

        public UsuarioController(ICadastroProxy cadastroProxy, ICommonProxy commonProxy)
        {
            this._cadastroProxy = cadastroProxy;
            this._commonProxy = commonProxy;
        }

        public ActionResult Listagem()
        {
            ValidaAutorizaoAcessoUsuario(Constants.ConstPermissoes.gerenciamento);

            var userInfo = GetUsuarioSession();

            if (!userInfo.Item2)
                return RedirectToAction("Login", "Login", new { area = "" });

            ViewBag.Usuario = userInfo.Item1;            

            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {               
                var resultService = _cadastroProxy.ListUsuarios();
                var list = resultService.Value;

                msgExibicao = resultService.Message;
                msgAnalise = !resultService.Status ? "Falha!" : string.Empty;

                list = ConfiguraListaExibicao(list);

                //Não lista os usuários com perfil Administracao, quando o usuário logado não for um Administrador
                if (!Constants.ConstPermissoes.administracao.Contains(userInfo.Item1.TipoPerfil.Permissao))
                {
                    if (list.Count > 0)
                    {
                        list = list.Where(l =>
                            l.TipoPerfil.Permissao != Constants.ConstPermissoes.administracao)
                                   .ToList();
                    }
                }

                return PartialView("_Listagem", list);
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
            ViewBag.ListStatus = GetListStatus();
            ViewBag.ListPerfil = GetListPerfil(true);
            ViewBag.ListFuncionarios = GetListFuncionarios();

            //var model = new UsuarioModel();
            //var defaultObj = model.GetModelDefault();
            return PartialView("_Gerenciar", 
                new UsuarioModel{ Id = 0, Ativo = true });
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
                ViewBag.ListStatus = GetListStatus();
                ViewBag.ListPerfil = GetListPerfil(true);
                ViewBag.ListFuncionarios = GetListFuncionarios();

                var resultService = _cadastroProxy.GetUsuario(id);
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

        [HttpPost]
        public ActionResult Salvar(UsuarioModel model)
        {
            ValidaAutorizaoAcessoUsuario(Constants.ConstPermissoes.gerenciamento);

            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var resultService = _cadastroProxy.SalvaUsuario(model);

                msgExibicao = resultService.Message;
                msgAnalise = !resultService.Status ? "Falha" : string.Empty;
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

        [HttpPost]
        public ActionResult Excluir(int id)
        {
            ValidaAutorizaoAcessoUsuario(Constants.ConstPermissoes.gerenciamento);

            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var resultService = _cadastroProxy.ExcluiUsuario(id);

                msgExibicao = resultService.Message;
                msgAnalise = !resultService.Status ? "Falha" : string.Empty;
            }
            catch (Exception ex)
            {
                var msgsRetornos = ErrosService.GetMensagemService(ex, HttpContext.Response);
                msgExibicao = msgsRetornos.Item1;
                msgAnalise = !string.IsNullOrEmpty(msgsRetornos.Item2) ? msgsRetornos.Item2 : Constants.Constants.msgFalhaAoExcluir;
            }

            var mensagensRetorno = mensagens.ConfiguraMensagemRetorno(msgExibicao, msgAnalise);
            return Json(new { mensagensRetorno }, JsonRequestBehavior.AllowGet);
        }

        #region Métodos Privados

        private List<UsuarioModel> ConfiguraListaExibicao(List<UsuarioModel> list)
        {
            //Seleciona somente os itens há serem exibidos para melhor performance
            if (list != null && list.Count > 0)
            {
                //var modelFuncionario = new FuncionarioModel();
                //var newFuncionario = modelFuncionario.GetModelDefault();

                list = list.Select(l =>
                new UsuarioModel
                {
                    Id = l.Id,
                    Cpf = l.Cpf,
                    Email = l.Email,
                    TipoPerfil = 
                        new TipoPerfilModel {
                            Tipo = l.TipoPerfil.Tipo
                        },
                    Ativo = l.Ativo
                }).ToList();
            }

            return list;
        }        

        private List<FuncionarioModel> GetListFuncionarios()
        {
            var listFuncionarios = _cadastroProxy.ListFuncionario().Value;

            if (listFuncionarios.Count > 0)
            {
                var emailsUsuariosCadastrados = GetEmailsFuncionariosCadastrados();

                if (emailsUsuariosCadastrados.Length > 0)
                {
                    //Remove os funcionários que já foram cadastrados
                    listFuncionarios = listFuncionarios.Where(l => !emailsUsuariosCadastrados.Contains(l.Pessoa.Email))
                                                       .ToList();
                }

                listFuncionarios = listFuncionarios.Select(l => new FuncionarioModel
                {
                    Id = l.Id,
                    Pessoa = new PessoaModel
                    {
                        Nome = l.Pessoa.Nome,
                        Email = l.Pessoa.Email,
                        Cpf = l.Pessoa.Cpf
                    },
                    Modalidades = l.Modalidades
                }).ToList();
            }

            return listFuncionarios;
        }

        private string[] GetEmailsFuncionariosCadastrados()
        {
            var emailsUsuariosCadastrados = new string[] { };
            var resultService = _cadastroProxy.ListUsuarios();
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
            var listPerfil = new List<DataSelectControl>();
            var listTipoPerfil = new List<TipoPerfilModel>();
            var listPerfilSession = GetListTipoPerfilSession();

            if (listPerfilSession.Item2)            
                listTipoPerfil = listPerfilSession.Item1;            
            else
            {
                SessionTipoPerfil sessionTipoPerfil = new SessionTipoPerfil();
                listTipoPerfil = _commonProxy.ListTipoPerfil().Value;
                sessionTipoPerfil.AddListToSession(listTipoPerfil, Constants.ConstSessions.listTipoPerfil);
            }

            listPerfil = listTipoPerfil.Select(l => new DataSelectControl
                                            {
                                                Name = l.Tipo,
                                                IntValue = l.Id
                                            })
                                       .ToList();

            if (filtroPerfil)
            {
                var usuario = GetUsuarioSession().Item1;
                //Não lista o Perfil Administrador caso o usuário não seja um Administrador
                if (!usuario.TipoPerfil.Permissao.Equals(Constants.ConstPermissoes.administracao))
                    listPerfil = listPerfil.Where(l => l.Value != Constants.ConstPermissoes.administracao).ToList();
            }

            return listPerfil;            
        }

        #endregion
    }
}