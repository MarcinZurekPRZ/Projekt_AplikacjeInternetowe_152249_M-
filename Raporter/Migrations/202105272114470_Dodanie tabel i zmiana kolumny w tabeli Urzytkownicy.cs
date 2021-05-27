namespace Raporter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodanietabelizmianakolumnywtabeliUrzytkownicy : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Funkcjes",
                c => new
                    {
                        FunkcjeID = c.Int(nullable: false, identity: true),
                        NazwaFunkcji = c.String(),
                    })
                .PrimaryKey(t => t.FunkcjeID);
            
            CreateTable(
                "dbo.Raporties",
                c => new
                    {
                        RaportyID = c.Int(nullable: false, identity: true),
                        DataRaportu = c.DateTime(nullable: false),
                        Temat = c.String(),
                        Tresc = c.String(),
                        UzytkownicyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RaportyID)
                .ForeignKey("dbo.Uzytkownicies", t => t.UzytkownicyID, cascadeDelete: true)
                .Index(t => t.UzytkownicyID);
            
            AddColumn("dbo.Uzytkownicies", "FunkcjeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Uzytkownicies", "FunkcjeID");
            AddForeignKey("dbo.Uzytkownicies", "FunkcjeID", "dbo.Funkcjes", "FunkcjeID", cascadeDelete: true);
            DropColumn("dbo.Uzytkownicies", "ID_funkcji");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Uzytkownicies", "ID_funkcji", c => c.Int(nullable: false));
            DropForeignKey("dbo.Raporties", "UzytkownicyID", "dbo.Uzytkownicies");
            DropForeignKey("dbo.Uzytkownicies", "FunkcjeID", "dbo.Funkcjes");
            DropIndex("dbo.Raporties", new[] { "UzytkownicyID" });
            DropIndex("dbo.Uzytkownicies", new[] { "FunkcjeID" });
            DropColumn("dbo.Uzytkownicies", "FunkcjeID");
            DropTable("dbo.Raporties");
            DropTable("dbo.Funkcjes");
        }
    }
}
