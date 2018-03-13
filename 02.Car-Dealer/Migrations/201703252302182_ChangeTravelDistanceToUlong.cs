namespace _02.Car_Dealer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTravelDistanceToUlong : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Cars", "TravelledDistance");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cars", "TravelledDistance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
