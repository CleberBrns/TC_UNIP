using System.Web.Http;
using TcUnip.AppApi.Results;
using TcUnip.Model.Cadastro;
using TcUnip.Service.Contract.Cadastro;

namespace TcUnip.AppApi.Controllers
{
    public class CadastroController : ApiController
    {
        readonly ICadastroService _cadastroService;
        public CadastroController(ICadastroService cadastroService)
        {
            this._cadastroService = cadastroService;
        }

        #region Usuário

        [HttpGet]
        [Route("api/Cadastro/GetUsuario/{id}")]
        public IHttpActionResult GetUsuario(int id)
        {
            var retorno = _cadastroService.GetUsuario(id);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/Cadastro/ListUsuario")]
        public IHttpActionResult ListUsuario()
        {
            var retorno = _cadastroService.ListUsuarios();

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpPost]
        [Route("api/Cadastro/AutenticaUsuario")]
        public IHttpActionResult AutenticaUsuario(UsuarioModel model)
        {
            var retorno = _cadastroService.AutenticaUsuario(model);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpPost]
        [Route("api/Cadastro/SalvaUsuario")]
        public IHttpActionResult SalvaUsuario(UsuarioModel model)
        {
            var retorno = _cadastroService.SalvaUsuario(model);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpDelete]
        [Route("api/Cadastro/ExcluiUsuario/{id}")]
        public IHttpActionResult ExcluiUsuario(int id)
        {
            var retorno = _cadastroService.ExcluiUsuario(id);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        #endregion

        #region Paciente

        [HttpGet]
        [Route("api/Cadastro/GetPaciente/{id}")]
        public IHttpActionResult GetPaciente(int id)
        {
            var retorno = _cadastroService.GetPaciente(id);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/Cadastro/ListPaciente")]
        public IHttpActionResult ListPaciente()
        {
            var retorno = _cadastroService.ListPaciente();

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpPost]
        [Route("api/Cadastro/SalvaPaciente")]
        public IHttpActionResult SalvaPaciente(PacienteModel model)
        {
            var retorno = _cadastroService.SalvaPaciente(model);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpDelete]
        [Route("api/Cadastro/ExcluiPaciente/{id}")]
        public IHttpActionResult ExcluiPaciente(int id)
        {
            var retorno = _cadastroService.ExcluiPaciente(id);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        #endregion

        #region Funcionario

        [HttpGet]
        [Route("api/Cadastro/GetFuncionario/{id}")]
        public IHttpActionResult GetFuncionario(int id)
        {
            var retorno = _cadastroService.GetFuncionario(id);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/Cadastro/ListFuncionario")]
        public IHttpActionResult ListFuncionario()
        {
            var retorno = _cadastroService.ListFuncionario();

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/Cadastro/ListProfissionais")]
        public IHttpActionResult ListProfissionais()
        {
            var retorno = _cadastroService.ListProfissionais();

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpPost]
        [Route("api/Cadastro/SalvaFuncionario")]
        public IHttpActionResult SalvaFuncionario(FuncionarioModel model)
        {
            var retorno = _cadastroService.SalvaFuncionario(model);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpDelete]
        [Route("api/Cadastro/ExcluiFuncionario/{id}")]
        public IHttpActionResult ExcluiFuncionario(int id)
        {
            var retorno = _cadastroService.ExcluiFuncionario(id);

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        #endregion
    }
}