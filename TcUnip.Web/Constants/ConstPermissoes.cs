using TcUnip.Model.Pessoa;

namespace TcUnip.Web.Constants
{
    public class ConstPermissoes
    {
        public const string administracao = "ADMINISTRACAO";
        public const string recepcao = "RECEPCAO";
        public const string profissional = "PROFISSIONAL";
        public const string gerenciamento = "ADMINISTRACAO,RECEPCAO";

        public Usuario GetUsuarioMaster()
        {
            return new Usuario
            {
                Email = "master@tcunip.com.br",
                Senha = "@dmin56784321",
                Permissoes = new string[] { administracao },
                Funcionario = new Funcionario
                {
                    Nome = "Administrador do Sistema",
                    Status = "Ativo"                    
                }
            };
        }

    }    
}