namespace _02.Car_Dealer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTravellDistanceToString : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "TravelledDistance", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "TravelledDistance");
        }
    }
}
