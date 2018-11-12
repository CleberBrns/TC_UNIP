using System.Data.Entity.ModelConfiguration;
using TcUnip.Data.Entity.Modelagem.Agenda;

namespace TcUnip.Data.Entity.Mapping.Modelagem.Agenda
{
    public class StatusSessaoMap : EntityTypeConfiguration<StatusSessao>, IMapping
    {
        public StatusSessaoMap()
        {
            ToTable("StatusSessao", "tcUnip");

            HasKey(x => x.Id);

            Property(x => x.Descricao)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
