using TcUnip.Data.Entity.SeedItens;

namespace TcUnip.Data.Entity.Migrations
{
    public class Seed
    {
        public static void SeedTcUnip(TcUnipContext context)
        {
            ModalidadeSeed.Seed(context);
            TipoPerfilSeed.Seed(context);
            UsuarioSeed.Seed(context);
        }
    }
}
