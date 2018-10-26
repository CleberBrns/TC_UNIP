using System.Web.Http;
using TcUnip.Model.Pessoa;
using TcUnip.Service.Contract.Pessoa;
using TcUnip.Api.Results;

namespace TcUnip.Api.Controllers
{
    public class PessoaController : ApiController
    {
        readonly IPacienteService _pacienteService;
        readonly IFuncionarioService _funcionarioService;
        readonly IUsuarioService _usuarioService;

        public PessoaController(IPacienteService pacienteService,
                                IFuncionarioService funcionarioService,
                                IUsuarioService usuarioService)
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
        [Route("api/Pessoa/ListPacientes")]
        public IHttpActionResult ListPacientes()
        {
            var retorno = _pacienteService.List();
            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpPost]
        [Route("api/Pessoa/SalvaPaciente")]
        public IHttpActionResult SalvaPaciente(Paciente model)
        {
            var retorno = _pacienteService.Salva(model);
            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpDelete]
        [Route("api/Pessao/ExcluiPaciente")]
        public IHttpActionResult ExcluiPaciente(string cpf)
        {
            var retorno = _pacienteService.Exclui(cpf);
            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        #endregion

        #region Funcionário

        [HttpGet]
        [Route("api/Pessoa/GetFuncionario/{cpf}")]
        public IHttpActionResult GetFuncionario(string cpf)
        {
            var retorno = _funcionarioService.Get(cpf);
            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/Pessoa/ListFuncionarios")]
        public IHttpActionResult ListFuncionarios()
        {
            var retorno = _funcionarioService.List();
            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/Pessoa/ListProfissionais")]
        public IHttpActionResult ListProfissionais()
        {
            var retorno = _funcionarioService.ListProfissionais();
            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpPost]
        [Route("api/Pessoa/SalvaFuncionario")]
        public IHttpActionResult SalvaFuncionario(Funcionario model)
        {
            var retorno = _funcionarioService.Salva(model);
            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpDelete]
        [Route("api/Pessao/ExcluiFuncionario")]
        public IHttpActionResult ExcluiFuncionario(string cpf)
        {
            var retorno = _funcionarioService.Exclui(cpf);
            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        #endregion

        #region Usuário

        [HttpGet]
        [Route("api/Pessoa/GetUsuario/{cpf}")]
        public IHttpActionResult GetUsuario(string email)
        {
            var retorno = _usuarioService.Get(email);
            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/Pessoa/ListUsuarios")]
        public IHttpActionResult ListUsuarios()
        {
            var retorno = _usuarioService.List();
            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpPost]
        [Route("api/Pessoa/AutenticaUsuario")]
        public IHttpActionResult AutenticaUsuario(Usuario model)
        {
            var retorno = _usuarioService.Autentica(model);
            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpPost]
        [Route("api/Pessoa/SalvaUsuario")]
        public IHttpActionResult SalvaUsuario(Usuario model)
        {
            var retorno = _usuarioService.Salva(model);
            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpDelete]
        [Route("api/Pessao/ExcluiUsuario")]
        public IHttpActionResult ExcluiUsuario(string cpf)
        {
            var retorno = _usuarioService.Excliu(cpf);
            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }


        #endregion
    }
}