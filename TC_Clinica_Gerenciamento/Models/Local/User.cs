
namespace TC_Clinica_Gerenciamento.Models.Local
{
    public class User
    {
        public string Login { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public UserType TipoUsuario { get; set; }
    }  
}