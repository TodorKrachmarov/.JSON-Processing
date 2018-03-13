namespace _02.Car_Dealer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeIsImportedToIsImporter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Suppliers", "IsImporter", c => c.Boolean(nullable: false));
            DropColumn("dbo.Suppliers", "IsImported");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Suppliers", "IsImported", c => c.Boolean(nullable: false));
            DropColumn("dbo.Suppliers", "IsImporter");
        }
    }
}
