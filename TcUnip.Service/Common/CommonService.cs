using System.Collections.Generic;
using System.Linq;
using TcUnip.Data.Contract.Cadastro;
using TcUnip.Data.Contract.Common;
using TcUnip.Model.Cadastro;
using TcUnip.Model.Common;
using TcUnip.Service.Contract.Common;

namespace TcUnip.Service.Common
{
    public class CommonService : ICommonService
    {
        #region Propriedades e Construtor

        readonly IModalidadeRepository _modalidadeRepository;
        readonly ITipoPerfilRepository _tipoPerfilRepository;


        public CommonService(IModalidadeRepository modalidadeRepository, ITipoPerfilRepository tipoPerfilRepository)
        {
            this._modalidadeRepository = modalidadeRepository;
            this._tipoPerfilRepository = tipoPerfilRepository;
        }

        #endregion

        public Result<List<ModalidadeModel>> ListModalidades()
        {
            var result = new Result<List<ModalidadeModel>>();
            result.Value = _modalidadeRepository.Lista().ToList();            

            return result;
        }

        public Result<List<TipoPerfilModel>> ListTipoPerfil()
        {
            var result = new Result<List<TipoPerfilModel>>();
            result.Value = _tipoPerfilRepository.Lista().ToList();

            return result;
        }
    }
}
