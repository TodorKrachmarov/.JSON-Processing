namespace _02.Car_Dealer
{
    using System.Linq;
    using Data;
    using Newtonsoft.Json;
    using System.IO;
    using System.Threading;
    using System.Globalization;
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            CarDealerContext context = new CarDealerContext();

            Console.WriteLine("1 :QueryOneOrderedCustomers");
            Console.WriteLine("2 :QueryTwoCarsFromMakeToyota");
            Console.WriteLine("3 :QueryThreeLocalSuppliers");
            Console.WriteLine("4 :QueryFourCarsWithTheirListOfParts");
            Console.WriteLine("5 :QueryFiveTotalSalesByCustomer");
            Console.WriteLine("6 :QuerySixSalesWithAppliedDiscount");
            Console.WriteLine("7 :Exit");
            Console.Write("Enter number: ");
            int num = int.Parse(Console.ReadLine());
            while (num != 7)
            {
                switch (num)
                {
                    case 1:
                        QueryOneOrderedCustomers(context);
                        break;
                    case 2:
                        QueryTwoCarsFromMakeToyota(context);
                        break;
                    case 3:
                        QueryThreeLocalSuppliers(context);
                        break;
                    case 4:
                        QueryFourCarsWithTheirListOfParts(context);
                        break;
                    case 5:
                        QueryFiveTotalSalesByCustomer(context);
                        break;
                    case 6:
                        QuerySixSalesWithAppliedDiscount(context);
                        break;
                }
                Console.Clear();
                Console.WriteLine("Success");
                Console.WriteLine("1 :QueryOneOrderedCustomers");
                Console.WriteLine("2 :QueryTwoCarsFromMakeToyota");
                Console.WriteLine("3 :QueryThreeLocalSuppliers");
                Console.WriteLine("4 :QueryFourCarsWithTheirListOfParts");
                Console.WriteLine("5 :QueryFiveTotalSalesByCustomer");
                Console.WriteLine("6 :QuerySixSalesWithAppliedDiscount");
                Console.WriteLine("7 :Exit");
                Console.Write("Enter number: ");
                num = int.Parse(Console.ReadLine());
            }
        }

        private static void QuerySixSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales.Select(s => new
            {
                car = new
                {
                    Make = s.Car.Make,
                    Model = s.Car.Model,
                    TravelledDistance = s.Car.TravelledDistance
                },
                customerName = s.Customer.Name,
                Discount = Math.Round(s.DiscountPercentage / 100, 1),
                price = s.Car.Parts.Sum(p => p.Price),
                priceWithDiscount = (s.Car.Parts.Sum(p => p.Price) - (s.Car.Parts.Sum(p => p.Price) * s.DiscountPercentage / 100))
            });

            string salesJson = JsonConvert.SerializeObject(sales, Formatting.Indented);

            File.WriteAllText("../../sales-discounts.json", salesJson);
        }

        private static void QueryFiveTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers.Where(c => c.Sales.Count() >= 1).ToList().OrderByDescending(c => c.Sales.Sum(s => s.Car.Parts.Sum(p => p.Price)))
                            .ThenByDescending(c => c.Sales.Count).Select(c => new
                            {
                                fullName = c.Name,
                                boughtCars = c.Sales.Count,
                                spentMoney = Math.Round(c.Sales.Sum(s => s.Car.Parts.Sum(p => p.Price)), 2)
                            });

            string customersJson = JsonConvert.SerializeObject(customers, Formatting.Indented);

            File.WriteAllText("../../customers-total-sales.json", customersJson);
        }

        private static void QueryFourCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars.Select(c => new
            {
                car = new
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                },
                parts = c.Parts.Select(p => new
                {
                    Name = p.Name,
                    Price = p.Price
                })
            });

            string carsJson = JsonConvert.SerializeObject(cars, Formatting.Indented);

            File.WriteAllText("../../cars-and-parts.json", carsJson);
        }

        private static void QueryThreeLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers.Where(s => s.IsImporter == false).Select(c => new
            {
                Id = c.SupplierId,
                Name = c.Name,
                PartsCount = c.Parts.Count
            });

            string suppliersJson = JsonConvert.SerializeObject(suppliers, Formatting.Indented);

            File.WriteAllText("../../local-suppliers.json", suppliersJson);
        }

        private static void QueryTwoCarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context.Cars.Where(c => c.Make == "Toyota")
                            .OrderBy(c => c.Model).ThenByDescending(c => c.TravelledDistance).Select(c => new
                            {
                                Id = c.CarId,
                                Make = c.Make,
                                Model = c.Model,
                                TravelledDistance = c.TravelledDistance
                            });

            string carsJson = JsonConvert.SerializeObject(cars, Formatting.Indented);

            File.WriteAllText("../../toyota-cars.json", carsJson);
        }

        private static void QueryOneOrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers.OrderBy(c => new { c.BirthDate, c.IsYoungDriver })
                            .Select(c => new
                            {
                                Id = c.CustomerId,
                                Name = c.Name,
                                BirthDate = c.BirthDate,
                                IsYoungDriver = c.IsYoungDriver,
                                Sales = "[]"
                            });

            string customersJson = JsonConvert.SerializeObject(customers, Formatting.Indented);

            File.WriteAllText("../../ordered-customers.json", customersJson);
        }
    }
}
