namespace _02.Car_Dealer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTravelledDistanceToLong : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cars", "TravelledDistance", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cars", "TravelledDistance", c => c.String());
        }
    }
}
