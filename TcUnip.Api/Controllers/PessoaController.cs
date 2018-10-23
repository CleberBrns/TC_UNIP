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
        [Route("api/Pessoa/ListFuncionarios/{getFromSession}")]
        public IHttpActionResult ListFuncionarios(bool getFromSession)
        {
            var retorno = _funcionarioService.List(getFromSession);
            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/Pessoa/ListProfissionais/{getFromSession}")]
        public IHttpActionResult ListProfissionais(bool getFromSession)
        {
            var retorno = _funcionarioService.ListProfissionais(getFromSession);
            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpPost]
        [Route("api/Pessoa/SalvaFuncionario")]
        public IHttpActionResult SalvaFuncionario(Funcionario model)
        {
            var retorno = _funcionarioService.Save(model);
            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpDelete]
        [Route("api/Pessao/ExcluiFuncionario")]
        public IHttpActionResult ExcluiFuncionario(string cpf)
        {
            var retorno = _funcionarioService.Delete(cpf);
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
        [Route("api/Pessoa/ListUsuarios/{getFromSession}")]
        public IHttpActionResult ListUsuarios(bool getFromSession)
        {
            var retorno = _usuarioService.List(getFromSession);
            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpPost]
        [Route("api/Pessoa/AutenticaUsuario")]
        public IHttpActionResult AutenticaUsuario(Usuario model)
        {
            var retorno = _usuarioService.Auth(model);
            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpPost]
        [Route("api/Pessoa/SalvaUsuario")]
        public IHttpActionResult SalvaUsuario(Usuario model)
        {
            var retorno = _usuarioService.Save(model);
            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpDelete]
        [Route("api/Pessao/ExcluiUsuario")]
        public IHttpActionResult ExcluiUsuario(string cpf)
        {
            var retorno = _usuarioService.Delete(cpf);
            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }


        #endregion
    }
}