using System;
using System.Collections.Generic;
using System.Linq;
using TcUnip.Data.Contract.Agenda;
using TcUnip.Data.Contract.FluxoCaixa;
using TcUnip.Model.Agenda;
using TcUnip.Model.Common;
using TcUnip.Model.FluxoCaixa;
using TcUnip.Service.Contract.FluxoCaixa;

namespace TcUnip.Service.FluxoCaixa
{
    public class FluxoCaixaService : IFluxoCaixaService
    {
        readonly Util.ConfiguraPesquisa configuraPesquia = new Util.ConfiguraPesquisa();
        #region Propriedades e Construtor

        readonly ICaixaRepository _caixaRepository;
        readonly ISessaoRepository _sessaoRepository;

        public FluxoCaixaService(ICaixaRepository caixaRepository, ISessaoRepository sessaoRepository)
        {
            this._caixaRepository = caixaRepository;
            this._sessaoRepository = sessaoRepository;
        }

        #endregion

        #region Caixa
        public Result<CaixaModel> GetCaixa(int id)
        {
            var result = new Result<CaixaModel>();
            result.Value = _caixaRepository.GetById(id);

            if (result.Value == null)
            {
                result.Message = "O registro não existe mais na base de dados!";
                result.Status = false;
            }

            return result;
        }

        public Result<List<CaixaModel>> ListCaixaDoDia()
        {
            var result = new Result<List<CaixaModel>>();

            var list = ConfiguraSaldoCaixa(_caixaRepository.ListCaixaPeriodo(configuraPesquia.GetPesquisaDoDia()));
            result.Value = list;

            return result;
        }

        public Result<List<CaixaModel>> ListCaixaPeriodo(PesquisaModel pesquisaModel)
        {
            pesquisaModel = configuraPesquia.ConfiguraDatasPesquisa(pesquisaModel);
            var result = new Result<List<CaixaModel>>();

            var list = ConfiguraSaldoCaixa(_caixaRepository.ListCaixaPeriodo(pesquisaModel));
            result.Value = list;

            return result;
        }

        public Result<bool> SalvaCaixa(CaixaModel model)
        {
            var result = new Result<bool>();
            result.Value = false;
            result.Status = false;

            model.Credito = Convert.ToDecimal(model.CreditoCadastro);
            model.Debito = Convert.ToDecimal(model.DebitoCadastro);

            if (model.Credito <= 0 && model.Debito <= 0)
                result.Message = "É obrigatório incluir valor nos campos Crédito ou Débito!";
            else
            {
                if (model.Id == 0)
                {
                    model = _caixaRepository.Salvar(model);
                    if (model.Id != 0)
                    {
                        result.Message = "Registro salvo com sucesso!";
                        result.Value = true;
                        result.Status = true;
                    }
                    else
                        result.Message = "Falha ao salvar o registro!";
                }
                else
                {
                    result.Value = _caixaRepository.Atualizar(model);

                    if (result.Value)
                    {
                        result.Message = "Registro atualizado com sucesso!";
                        result.Value = true;
                        result.Status = true;
                    }
                    else
                        result.Message = "Falha ao atualizar o registro!";
                }
            }

            return result;
        }

        public Result<bool> ExcluiCaixa(int id)
        {
            var result = new Result<bool>();
            result.Status = false;

            result.Value = _caixaRepository.Excluir(x => x.Id == id);

            if (result.Value)
            {
                result.Message = "Registro excluído com sucesso!";
                result.Status = true;
            }
            else
                result.Message = "Falha ao excluir o registro!";

            return result;
        }

        private List<CaixaModel> ConfiguraSaldoCaixa(List<CaixaModel> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var isFirst = i == 0;
                var itemLista = list[i];
                var saldoAnterior = !isFirst ? list[(i - 1)].Saldo : 0;

                itemLista.Saldo = itemLista.Credito - itemLista.Debito;

                if (!isFirst)
                    itemLista.Saldo += saldoAnterior;
            }

            return list;
        }

        #endregion

        #region Recibo

        public Result<ReciboModel> GetRecibo(int id)
        {
            var result = new Result<ReciboModel>();
            var sessao = _sessaoRepository.GetById(id);

            if (sessao != null)            
                result.Value = ConfiguraRecibo(sessao);            
            else
            {
                result.Message = "A Sessão não existe mais na base de dados!";
                result.Status = false;
            }

            return result;
        }
        public Result<List<ReciboModel>> ListRecibosDoDia()
        {
            var result = new Result<List<ReciboModel>>();
            var listSessoes = _sessaoRepository.ListSessoesPeriodo(configuraPesquia.GetPesquisaDoDia());

            result.Value = ConfiguraListaRecibo(listSessoes);

            return result;
        }

        public Result<List<ReciboModel>> ListRecibosPeriodo(PesquisaModel pesquisaModel)
        {
            pesquisaModel = configuraPesquia.ConfiguraDatasPesquisa(pesquisaModel);
            var result = new Result<List<ReciboModel>>();
            var listSessoes = _sessaoRepository.ListSessoesPeriodo(pesquisaModel);

            result.Value = ConfiguraListaRecibo(listSessoes);

            return result;
        }

        public Result<List<ReciboModel>> ListRecibosPeriodoFuncionario(PesquisaModel pesquisaModel)
        {
            pesquisaModel = configuraPesquia.ConfiguraDatasPesquisa(pesquisaModel);
            var result = new Result<List<ReciboModel>>();
            var listSessoes = _sessaoRepository.ListSessoesPeriodoFuncionario(pesquisaModel);

            result.Value = ConfiguraListaRecibo(listSessoes);

            return result;
        }

        public Result<List<ReciboModel>> ListRecibosPeriodoPaciente(PesquisaModel pesquisaModel)
        {
            pesquisaModel = configuraPesquia.ConfiguraDatasPesquisa(pesquisaModel);
            var result = new Result<List<ReciboModel>>();
            var listSessoes = _sessaoRepository.ListSessoesPeriodoPaciente(pesquisaModel);

            result.Value = ConfiguraListaRecibo(listSessoes);

            return result;
        }

        #region Métodos Privados

        /// <summary>
        /// Configura o Model Sessão para carregar os dados do Recibo
        /// </summary>
        /// <param name="modelRetorno"></param>
        /// <returns></returns>
        private ReciboModel ConfiguraRecibo(SessaoModel modelRetorno)
        {
            var recibo = new ReciboModel();
            recibo = new ReciboModel
            {
                IdSessao = modelRetorno.Id,
                Paciente = modelRetorno.Paciente.Pessoa.Nome,
                CpfPaciente = modelRetorno.Paciente.Pessoa.Cpf,
                Profissional = modelRetorno.Funcionario.Pessoa.Nome,
                Data = modelRetorno.Data,
                Valor = modelRetorno.Valor.ToString(),
                ModalidadeSessao = modelRetorno.Modalidade.Nome
            };

            return recibo;
        }

        /// <summary>
        /// Configura a Lista de Models Sessão para carregar os dados da Lista de Models Recibo
        /// </summary>
        /// <param name="listRetorno"></param>
        /// <returns></returns>
        private List<ReciboModel> ConfiguraListaRecibo(List<SessaoModel> listRetorno)
        {
            var listaConfigurada = new List<ReciboModel>();
            listaConfigurada = listRetorno.Select(r =>
                                       new ReciboModel
                                       {
                                           IdSessao = r.Id,
                                           Paciente = r.Paciente.Pessoa.Nome,
                                           CpfPaciente = r.Paciente.Pessoa.Cpf,
                                           Profissional = r.Funcionario.Pessoa.Nome,
                                           Data = r.Data,
                                           Valor = r.Valor.ToString(),
                                           ModalidadeSessao = r.Modalidade.Nome
                                       })
                                       .ToList();

            return listaConfigurada.OrderBy(l => l.Data).ToList();
        }

        #endregion

        #endregion
    }
}
