using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations.History;

namespace TcUnip.Data.Entity.Migrations
{
    public class TcUnipHistoryContext : HistoryContext
    {
        public TcUnipHistoryContext(DbConnection dbConnection, string defaultSchema)
            : base(dbConnection, defaultSchema)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<HistoryRow>().ToTable(tableName: "_MigrationHistory", schemaName: "tcUnip");
            modelBuilder.Entity<HistoryRow>().Property(p => p.MigrationId).HasColumnName("Migration_ID");
        }
    }
}
