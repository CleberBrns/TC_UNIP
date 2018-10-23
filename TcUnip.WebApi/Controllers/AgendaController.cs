using System.Web.Http;
using TcUnip.Model.Calendario;
using TcUnip.Service.Contract.Calendario;
using TcUnip.WebApi.Results;

namespace TcUnip.WebApi.Controllers
{
    public class AgendaController : ApiController
    {
        readonly IAgendaService _agendaService;
        public AgendaController(IAgendaService agendaService)
        {
            this._agendaService = agendaService;
        }

        [HttpGet]
        [Route("api/Calendario/GetAgenda/{id}")]
        public IHttpActionResult GetAgenda(string id)
        {
            var retorno = _agendaService.Get(id);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/Calendario/ListAgendaDoDia/{getFromSession}")]
        public IHttpActionResult ListAgendaDoDia(bool getFromSession)
        {
            var retorno = _agendaService.ListAgendaDoDia(getFromSession);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/Calendario/ListAgendaPeriodo/{dateFrom}/{dateTo}/{getFromSession}")]
        public IHttpActionResult ListAgendaPeriodo(string dateFrom, string dateTo, bool getFromSession)
        {
            var retorno = _agendaService.ListAgendaPeriodo(dateFrom, dateTo, getFromSession);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/Calendario/ConsultasPeriodoFuncionario/{dateFrom}/{dateTo}/{getFromSession}")]
        public IHttpActionResult ConsultasPeriodoFuncionario(string cpf, string dateFrom, string dateTo)
        {
            var retorno = _agendaService.ConsultasPeriodoFuncionario(cpf, dateFrom, dateTo);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/Calendario/ConsultasPeriodoPaciente/{dateFrom}/{dateTo}/{getFromSession}")]
        public IHttpActionResult ConsultasPeriodoPaciente(string cpf, string dateFrom, string dateTo)
        {
            var retorno = _agendaService.ConsultasPeriodoPaciente(cpf, dateFrom, dateTo);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpPost]
        [Route("api/Calendario/SalvaAgenda")]
        public IHttpActionResult SalvaAgenda(Agenda model)
        {
            var retorno = _agendaService.Save(model);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpDelete]
        [Route("api/Calendario/ExcluiAgenda")]
        public IHttpActionResult ExcluiAgenda(string id)
        {
            var retorno = _agendaService.Delete(id);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }
    }
}