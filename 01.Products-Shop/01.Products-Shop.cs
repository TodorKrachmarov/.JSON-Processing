namespace _01.Products_Shop
{
    using System;
    using System.Linq;
    using Data;
    using Newtonsoft.Json;
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            ProductsShopContext context = new ProductsShopContext();

            Console.WriteLine("1 :QueryOneProductsInRange");
            Console.WriteLine("2 :QueryTwoSuccessfullySoldProducts");
            Console.WriteLine("3 :QueryThreeCategoriesByProductsCount");
            Console.WriteLine("4 :QueryFourUsersAndProducts");
            Console.WriteLine("5 :Exit");
            Console.Write("Enter Number: ");
            int num = int.Parse(Console.ReadLine());
            while (num != 5)
            {
                switch (num)
                {
                    case 1:
                        QueryOneProductsInRange(context);
                        break;
                    case 2:
                        QueryTwoSuccessfullySoldProducts(context);
                        break;
                    case 3:
                        QueryThreeCategoriesByProductsCount(context);
                        break;
                    case 4:
                        QueryFourUsersAndProducts(context);
                        break;
                }
                Console.Clear();
                Console.WriteLine("Success");
                Console.WriteLine("1 :QueryOneProductsInRange");
                Console.WriteLine("2 :QueryTwoSuccessfullySoldProducts");
                Console.WriteLine("3 :QueryThreeCategoriesByProductsCount");
                Console.WriteLine("4 :QueryFourUsersAndProducts");
                Console.WriteLine("5 :Exit");
                Console.Write("Enter Number: ");
                num = int.Parse(Console.ReadLine());
            }
        }

        private static void QueryFourUsersAndProducts(ProductsShopContext context)
        {
            var users = context.Users.Where(u => u.ProductsSold.Count > 1).OrderByDescending(u => u.ProductsSold.Count).ThenBy(u => u.LastName)
                            .Select(u => new
                            {
                                firstName = u.FirstName,
                                lastName = u.LastName,
                                age = u.Age,
                                soldProducts = new
                                {
                                    count = u.ProductsSold.Count,
                                    products = u.ProductsSold.Select(p => new
                                    {
                                        name = p.Name,
                                        price = p.Price
                                    })
                                }
                            });

            string usersJson = JsonConvert.SerializeObject(new { usersCount = users.Count(), users = users }, Formatting.Indented);

            File.WriteAllText("../../users-and-products.json", usersJson);
        }

        private static void QueryThreeCategoriesByProductsCount(ProductsShopContext context)
        {
            var categories = context.Categories.OrderBy(c => c.Name).Select(c => new
            {
                category = c.Name,
                productsCount = c.Products.Count,
                averagePrice = c.Products.Average(p => p.Price),
                totalRevenue = c.Products.Sum(p => p.Price)
            });

            string categoriesJson = JsonConvert.SerializeObject(categories, Formatting.Indented);

            File.WriteAllText("../../categories-by-products.json", categoriesJson);
        }

        private static void QueryTwoSuccessfullySoldProducts(ProductsShopContext context)
        {
            var users = context.Users.Where(u => u.ProductsSold.Count > 0)
                .OrderBy(u => u.FirstName)
                .ThenBy(u => u.LastName)
                .Select(u => new
            {
                firstName = u.FirstName,
                lastName = u.LastName,
                soldProducts = u.ProductsSold.Where(p => p.BuyerId != null).Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    buyerFirstName = p.Buyer.FirstName,
                    buyerLastName = p.Buyer.LastName
                })
            });

            string usersJson = JsonConvert.SerializeObject(users, Formatting.Indented);

            File.WriteAllText("../../users-sold-products.json", usersJson);
        }

        private static void QueryOneProductsInRange(ProductsShopContext context)
        {
            var products = context.Products.Where(p => p.Price >= 500 && p.Price <= 1000).OrderBy(p => p.Price).Select(p => new
            {
                name = p.Name,
                price = p.Price,
                seller = p.Seller.FirstName + " " + p.Seller.LastName
            });

            string productsJson = JsonConvert.SerializeObject(products, Formatting.Indented);

            File.WriteAllText("../../products-in-range.json", productsJson);
        }
    }
}
