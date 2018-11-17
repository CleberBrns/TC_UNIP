using System.Data.Entity.ModelConfiguration;
using TcUnip.Data.Entity.Modelagem.Cadastro;

namespace TcUnip.Data.Entity.Mapping.Modelagem.Cadastro
{
    public class PessoaMap : EntityTypeConfiguration<Pessoa>, IMapping
    {
        public PessoaMap()
        {
            ToTable("Pessoa", "tcUnip");

            HasKey(x => x.Id);

            Property(x => x.Nome)
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.Email)
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.Cpf)
                .HasMaxLength(15)
                .IsRequired();

            Property(x => x.Telefone)
                .HasMaxLength(20);                

            Property(x => x.Logradouro)
                .HasMaxLength(500);

            Property(x => x.Cep)
                .HasMaxLength(10);

            Property(x => x.Complemento)
                .HasMaxLength(500);

            Property(x => x.Ativo)
                .IsRequired();

            Property(x => x.Excluido)
                .IsRequired();
        }
    }
}
