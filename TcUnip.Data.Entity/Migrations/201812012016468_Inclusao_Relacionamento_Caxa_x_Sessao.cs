namespace TcUnip.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inclusao_Relacionamento_Caxa_x_Sessao : DbMigration
    {
        public override void Up()
        {
            AddColumn("tcUnip.Caixa", "IdSessao", c => c.Int());
            CreateIndex("tcUnip.Caixa", "IdSessao");
            AddForeignKey("tcUnip.Caixa", "IdSessao", "tcUnip.Sessao", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("tcUnip.Caixa", "IdSessao", "tcUnip.Sessao");
            DropIndex("tcUnip.Caixa", new[] { "IdSessao" });
            DropColumn("tcUnip.Caixa", "IdSessao");
        }
    }
}
