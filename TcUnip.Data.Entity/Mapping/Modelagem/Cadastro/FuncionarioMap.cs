using System.Data.Entity.ModelConfiguration;
using TcUnip.Data.Entity.Modelagem.Cadastro;

namespace TcUnip.Data.Entity.Mapping.Modelagem.Cadastro
{
    public class FuncionarioMap : EntityTypeConfiguration<Funcionario>, IMapping
    {
        public FuncionarioMap()
        {
           ToTable("Funcionario", "tcUnip");

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
