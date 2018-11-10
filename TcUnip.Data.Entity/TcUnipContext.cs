using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using TcUnip.Data.Entity.Mapping;
using TcUnip.Data.Entity.Modelagem.Agenda;
using TcUnip.Data.Entity.Modelagem.Cadastro;
using TcUnip.Data.Entity.Modelagem.FluxoCaixa;

namespace TcUnip.Data.Entity
{
    public class TcUnipContext : DbContext
    {
        public TcUnipContext() : base("tcUnipConn")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        #region Tabelas

        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Funcionario> Funcionario { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Modalidade> Modalidade { get; set; }
        public DbSet<Sessao> Sessao { get; set; }
        public DbSet<StatusSessao> StatusSessao { get; set; }

        public DbSet<Caixa> Caixa { get; set; }

        #endregion

        #region Configurações Padrões

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Altera as configurações padrões do Entity ao criar as tabelas
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties().Where(p => p.Name == "Id").Configure(x => x.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity));
            modelBuilder.Properties<string>().Configure(p => p.HasColumnType("varchar"));
            modelBuilder.Properties<string>().Configure(p => p.HasMaxLength(100));
            modelBuilder.Properties<DateTime>().Configure(p => p.HasColumnType("datetime2"));


            //Recuperando todas as classes que herdam da IMapping
            var typesToMapping = (from x in Assembly.GetExecutingAssembly().GetTypes()
                                  where x.IsClass && typeof(IMapping).IsAssignableFrom(x)
                                  select x).ToList();

            foreach (var type in typesToMapping)
            {
                dynamic mappingClass = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(mappingClass);
            }

            base.OnModelCreating(modelBuilder);
        }

        public void ChangeObjectState(object model, EntityState state)
        {
            //Trocando o estado de um objeto
            ((IObjectContextAdapter)this)
                .ObjectContext
                .ObjectStateManager
                .ChangeObjectState(model, state);
        }

        #endregion
    }
}
