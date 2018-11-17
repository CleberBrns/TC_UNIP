using System.Collections.Generic;
using TcUnip.Model.Cadastro;
using TcUnip.Model.Common;

namespace TcUnip.Service.Contract.Common
{
    public interface ICommonService
    {
        Result<List<TipoPerfilModel>> ListTipoPerfil();
        Result<List<ModalidadeModel>> ListModalidades();
    }
}
