using System.Web.Http;
using TcUnip.AppApi.Results;
using TcUnip.Model.Common;
using TcUnip.Model.FluxoCaixa;
using TcUnip.Service.Contract.FluxoCaixa;

namespace TcUnip.AppApi.Controllers
{
    public class FluxoCaixaController : ApiController
    {
        readonly IFluxoCaixaService _fluxoCaixaService;
        public FluxoCaixaController(IFluxoCaixaService fluxoCaixaService)
        {
            this._fluxoCaixaService = fluxoCaixaService;
        }

        #region Caixa

        [HttpGet]
        [Route("api/FluxoCaixa/GetCaixa/{id}")]
        public IHttpActionResult GetCaixa(int id)
        {
            var retorno = _fluxoCaixaService.GetCaixa(id);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/FluxoCaixa/ListCaixaDoDia")]
        public IHttpActionResult ListCaixaDoDia()
        {
            var retorno = _fluxoCaixaService.ListCaixaDoDia();

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpPost]
        [Route("api/FluxoCaixa/ListCaixaPeriodo")]
        public IHttpActionResult ListCaixaPeriodo(PesquisaModel pesquisaModel)
        {
            var retorno = _fluxoCaixaService.ListCaixaPeriodo(pesquisaModel);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpPost]
        [Route("api/FluxoCaixa/SalvaCaixa")]
        public IHttpActionResult SalvaCaixa(CaixaModel model)
        {
            if (!ModelState.IsValid)
                return new InvalidListMessageResult(ModelState);

            var retorno = _fluxoCaixaService.SalvaCaixa(model);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpDelete]
        [Route("api/FluxoCaixa/ExcluiCaixa/{id}")]
        public IHttpActionResult ExcluiCaixa(int id)
        {
            var retorno = _fluxoCaixaService.ExcluiCaixa(id);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        #endregion

        #region Recibo

        [HttpGet]
        [Route("api/FluxoCaixa/GetRecibo/{id}")]
        public IHttpActionResult GetRecibo(int id)
        {
            var retorno = _fluxoCaixaService.GetRecibo(id);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/FluxoCaixa/ListRecibosDoDia")]
        public IHttpActionResult ListRecibosDoDia()
        {
            var retorno = _fluxoCaixaService.ListRecibosDoDia();

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpPost]
        [Route("api/FluxoCaixa/ListRecibosPeriodo")]
        public IHttpActionResult ListRecibosPeriodo(PesquisaModel pesquisaModel)
        {
            var retorno = _fluxoCaixaService.ListRecibosPeriodo(pesquisaModel);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpPost]
        [Route("api/FluxoCaixa/ListRecibosPeriodoPaciente")]
        public IHttpActionResult ListRecibosPeriodoPaciente(PesquisaModel pesquisaModel)
        {
            var retorno = _fluxoCaixaService.ListRecibosPeriodoPaciente(pesquisaModel);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpPost]
        [Route("api/FluxoCaixa/ListRecibosPeriodoFuncionario")]
        public IHttpActionResult ListRecibosPeriodoFuncionario(PesquisaModel pesquisaModel)
        {
            var retorno = _fluxoCaixaService.ListRecibosPeriodoFuncionario(pesquisaModel);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        #endregion

    }
}