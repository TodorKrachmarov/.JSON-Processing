namespace _02.Car_Dealer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTravelDistanceFromIntToDecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cars", "TravelledDistance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cars", "TravelledDistance", c => c.Int(nullable: false));
        }
    }
}
