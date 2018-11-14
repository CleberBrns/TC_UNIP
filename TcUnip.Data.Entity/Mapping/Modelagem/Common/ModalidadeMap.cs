using System.Data.Entity.ModelConfiguration;
using TcUnip.Data.Entity.Modelagem.Common;

namespace TcUnip.Data.Entity.Mapping.Modelagem.Common
{
    public class ModalidadeMap : EntityTypeConfiguration<Modalidade>, IMapping
    {
        public ModalidadeMap()
        {
            ToTable("Modalidade", "tcUnip");

            HasKey(x => x.Id);

            Property(x => x.Nome)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
