namespace TcUnip.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "tcUnip.Caixa",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Descricao = c.String(nullable: false, maxLength: 100, unicode: false),
                        Debito = c.Decimal(nullable: false, storeType: "money"),
                        Credito = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "tcUnip.Funcionario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ativo = c.Boolean(nullable: false),
                        Excluido = c.Boolean(nullable: false),
                        IdPessoa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("tcUnip.Pessoa", t => t.IdPessoa)
                .Index(t => t.IdPessoa);
            
            CreateTable(
                "tcUnip.ModalidadeFuncionario",
                c => new
                    {
                        IdModalidade = c.Int(nullable: false),
                        IdFuncionario = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdModalidade, t.IdFuncionario })
                .ForeignKey("tcUnip.Funcionario", t => t.IdFuncionario)
                .ForeignKey("tcUnip.Modalidade", t => t.IdModalidade)
                .Index(t => t.IdModalidade)
                .Index(t => t.IdFuncionario);
            
            CreateTable(
                "tcUnip.Modalidade",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "tcUnip.Pessoa",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        Cpf = c.String(nullable: false, maxLength: 15, unicode: false),
                        Email = c.String(nullable: false, maxLength: 100, unicode: false),
                        Telefone = c.String(maxLength: 20, unicode: false),
                        Logradouro = c.String(maxLength: 500, unicode: false),
                        Cep = c.String(maxLength: 10, unicode: false),
                        Complemento = c.String(maxLength: 500, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        Excluido = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "tcUnip.Paciente",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ativo = c.Boolean(nullable: false),
                        Excluido = c.Boolean(nullable: false),
                        IdPessoa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("tcUnip.Pessoa", t => t.IdPessoa)
                .Index(t => t.IdPessoa);
            
            CreateTable(
                "tcUnip.Sessao",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Valor = c.Decimal(nullable: false, storeType: "money"),
                        Data = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Excluido = c.Boolean(nullable: false),
                        IdPaciente = c.Int(nullable: false),
                        IdFuncioario = c.Int(nullable: false),
                        IdModalidade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("tcUnip.Funcionario", t => t.IdFuncioario)
                .ForeignKey("tcUnip.Modalidade", t => t.IdModalidade)
                .ForeignKey("tcUnip.Paciente", t => t.IdPaciente)
                .Index(t => t.IdPaciente)
                .Index(t => t.IdFuncioario)
                .Index(t => t.IdModalidade);
            
            CreateTable(
                "tcUnip.TipoPerfil",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tipo = c.String(nullable: false, maxLength: 20, unicode: false),
                        Permissao = c.String(nullable: false, maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "tcUnip.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 100, unicode: false),
                        Cpf = c.String(nullable: false, maxLength: 15, unicode: false),
                        Senha = c.String(nullable: false, maxLength: 100, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        Excluido = c.Boolean(nullable: false),
                        IdFuncionario = c.Int(),
                        IdTipoPerfil = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("tcUnip.Funcionario", t => t.IdFuncionario)
                .ForeignKey("tcUnip.TipoPerfil", t => t.IdTipoPerfil)
                .Index(t => t.IdFuncionario)
                .Index(t => t.IdTipoPerfil);
            
        }
        
        public override void Down()
        {
            DropForeignKey("tcUnip.Usuario", "IdTipoPerfil", "tcUnip.TipoPerfil");
            DropForeignKey("tcUnip.Usuario", "IdFuncionario", "tcUnip.Funcionario");
            DropForeignKey("tcUnip.Sessao", "IdPaciente", "tcUnip.Paciente");
            DropForeignKey("tcUnip.Sessao", "IdModalidade", "tcUnip.Modalidade");
            DropForeignKey("tcUnip.Sessao", "IdFuncioario", "tcUnip.Funcionario");
            DropForeignKey("tcUnip.Paciente", "IdPessoa", "tcUnip.Pessoa");
            DropForeignKey("tcUnip.Funcionario", "IdPessoa", "tcUnip.Pessoa");
            DropForeignKey("tcUnip.ModalidadeFuncionario", "IdModalidade", "tcUnip.Modalidade");
            DropForeignKey("tcUnip.ModalidadeFuncionario", "IdFuncionario", "tcUnip.Funcionario");
            DropIndex("tcUnip.Usuario", new[] { "IdTipoPerfil" });
            DropIndex("tcUnip.Usuario", new[] { "IdFuncionario" });
            DropIndex("tcUnip.Sessao", new[] { "IdModalidade" });
            DropIndex("tcUnip.Sessao", new[] { "IdFuncioario" });
            DropIndex("tcUnip.Sessao", new[] { "IdPaciente" });
            DropIndex("tcUnip.Paciente", new[] { "IdPessoa" });
            DropIndex("tcUnip.ModalidadeFuncionario", new[] { "IdFuncionario" });
            DropIndex("tcUnip.ModalidadeFuncionario", new[] { "IdModalidade" });
            DropIndex("tcUnip.Funcionario", new[] { "IdPessoa" });
            DropTable("tcUnip.Usuario");
            DropTable("tcUnip.TipoPerfil");
            DropTable("tcUnip.Sessao");
            DropTable("tcUnip.Paciente");
            DropTable("tcUnip.Pessoa");
            DropTable("tcUnip.Modalidade");
            DropTable("tcUnip.ModalidadeFuncionario");
            DropTable("tcUnip.Funcionario");
            DropTable("tcUnip.Caixa");
        }
    }
}
