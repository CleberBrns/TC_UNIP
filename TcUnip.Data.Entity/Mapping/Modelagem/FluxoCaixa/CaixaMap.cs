using System.Data.Entity.ModelConfiguration;
using TcUnip.Data.Entity.Modelagem.FluxoCaixa;

namespace TcUnip.Data.Entity.Mapping.Modelagem.FluxoCaixa
{
    public class CaixaMap : EntityTypeConfiguration<Caixa>, IMapping
    {
        public CaixaMap()
        {
            ToTable("Caixa", "tcUnip");

            HasKey(x => x.Id);

            Property(x => x.Data)
                .IsRequired();

            Property(x => x.Descricao)
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.Credito)
                .IsRequired();

            Property(x => x.Debito)
                .IsRequired();
        }
    }
}
