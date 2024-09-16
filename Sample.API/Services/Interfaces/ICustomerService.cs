using Microsoft.AspNetCore.JsonPatch;
using Sample.API.Infrastructure;
using Sample.API.Models;
using System.Linq.Expressions;

namespace Sample.API.Services.Interfaces
{
    public interface ICustomerService
    {
        Task AddCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int customerId);
        Task<Customer> GetCustomerByIdAsync(int customerId);
        Task<PaginatedResult<Customer>> GetCustomersAsync(Expression<Func<Customer, bool>> filter = null, int page = 1, int pageSize = 10);
        Task UpdateCustomerAsync(Customer customer);
        Task PatchCustomerAsync(int customerId, JsonPatchDocument<Customer> patchDoc);
    }
}