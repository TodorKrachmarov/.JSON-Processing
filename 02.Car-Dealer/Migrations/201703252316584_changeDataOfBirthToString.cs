namespace _02.Car_Dealer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeDataOfBirthToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "DateOfBirth", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "DateOfBirth", c => c.DateTime(nullable: false));
        }
    }
}
