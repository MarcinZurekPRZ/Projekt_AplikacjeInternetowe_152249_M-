namespace Raporter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Oddzialies",
                c => new
                    {
                        OddzialyID = c.Int(nullable: false, identity: true),
                        NazwaOddzialu = c.String(),
                        SymbolOddzialu = c.String(),
                    })
                .PrimaryKey(t => t.OddzialyID);
            
            CreateTable(
                "dbo.Uzytkownicies",
                c => new
                    {
                        UzytkownicyID = c.Int(nullable: false, identity: true),
                        Imie = c.String(),
                        Nazwisko = c.String(),
                        OddzialyID = c.Int(nullable: false),
                        ID_funkcji = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UzytkownicyID)
                .ForeignKey("dbo.Oddzialies", t => t.OddzialyID, cascadeDelete: true)
                .Index(t => t.OddzialyID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Uzytkownicies", "OddzialyID", "dbo.Oddzialies");
            DropIndex("dbo.Uzytkownicies", new[] { "OddzialyID" });
            DropTable("dbo.Uzytkownicies");
            DropTable("dbo.Oddzialies");
        }
    }
}
