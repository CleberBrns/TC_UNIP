using System.Web.Http;
using TcUnip.AppApi.Results;
using TcUnip.Model.Agenda;
using TcUnip.Model.Common;
using TcUnip.Service.Contract.Agenda;

namespace TcUnip.AppApi.Controllers
{
    public class AgendaController : ApiController
    {
        readonly IAgendaService _agendaService;
        public AgendaController(IAgendaService agendaService)
        {
            this._agendaService = agendaService;
        }

        [HttpGet]
        [Route("api/Agenda/GetAgenda/{id}")]
        public IHttpActionResult GetAgenda(int id)
        {
            var retorno = _agendaService.Get(id);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/Agenda/ListAgendaDoDia")]
        public IHttpActionResult ListAgendaDoDia()
        {
            var retorno = _agendaService.ListAgendaDoDia();

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/Agenda/ListAgendaPeriodo/{pesquisaModel}")]
        public IHttpActionResult ListAgendaPeriodo(PesquisaModel pesquisaModel)
        {
            var retorno = _agendaService.ListAgendaPeriodo(pesquisaModel);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/Agenda/ListAgendaPeriodoFuncionario/{pesquisaModel}")]
        public IHttpActionResult ListAgendaPeriodoFuncionario(PesquisaModel pesquisaModel)
        {
            var retorno = _agendaService.ListAgendaPeriodoFuncionario(pesquisaModel);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/Agenda/ListAgendaPeriodoPaciente/{pesquisaModel}")]
        public IHttpActionResult ListAgendaPeriodoPaciente(PesquisaModel pesquisaModel)
        {
            var retorno = _agendaService.ListAgendaPeriodoPaciente(pesquisaModel);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpPost]
        [Route("api/Agenda/SalvaAgenda")]
        public IHttpActionResult SalvaAgenda(SessaoModel model)
        {
            var retorno = _agendaService.Salva(model);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpDelete]
        [Route("api/Agenda/ExcluiAgenda/{id}")]
        public IHttpActionResult ExcluiAgenda(int id)
        {
            var retorno = _agendaService.Exclui(id);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }
    }
}