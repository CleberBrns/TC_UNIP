using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TcUnip.Model.Cadastro;
using TcUnip.Model.Common;
using TcUnip.Web.Controllers;
using TcUnip.Web.Models.Local;
using TcUnip.Web.Models.Proxy.Contract;
using TcUnip.Web.Util;

namespace TcUnip.Web.Areas.Funcionario.Controllers
{    
    public class FuncionarioController : BaseController
    {
        readonly ICadastroProxy _cadastroProxy;
        readonly Mensagens mensagens = new Mensagens();        

        public FuncionarioController(ICadastroProxy cadastroProxy)
        {
            this._cadastroProxy = cadastroProxy;
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
                var list = new List<FuncionarioModel>();

                var resultService = _cadastroProxy.ListFuncionario();
                list = resultService.Value;

                msgExibicao = resultService.Message;
                msgAnalise = !resultService.Status ? "Falha!" : string.Empty;

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
            ValidaAutorizaoAcessoUsuario(Constants.ConstPermissoes.gerenciamento);

            ViewBag.Usuario = GetUsuarioSession().Item1;

            ViewBag.ListStatus = GetListStatus();
            ViewBag.ListModalidades = GetListModalidades();

            //var model = new FuncionarioModel();
            //var defaultObj = model.GetModelDefault();
            return PartialView("_Gerenciar", new FuncionarioModel());
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
                ViewBag.ListModalidades = GetListModalidades();

                var resultService = _cadastroProxy.GetFuncionario(id);

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
        public ActionResult Salvar(FuncionarioModel model)
        {
            ValidaAutorizaoAcessoUsuario(Constants.ConstPermissoes.gerenciamento);

            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var resultService = _cadastroProxy.SalvaFuncionario(model);

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
        public ActionResult Excluir(int id)
        {
            ValidaAutorizaoAcessoUsuario(Constants.ConstPermissoes.gerenciamento);

            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            try
            {
                var resultService = _cadastroProxy.ExcluiFuncionario(id);

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

        private List<FuncionarioModel> ConfiguraListaExibicao(List<FuncionarioModel> list)
        {
            //Seleciona somente os itens há serem exibidos para melhor performance
            if (list != null && list.Count > 0)
            {
                list = list.Select(l =>
                    new FuncionarioModel
                    {
                        Pessoa = new PessoaModel
                        {
                            Nome = l.Pessoa.Nome,
                            Cpf = l.Pessoa.Cpf,
                            Email = l.Pessoa.Email
                        },
                        Ativo = l.Ativo
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
            var listModalidadesSelect = new List<DataSelectControl>();
            var listModalidade = new List<ModalidadeModel>();
            var listModalidadesSession = GetListModalidadesSession();

            if (listModalidadesSession.Item2)
                listModalidade = listModalidadesSession.Item1;
            else
            {
                CommonSelectControls commonSelectControls = new CommonSelectControls();
                listModalidade = commonSelectControls.ListModalidades();
            }

            listModalidadesSelect = listModalidade.Select(l => new DataSelectControl
            {
                Name = l.Nome,
                IntValue = l.Id
            }).ToList();

            return listModalidadesSelect;
        }        

        #endregion
    }
}