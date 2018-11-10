namespace TcUnip.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TcUnip.Data.Entity.TcUnipContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetHistoryContextFactory("System.Data.SqlClient", (connection, defaultSchema) => new TcUnipHistoryContext(connection, defaultSchema));
        }

        protected override void Seed(TcUnip.Data.Entity.TcUnipContext context)
        {
            //  This method will be called after migrating to the latest version.
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            Migrations.Seed.SeedTcUnip(context);

        }
    }
}
