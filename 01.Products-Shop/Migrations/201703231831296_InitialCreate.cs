namespace _01.Products_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BuyerId = c.Int(),
                        SellerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.BuyerId)
                .ForeignKey("dbo.Users", t => t.SellerId, cascadeDelete: true)
                .Index(t => t.BuyerId)
                .Index(t => t.SellerId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(nullable: false),
                        Age = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserFriends",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        FriendId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.FriendId })
                .ForeignKey("dbo.Users", t => t.FriendId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.FriendId);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Product_Id = c.Int(nullable: false),
                        Category_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.Category_Id })
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.Product_Id)
                .Index(t => t.Category_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "SellerId", "dbo.Users");
            DropForeignKey("dbo.ProductCategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.ProductCategories", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "BuyerId", "dbo.Users");
            DropForeignKey("dbo.UserFriends", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserFriends", "FriendId", "dbo.Users");
            DropIndex("dbo.ProductCategories", new[] { "Category_Id" });
            DropIndex("dbo.ProductCategories", new[] { "Product_Id" });
            DropIndex("dbo.UserFriends", new[] { "FriendId" });
            DropIndex("dbo.UserFriends", new[] { "UserId" });
            DropIndex("dbo.Products", new[] { "SellerId" });
            DropIndex("dbo.Products", new[] { "BuyerId" });
            DropTable("dbo.ProductCategories");
            DropTable("dbo.UserFriends");
            DropTable("dbo.Users");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
