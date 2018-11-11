using System.Web.Http;
using TcUnip.Api.Results;
using TcUnip.Service.Contract.Contabil;

namespace TcUnip.WebApi.Controllers
{
    public class ReciboController : ApiController
    {
        readonly IReciboService _reciboService;
        public ReciboController(IReciboService reciboService)
        {
            this._reciboService = reciboService;
        }

        [HttpGet]
        [Route("api/Contabilidade/GetRecibo/{id}")]
        public IHttpActionResult GetRecibo(string id)
        {
            var retorno = _reciboService.Get(id);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/Contabilidade/ListRecibosDoDia")]
        public IHttpActionResult ListRecibosDoDia()
        {
            var retorno = _reciboService.ListRecibosDoDia();

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/Contabilidade/ListRecibosPeriodo/{dateFrom}/{dateTo}")]
        public IHttpActionResult ListRecibosPeriodo(string dateFrom, string dateTo)
        {
            var retorno = _reciboService.ListRecibosPeriodo(dateFrom, dateTo);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/Contabilidade/ListRecibosPeriodoFuncionario/{dateFrom}/{dateTo}/{getFromSession}")]
        public IHttpActionResult RecibosPeriodoFuncionario(string cpf, string dateFrom, string dateTo)
        {
            var retorno = _reciboService.ListRecibosPeriodoFuncionario(cpf, dateFrom, dateTo);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/Contabilidade/ListRecibosPeriodoPaciente/{dateFrom}/{dateTo}/{getFromSession}")]
        public IHttpActionResult RecibosPeriodoPaciente(string cpf, string dateFrom, string dateTo)
        {
            var retorno = _reciboService.ListRecibosPeriodoPaciente(cpf, dateFrom, dateTo);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }
    }
}