namespace _02.Car_Dealer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using Models;
    using System.Threading;
    using System.Globalization;

    internal sealed class Configuration : DbMigrationsConfiguration<_02.Car_Dealer.Data.CarDealerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "_02.Car_Dealer.Data.CarDealerContext";
        }

        protected override void Seed(_02.Car_Dealer.Data.CarDealerContext context)
        {
            ImportSuppliers(context);
            ImportParts(context);
            ImportCars(context);
            ImportCustomers(context);
            ImportSales(context);
        }

        private static void ImportSales(Data.CarDealerContext context)
        {
            int num = 0;
            int customersCount = context.Customers.Count();
            int carsCaunt = context.Cars.Count();
            for (int i = 1; i <= 100; i++)
            {
                Sale sale = new Sale();
                sale.CarId = (num % carsCaunt) + 1;
                sale.CustomerId = (num % customersCount) + 1;
                if (i % 2 == 0)
                {
                    sale.DiscountPercentage = 15;
                }
                else
                {
                    sale.DiscountPercentage = 30;
                }

                context.Sales.AddOrUpdate(s => new { s.CarId, s.CustomerId }, sale);
                context.SaveChanges();
                num++;
            }
        }

        private static void ImportCustomers(Data.CarDealerContext context)
        {

            //Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            var customersJson = File.ReadAllText(@"F:\SoftUni\Databases Advanced - Entity Framework\10.JSON Processing\02.Car-Dealer\Import\customers.json");

            List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(customersJson);

            foreach (var cus in customers)
            {
                context.Customers.AddOrUpdate(c => c.Name, cus);
            }

            context.SaveChanges();
        }

        private static void ImportCars(Data.CarDealerContext context)
        {
            var carsJson = File.ReadAllText(@"F:\SoftUni\Databases Advanced - Entity Framework\10.JSON Processing\02.Car-Dealer\Import\cars.json");

            List<Car> cars = JsonConvert.DeserializeObject<List<Car>>(carsJson);

            int num = 0;
            int num1 = 1;
            int partsCount = context.Parts.Count();
            foreach (var ca in cars)
            {
                List<Part> parts = new List<Part>();
                for (int i = 0; i < 20; i++)
                {
                    Part part = context.Parts.Find((num % partsCount) + 1);
                    parts.Add(part);
                    num++;
                }
                ca.Parts = parts;
                ca.CarId = num1;
                num1++;
                context.Cars.AddOrUpdate(c => c.CarId, ca);
            }

            context.SaveChanges();
        }

        private static void ImportParts(Data.CarDealerContext context)
        {
            var partsJson = File.ReadAllText(@"F:\SoftUni\Databases Advanced - Entity Framework\10.JSON Processing\02.Car-Dealer\Import\parts.json");

            List<Part> parts = JsonConvert.DeserializeObject<List<Part>>(partsJson);

            int num = 0;
            int suppliersCount = context.Suppliers.Count();
            foreach (var pa in parts)
            {
                pa.SupplierId = (num % suppliersCount) + 1;
                num++;
                context.Parts.AddOrUpdate(p => new { p.Name, p.Price }, pa);
            }

            context.SaveChanges();
        }

        private static void ImportSuppliers(Data.CarDealerContext context)
        {
            var suppliersJson = File.ReadAllText(@"F:\SoftUni\Databases Advanced - Entity Framework\10.JSON Processing\02.Car-Dealer\Import\suppliers.json");

            List<Supplier> suppliers = JsonConvert.DeserializeObject<List<Supplier>>(suppliersJson);

            foreach (var su in suppliers)
            {
                context.Suppliers.AddOrUpdate(s => s.Name, su);
            }

            context.SaveChanges();
        }
    }
}
