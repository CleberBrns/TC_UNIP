using TC_Clinica_Gerenciamento.Models.Local;

namespace TC_Clinica_Gerenciamento.Util
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