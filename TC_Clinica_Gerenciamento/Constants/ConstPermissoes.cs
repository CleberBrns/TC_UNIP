using TCC_Unip.Models.Servico;

namespace TCC_Unip.Constants
{
    public class ConstPermissoes
    {
        public const string administracao = "ADMINISTRACAO";
        public const string recepcao = "RECEPCAO";
        public const string profissional = "PROFISSIONAL";

        public Usuario GetUsuarioMaster()
        {
            return new Usuario
            {
                Email = "master@tcunip.com.br",
                Senha = "@dmin56784321",
                Permissoes = new string[] { administracao }
            };
        }

    }    
}