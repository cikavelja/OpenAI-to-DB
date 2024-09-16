using Microsoft.AspNetCore.JsonPatch;
using Sample.API.Infrastructure;
using Sample.API.Models;
using Sample.API.Persistence.Dto.OrderDto;
using System.Linq.Expressions;

namespace Sample.API.Services.Interfaces
{
    public interface IOrderService
    {
        Task DeleteOrderAsync(int orderId);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<PaginatedResult<Order>> GetOrdersAsync(Expression<Func<Order, bool>> filter = null, int page = 1, int pageSize = 10);
        Task PlaceOrderAsync(int customerId, List<int> productIds);
        Task PatchOrderAsync(int orderId, JsonPatchDocument<Order> patchDoc);
    }
}