namespace _02.Car_Dealer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeBirtDateToDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "DateOfBirth", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "DateOfBirth", c => c.String());
        }
    }
}
