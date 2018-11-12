using System.Data.Entity.ModelConfiguration;
using TcUnip.Data.Entity.Modelagem.Cadastro;

namespace TcUnip.Data.Entity.Mapping.Modelagem.Cadastro
{
    public class PacienteMap : EntityTypeConfiguration<Paciente>, IMapping
    {
        public PacienteMap()
        {
            ToTable("Paciente", "tcUnip");

            HasKey(x => x.Id);

            Property(x => x.IdPessoa)
               .IsRequired();

            Property(x => x.Ativo)
               .IsRequired();

            Property(x => x.Excluido)
                .IsRequired();
        }
    }
}
