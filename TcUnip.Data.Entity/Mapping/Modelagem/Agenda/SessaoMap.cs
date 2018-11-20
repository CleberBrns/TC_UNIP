using System.Data.Entity.ModelConfiguration;
using TcUnip.Data.Entity.Modelagem.Agenda;

namespace TcUnip.Data.Entity.Mapping.Modelagem.Agenda
{
    public class SessaoMap : EntityTypeConfiguration<Sessao>, IMapping
    {
        public SessaoMap()
        {
            ToTable("Sessao", "tcUnip");

            HasKey(x => x.Id);

            Property(x => x.Valor)
                .IsRequired();

            Property(x => x.Data)
                .IsRequired();

            Property(x => x.Excluido)
                .IsRequired();

            Property(x => x.IdPaciente)
                .IsRequired();

            Property(x => x.IdFuncionario)
                .IsRequired();

            Property(x => x.IdModalidade)
                .IsRequired();            
        }
    }
}
