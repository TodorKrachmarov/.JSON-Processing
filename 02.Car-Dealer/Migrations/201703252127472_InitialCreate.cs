namespace _02.Car_Dealer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        CarId = c.Int(nullable: false, identity: true),
                        Make = c.String(),
                        Model = c.String(),
                        TravelledDistance = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CarId);
            
            CreateTable(
                "dbo.Parts",
                c => new
                    {
                        PartId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        SupplierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PartId)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsImported = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SupplierId);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        CarId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        DiscountPercentage = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.SaleId)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CarId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        IsYoungDriver = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.PartCars",
                c => new
                    {
                        Part_PartId = c.Int(nullable: false),
                        Car_CarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Part_PartId, t.Car_CarId })
                .ForeignKey("dbo.Parts", t => t.Part_PartId, cascadeDelete: true)
                .ForeignKey("dbo.Cars", t => t.Car_CarId, cascadeDelete: true)
                .Index(t => t.Part_PartId)
                .Index(t => t.Car_CarId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Sales", "CarId", "dbo.Cars");
            DropForeignKey("dbo.Parts", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.PartCars", "Car_CarId", "dbo.Cars");
            DropForeignKey("dbo.PartCars", "Part_PartId", "dbo.Parts");
            DropIndex("dbo.PartCars", new[] { "Car_CarId" });
            DropIndex("dbo.PartCars", new[] { "Part_PartId" });
            DropIndex("dbo.Sales", new[] { "CustomerId" });
            DropIndex("dbo.Sales", new[] { "CarId" });
            DropIndex("dbo.Parts", new[] { "SupplierId" });
            DropTable("dbo.PartCars");
            DropTable("dbo.Customers");
            DropTable("dbo.Sales");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Parts");
            DropTable("dbo.Cars");
        }
    }
}
