using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Sample.API.Infrastructure;
using Sample.API.Models;
using Sample.API.Persistence.Repositories;
using Sample.API.Services.Interfaces;
using System.Linq.Expressions;

namespace Sample.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Retrieve orders with optional filtering and pagination
        public async Task<PaginatedResult<Order>> GetOrdersAsync(Expression<Func<Order, bool>> filter = null, int page = 1, int pageSize = 10)
        {
            return await _unitOfWork.Orders.FilterAsync(filter, page, pageSize,
                include: o => o.Include(order => order.Customer).Include(order => order.OrderDetails));
        }

        // Retrieve a single order by ID
        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order is null)
            {
                throw new Exception("Order not found");
            }

            return order;
        }

        // Place a new order
        public async Task PlaceOrderAsync(int customerId, List<int> productIds)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
            if (customer is null)
            {
                throw new Exception("Order not found");
            }

            var order = new Order
            {
                CustomerId = customerId,
                OrderDate = DateTime.UtcNow,
                OrderDetails = new List<OrderDetail>()
            };

            foreach (var productId in productIds)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(productId);
                if (product is null)
                {
                    throw new Exception($"Product with ID {productId} not found");
                }

                order.OrderDetails.Add(new OrderDetail
                {
                    ProductId = productId,
                    Quantity = 1 // For simplicity, set default quantity as 1
                });
            }

            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.CompleteAsync();
        }

        //Patchan existing order
        public async Task PatchOrderAsync(int orderId, JsonPatchDocument<Order> patchDoc)
        {
            // Fetch the order by ID
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);

            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {orderId} not found.");
            }

            // Apply the patch
            _unitOfWork.Orders.ApplyPatch(order, patchDoc);

            // Save changes via Unit of Work
            await _unitOfWork.CompleteAsync();
        }
        // Delete an order
        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order != null)
            {
                _unitOfWork.Orders.Delete(order);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
