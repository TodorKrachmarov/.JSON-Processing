namespace _02.Car_Dealer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDiscountToDecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sales", "DiscountPercentage", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sales", "DiscountPercentage", c => c.Double(nullable: false));
        }
    }
}
