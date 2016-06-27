namespace SaoBei.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jogo", "GolsSaoBei", c => c.Int(nullable: false));
            AddColumn("dbo.Jogo", "GolsAdversario", c => c.Int(nullable: false));
            AddColumn("dbo.Jogo", "Resultado", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jogo", "Resultado");
            DropColumn("dbo.Jogo", "GolsAdversario");
            DropColumn("dbo.Jogo", "GolsSaoBei");
        }
    }
}
