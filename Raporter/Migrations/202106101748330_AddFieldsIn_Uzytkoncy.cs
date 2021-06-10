namespace Raporter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldsIn_Uzytkoncy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Uzytkownicies", "Haslo", c => c.String());
            AddColumn("dbo.Uzytkownicies", "Login", c => c.String(maxLength: 20));
            CreateIndex("dbo.Uzytkownicies", "Login", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Uzytkownicies", new[] { "Login" });
            DropColumn("dbo.Uzytkownicies", "Login");
            DropColumn("dbo.Uzytkownicies", "Haslo");
        }
    }
}
