using System.Web.Http;
using TcUnip.Api.Results;
using TcUnip.Model.Pessoa;
using TcUnip.Service.Contract.Pessoa;

namespace TcUnip.Api.Controllers
{
    public class PessoaController : ApiController
    {
        readonly IPacienteService _pacienteService;
        readonly IFuncionarioService _funcionarioService;
        readonly IUsuarioService _usuarioService;        

        public PessoaController(IPacienteService pacienteService, IFuncionarioService funcionarioService, IUsuarioService usuarioService)
        {
            this._pacienteService = pacienteService;
            this._funcionarioService = funcionarioService;
            this._usuarioService = usuarioService;
        }

        #region Paciente

        [HttpGet]
        [Route("api/Pessoa/GetPaciente/{cpf}")]
        public IHttpActionResult GetPaciente(string cpf)
        {
            var retorno = _pacienteService.Get(cpf);
            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/Pessoa/ListPacientes/{getFromSession}")]
        public IHttpActionResult ListPacientes(bool getFromSession)
        {
            var retorno = _pacienteService.List(getFromSession);
            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpPost]
        [Route("api/Pessoa/SalvaPaciente")]
        public IHttpActionResult SalvaPaciente(Paciente model)
        {
            var retorno = _pacienteService.Save(model);
            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpDelete]
        [Route("api/Pessao/ExcluiPaciente")]
        public IHttpActionResult ExcluiPaciente(string cpf)
        {
            var retorno = _pacienteService.Delete(cpf);
            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        #endregion

        #region Funcionário

        #endregion

        #region Usuário

        #endregion
    }
}