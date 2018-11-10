using TcUnip.Data.Entity.SeedItens;

namespace TcUnip.Data.Entity.Migrations
{
    public class Seed
    {
        public static void SeedTcUnip(TcUnipContext context)
        {
            ModalidadeSeed.Seed(context);
            StatusSessaoSeed.Seed(context);
            var listTipoPerfil = TipoPerfilSeed.Seed(context);
            UsuarioSeed.Seed(context, listTipoPerfil);

        }
    }
}
