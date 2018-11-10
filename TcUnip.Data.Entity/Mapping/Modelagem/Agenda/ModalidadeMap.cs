using System.Data.Entity.ModelConfiguration;
using TcUnip.Data.Entity.Modelagem.Agenda;

namespace TcUnip.Data.Entity.Mapping.Modelagem.Agenda
{
    public class ModalidadeMap : EntityTypeConfiguration<Modalidade>, IMapping
    {
        public ModalidadeMap()
        {
            ToTable("Modalidades", "tcUnip");

            HasKey(x => x.Id);

            Property(x => x.Nome)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
