using Sample.API.Models;

namespace Sample.API.Persistence.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Customer> Customers { get; }
        IRepository<Product> Products { get; }
        IRepository<Order> Orders { get; }
        IRepository<OrderDetail> OrderDetails { get; }

        Task<int> CompleteAsync();
    }
}