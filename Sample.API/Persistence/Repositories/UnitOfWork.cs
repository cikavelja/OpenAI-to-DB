using Sample.API.Infrastructure.Data;
using Sample.API.Models;
using System;

namespace Sample.API.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Customers = new Repository<Customer>(context);
            Products = new Repository<Product>(context);
            Orders = new Repository<Order>(context);
            OrderDetails = new Repository<OrderDetail>(context);
        }

        public IRepository<Customer> Customers { get; private set; }
        public IRepository<Product> Products { get; private set; }
        public IRepository<Order> Orders { get; private set; }
        public IRepository<OrderDetail> OrderDetails { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
