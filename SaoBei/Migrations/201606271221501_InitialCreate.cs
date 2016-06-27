namespace SaoBei.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adversario",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        NomeContato = c.String(),
                        Telefone = c.String(),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Calendario",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Ano = c.Int(nullable: false),
                        DataVencimentoAnuidade = c.DateTime(nullable: false),
                        Janeiro = c.Boolean(nullable: false),
                        Fevereiro = c.Boolean(nullable: false),
                        Marco = c.Boolean(nullable: false),
                        Abril = c.Boolean(nullable: false),
                        Maio = c.Boolean(nullable: false),
                        Junho = c.Boolean(nullable: false),
                        Julho = c.Boolean(nullable: false),
                        Agosto = c.Boolean(nullable: false),
                        Setembro = c.Boolean(nullable: false),
                        Outubro = c.Boolean(nullable: false),
                        Novembro = c.Boolean(nullable: false),
                        Dezembro = c.Boolean(nullable: false),
                        ValorMensalidade = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorAnuidade = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Integrante",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Telefone = c.String(),
                        DataNascimento = c.DateTime(nullable: false),
                        Email = c.String(),
                        TipoIntegrante = c.Int(nullable: false),
                        Senha = c.String(maxLength: 20),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MensalidadeIntegrante",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Mes = c.Int(nullable: false),
                        CalendarioID = c.Int(nullable: false),
                        IntegranteID = c.Int(nullable: false),
                        DataPagamento = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Calendario", t => t.CalendarioID)
                .ForeignKey("dbo.Integrante", t => t.IntegranteID)
                .Index(t => t.CalendarioID)
                .Index(t => t.IntegranteID);
            
            CreateTable(
                "dbo.IntegranteJogo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IntegranteID = c.Int(nullable: false),
                        JogoID = c.Int(nullable: false),
                        Gols = c.Int(),
                        Assistencias = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Integrante", t => t.IntegranteID)
                .ForeignKey("dbo.Jogo", t => t.JogoID)
                .Index(t => t.IntegranteID)
                .Index(t => t.JogoID);
            
            CreateTable(
                "dbo.Jogo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        Hora = c.DateTime(nullable: false),
                        AdversarioID = c.Int(nullable: false),
                        CalendarioID = c.Int(nullable: false),
                        SituacaoJogo = c.Int(nullable: false),
                        MotivoCancelamento = c.String(),
                        LocalJogoID = c.Int(nullable: false),
                        DestaqueID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Adversario", t => t.AdversarioID)
                .ForeignKey("dbo.Calendario", t => t.CalendarioID)
                .ForeignKey("dbo.Integrante", t => t.DestaqueID)
                .ForeignKey("dbo.LocalJogo", t => t.LocalJogoID)
                .Index(t => t.AdversarioID)
                .Index(t => t.CalendarioID)
                .Index(t => t.LocalJogoID)
                .Index(t => t.DestaqueID);
            
            CreateTable(
                "dbo.LocalJogo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 300),
                        ValorJogo = c.Decimal(precision: 18, scale: 2),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Log",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TipoLog = c.Int(nullable: false),
                        Mensagem = c.String(nullable: false),
                        Observacao = c.String(),
                        Usuario = c.String(),
                        DataHora = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IntegranteJogo", "JogoID", "dbo.Jogo");
            DropForeignKey("dbo.Jogo", "LocalJogoID", "dbo.LocalJogo");
            DropForeignKey("dbo.Jogo", "DestaqueID", "dbo.Integrante");
            DropForeignKey("dbo.Jogo", "CalendarioID", "dbo.Calendario");
            DropForeignKey("dbo.Jogo", "AdversarioID", "dbo.Adversario");
            DropForeignKey("dbo.IntegranteJogo", "IntegranteID", "dbo.Integrante");
            DropForeignKey("dbo.MensalidadeIntegrante", "IntegranteID", "dbo.Integrante");
            DropForeignKey("dbo.MensalidadeIntegrante", "CalendarioID", "dbo.Calendario");
            DropIndex("dbo.Jogo", new[] { "DestaqueID" });
            DropIndex("dbo.Jogo", new[] { "LocalJogoID" });
            DropIndex("dbo.Jogo", new[] { "CalendarioID" });
            DropIndex("dbo.Jogo", new[] { "AdversarioID" });
            DropIndex("dbo.IntegranteJogo", new[] { "JogoID" });
            DropIndex("dbo.IntegranteJogo", new[] { "IntegranteID" });
            DropIndex("dbo.MensalidadeIntegrante", new[] { "IntegranteID" });
            DropIndex("dbo.MensalidadeIntegrante", new[] { "CalendarioID" });
            DropTable("dbo.Log");
            DropTable("dbo.LocalJogo");
            DropTable("dbo.Jogo");
            DropTable("dbo.IntegranteJogo");
            DropTable("dbo.MensalidadeIntegrante");
            DropTable("dbo.Integrante");
            DropTable("dbo.Calendario");
            DropTable("dbo.Adversario");
        }
    }
}
