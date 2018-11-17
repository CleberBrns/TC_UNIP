using System.Collections.Generic;
using TcUnip.Model.Cadastro;
using TcUnip.Model.Common;
using TcUnip.Web.Models.Proxy.Contract;
using TcUnip.Web.Session;

namespace TcUnip.Web.Util
{
    public class CommonSelectControls
    {        
        readonly ICommonProxy _commonProxy;

        public List<TipoPerfilModel> ListTipoPerfil()
        {
            SessionTipoPerfil sessionTipoPerfil = new SessionTipoPerfil();
            var list = _commonProxy.ListTipoPerfil().Value;
            sessionTipoPerfil.AddListToSession(list, Constants.ConstSessions.listTipoPerfil);
            return list;
        }

        public List<ModalidadeModel> ListModalidades()
        {
            SessionModalidades sessionModalidades = new SessionModalidades();
            var list = _commonProxy.ListModalidades().Value;
            sessionModalidades.AddListToSession(list, Constants.ConstSessions.listModalidades);
            return list;
        }
    }
}