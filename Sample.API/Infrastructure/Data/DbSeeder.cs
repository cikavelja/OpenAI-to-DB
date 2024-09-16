using Sample.API.Models;

namespace Sample.API.Infrastructure.Data
{
    public static class DbSeeder
    {
        // Seed method to insert initial data
        public static void Seed(AppDbContext dbContext)
        {
            // Add a log message for debugging
            Console.WriteLine("Seeding the database...");

            if (!dbContext.Customers.Any() && !dbContext.Products.Any() && !dbContext.Orders.Any())
            {
                // Seed customers
                Console.WriteLine("Seeding customers...");
                var customers = Enumerable.Range(1, 10).Select(i => new Customer
                {
                    Name = $"Customer {i}"
                }).ToList();
                dbContext.Customers.AddRange(customers);

                // Seed products
                Console.WriteLine("Seeding products...");
                var products = Enumerable.Range(1, 10).Select(i => new Product
                {
                    ProductName = $"Product {i}",
                    Price = i * 10 // Sample price
                }).ToList();
                dbContext.Products.AddRange(products);

                // Seed orders
                Console.WriteLine("Seeding orders...");
                var random = new Random();
                var orders = Enumerable.Range(1, 10).Select(i => new Order
                {
                    CustomerId = random.Next(1, 11), // Assign to random customer
                    OrderDate = DateTime.Now,
                    OrderDetails = new List<OrderDetail>
            {
                new OrderDetail
                {
                    ProductId = random.Next(1, 11), // Assign random product
                    Quantity = random.Next(1, 5) // Random quantity between 1 and 5
                }
            }
                }).ToList();
                dbContext.Orders.AddRange(orders);

                // Save the data to the database
                dbContext.SaveChanges();
                Console.WriteLine("Database seeded.");
            }
            else
            {
                Console.WriteLine("Database already seeded, skipping...");
            }
        }
    }
}
