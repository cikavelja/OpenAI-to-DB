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
                // Seed customers with real names
                Console.WriteLine("Seeding customers...");
                var customerNames = new List<string>
        {
            "Alice Johnson", "Bob Williams", "Carol Davis", "David Wilson", "Eve Brown",
            "Franklin Green", "Grace Lee", "Henry Martin", "Isabel White", "Jack Harris"
        };

                var customers = customerNames.Select((name, i) => new Customer
                {
                    Name = name
                }).ToList();
                dbContext.Customers.AddRange(customers);

                // Seed products with unique vegetable names
                Console.WriteLine("Seeding products...");
                var vegetableNames = new List<string>
        {
            "Carrot", "Broccoli", "Spinach", "Cauliflower", "Tomato",
            "Cucumber", "Pepper", "Onion", "Garlic", "Zucchini"
        };

                var products = vegetableNames.Select((veg, i) => new Product
                {
                    ProductName = veg,
                    Price = (i + 1) * 10 // Sample price, can be adjusted
                }).ToList();
                dbContext.Products.AddRange(products);

                // Seed orders
                Console.WriteLine("Seeding orders...");
                var random = new Random();
                var orders = Enumerable.Range(1, 10).Select(i => new Order
                {
                    CustomerId = random.Next(1, customers.Count + 1), // Assign to random customer
                    OrderDate = DateTime.Now,
                    OrderDetails = new List<OrderDetail>
            {
                new OrderDetail
                {
                    ProductId = random.Next(1, products.Count + 1), // Assign random product
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
