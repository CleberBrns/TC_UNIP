using System.Data.Entity.ModelConfiguration;
using TcUnip.Data.Entity.Modelagem.Cadastro;

namespace TcUnip.Data.Entity.Mapping.Modelagem.Cadastro
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>, IMapping
    {
        public UsuarioMap()
        {
            ToTable("Usuario", "tcUnip");

            HasKey(x => x.Id);

            Property(x => x.Email)
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.Cpf)
                .HasMaxLength(15)
                .IsRequired();

            Property(x => x.Senha)
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.Ativo)
                .IsRequired();

            Property(x => x.Excluido)
                .IsRequired();

            Property(x => x.IdFuncionario)
                .IsOptional();

            HasOptional(x => x.Funcionario);

            Property(x => x.IdTipoPerfil)
                .IsRequired();
        }
    }
}
