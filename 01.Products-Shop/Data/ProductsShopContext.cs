namespace _01.Products_Shop.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Models;
    using Migrations;

    public class ProductsShopContext : DbContext
    {
        public ProductsShopContext()
            : base("name=ProductsShopContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ProductsShopContext, Configuration>());

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<UserFriend> UserFriends { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserFriend>()
                .HasRequired(u => u.Friend).WithMany()
                .HasForeignKey(i => i.FriendId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasOptional(u => u.Buyer)
                .WithMany(p => p.ProductsBought)
                .HasForeignKey(i => i.BuyerId);

            modelBuilder.Entity<Product>()
                .HasRequired(u => u.Seller)
                .WithMany(p => p.ProductsSold)
                .HasForeignKey(i => i.SellerId);

            base.OnModelCreating(modelBuilder); 
        }
    }
   
}