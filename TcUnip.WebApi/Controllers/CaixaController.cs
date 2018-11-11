using System.Web.Http;
using TcUnip.Api.Results;
using TcUnip.Model.Contabil;
using TcUnip.Service.Contract.Contabil;

namespace TcUnip.Api.Controllers
{
    public class CaixaController : ApiController
    {
        readonly ICaixaService _caixaService;
        public CaixaController(ICaixaService caixaService)
        {
            this._caixaService = caixaService;
        }
        
        [HttpGet]
        [Route("api/Caixa/GetCaixa/{id}")]
        public IHttpActionResult GetCaixa(string id)
        {
            var retorno = _caixaService.Get(id);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/Caixa/ListCaixaDoDia")]
        public IHttpActionResult ListCaixaDoDia()
        {
            var retorno = _caixaService.ListCaixaDoDia();

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/Caixa/ListCaixaPeriodo/{dateFrom}/{dateTo}")]
        public IHttpActionResult ListCaixaPeriodo(string dateFrom, string dateTo)
        {
            var retorno = _caixaService.ListCaixaPeriodo(dateFrom, dateTo);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpPost]
        [Route("api/Caixa/SalvaCaixa")]
        public IHttpActionResult SalvaCaixa(Caixa model)
        {
            var retorno = _caixaService.Salva(model);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpDelete]
        [Route("api/Caixa/ExcluiCaixa/{id}")]
        public IHttpActionResult ExcluiCaixa(string id)
        {
            var retorno = _caixaService.Exclui(id);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }
    }
}