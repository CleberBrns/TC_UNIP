using System.Web.Http;
using TcUnip.AppApi.Results;
using TcUnip.Service.Contract.Common;

namespace TcUnip.AppApi.Controllers
{
    public class CommonController : ApiController
    {
        readonly ICommonService _commonService;
        public CommonController(ICommonService commonService)
        {
            this._commonService = commonService;
        }

        [HttpGet]
        [Route("api/Common/ListTipoPerfil")]
        public IHttpActionResult ListTipoPerfil()
        {
            var retorno = _commonService.ListTipoPerfil();

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("api/Common/ListModalidades")]
        public IHttpActionResult ListModalidades()
        {
            var retorno = _commonService.ListModalidades();

            if (!retorno.Status)
                return new InvalidListMessageResult(retorno.Message);

            return Ok(retorno);
        }
    }
}