using TCC_Unip.Models.Local;

namespace TCC_Unip.Util
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