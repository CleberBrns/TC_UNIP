using TcUnip.Web.Models.Local;

namespace TcUnip.Web.Util
{
    public class Mensagens
    {        
        public MensagensDeRetorno ConfiguraMensagemRetorno(string msgExibicao, string msgAnalise)
        {
            return new MensagensDeRetorno
            {
                MensagemExibicao = msgExibicao,
                MensagemAnalise = msgAnalise,
                Status = string.IsNullOrEmpty(msgAnalise)
            };
        }        
    }
}