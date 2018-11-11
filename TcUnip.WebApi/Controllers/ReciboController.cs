using System.Web.Http;
using TcUnip.Api.Results;
using TcUnip.Service.Contract.Contabil;

namespace TcUnip.Api.Controllers
{
    public class ReciboController : ApiController
    {
        readonly IReciboService _reciboService;
        public ReciboController(IReciboService reciboService)
        {
            this._reciboService = reciboService;
        }

        [HttpGet]
        [Route("api/Recibo/GetRecibo/{id}")]
        public IHttpActionResult GetRecibo(string id)
        {
            var retorno = _reciboService.Get(id);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/Recibo/ListRecibosDoDia")]
        public IHttpActionResult ListRecibosDoDia()
        {
            var retorno = _reciboService.ListRecibosDoDia();

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/Recibo/ListRecibosPeriodo/{dateFrom}/{dateTo}")]
        public IHttpActionResult ListRecibosPeriodo(string dateFrom, string dateTo)
        {
            var retorno = _reciboService.ListRecibosPeriodo(dateFrom, dateTo);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/Recibo/ListRecibosPeriodoFuncionario/{dateFrom}/{dateTo}/{getFromSession}")]
        public IHttpActionResult RecibosPeriodoFuncionario(string cpf, string dateFrom, string dateTo)
        {
            var retorno = _reciboService.ListRecibosPeriodoFuncionario(cpf, dateFrom, dateTo);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/Recibo/ListRecibosPeriodoPaciente/{dateFrom}/{dateTo}/{getFromSession}")]
        public IHttpActionResult RecibosPeriodoPaciente(string cpf, string dateFrom, string dateTo)
        {
            var retorno = _reciboService.ListRecibosPeriodoPaciente(cpf, dateFrom, dateTo);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }
    }
}