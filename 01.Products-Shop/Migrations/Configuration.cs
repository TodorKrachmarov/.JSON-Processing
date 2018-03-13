namespace _01.Products_Shop.Migrations
{
    using Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<_01.Products_Shop.Data.ProductsShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "_01.Products_Shop.Data.ProductsShopContext";
        }

        protected override void Seed(_01.Products_Shop.Data.ProductsShopContext context)
        {
            InsertUsers(context);
            InsertProducts(context);
            InsertCategories(context);
        }

        private static void InsertCategories(Data.ProductsShopContext context)
        {
            string categoryJson = File.ReadAllText(@"F:\SoftUni\Databases Advanced - Entity Framework\10.JSON Processing\01.Products-Shop\Import\categories.json");

            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(categoryJson);
            int num1 = 1;
            int num2 = 18;
            foreach (var cat in categories)
            {
                var products = context.Products.Where(p => p.Id >= num1 && p.Id <= num2);
                foreach (var p in products)
                {
                    cat.Products.Add(p);
                }

                context.Categories.AddOrUpdate(c => c.Name, cat);
                num1 += 18;
                num2 += 18;
            }
            

            context.SaveChanges();
        }

        private static void InsertProducts(Data.ProductsShopContext context)
        {
            string productJson = File.ReadAllText(@"F:\SoftUni\Databases Advanced - Entity Framework\10.JSON Processing\01.Products-Shop\Import\products.json");

            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(productJson);

            int num = 0;
            int userCount = context.Users.Count();
            foreach (var p in products)
            {
                p.SellerId = (num % userCount) + 1;
                num++;
                if (num % 3 != 0)
                {
                    p.BuyerId = (num * 2 % userCount) + 1;
                }

                context.Products.AddOrUpdate(pr => new { pr.Price, pr.Name}, p);
            }

            context.SaveChanges();
        }

        private static void InsertUsers(Data.ProductsShopContext context)
        {
            string userJason = File.ReadAllText(@"F:\SoftUni\Databases Advanced - Entity Framework\10.JSON Processing\01.Products-Shop\Import\users.json");

            List<User> users = JsonConvert.DeserializeObject<List<User>>(userJason);

            foreach (var us in users)
            {
                context.Users.AddOrUpdate(u => new { u.FirstName, u.LastName }, us);
            }

            context.SaveChanges();
        }
    }
}
