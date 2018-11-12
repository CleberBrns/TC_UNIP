using System.Data.Entity.ModelConfiguration;
using TcUnip.Data.Entity.Modelagem.Cadastro;

namespace TcUnip.Data.Entity.Mapping.Modelagem.Cadastro
{
    public class ModalidadeFuncionarioMap : EntityTypeConfiguration<ModalidadeFuncionario>, IMapping
    {
        public ModalidadeFuncionarioMap()
        {
            ToTable("ModalidadeFuncionario", "tcUnip");

            HasKey(x => new { x.IdModalidade, x.IdFuncionario  });

            Property(x => x.IdModalidade)
               .IsRequired();

            Property(x => x.IdFuncionario)
                .IsRequired();
        }
    }
}
