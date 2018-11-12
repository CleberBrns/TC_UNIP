using System.Data.Entity.ModelConfiguration;
using TcUnip.Data.Entity.Modelagem.Cadastro;

namespace TcUnip.Data.Entity.Mapping.Modelagem.Cadastro
{
    public class TipoPerfilMap : EntityTypeConfiguration<TipoPerfil>, IMapping
    {
        public TipoPerfilMap()
        {
            ToTable("TipoPerfil", "tcUnip");

            HasKey(x => x.Id);

            Property(x => x.Tipo)
                .HasMaxLength(20)
                .IsRequired();

            Property(x => x.Permissao)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
