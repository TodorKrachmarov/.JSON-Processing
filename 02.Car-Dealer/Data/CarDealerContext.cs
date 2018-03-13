namespace _02.Car_Dealer.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Models;
    using Migrations;

    public class CarDealerContext : DbContext
    {
        public CarDealerContext()
            : base("name=CarDealerContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CarDealerContext, Configuration>());
        }
         public virtual DbSet<Car> Cars { get; set; }
         public virtual DbSet<Part> Parts { get; set; }
         public virtual DbSet<Supplier> Suppliers { get; set; }
         public virtual DbSet<Customer> Customers { get; set; }
         public virtual DbSet<Sale> Sales { get; set; }
    }
}