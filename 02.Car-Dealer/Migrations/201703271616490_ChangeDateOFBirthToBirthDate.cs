namespace _02.Car_Dealer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDateOFBirthToBirthDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "BirthDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Customers", "DateOfBirth");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "DateOfBirth", c => c.DateTime(nullable: false));
            DropColumn("dbo.Customers", "BirthDate");
        }
    }
}
