using Xunit;
using Moq;
using Sample.API.Models;
using Sample.API.Services;
using Sample.API.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sample.API.Persistence.Repositories;
using Microsoft.AspNetCore.JsonPatch;


namespace Sample.Test
{
    public class OrderServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IRepository<Customer>> _mockCustomerRepository;
        private readonly Mock<IRepository<Product>> _mockProductRepository;
        private readonly Mock<IRepository<Order>> _mockOrderRepository;
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockCustomerRepository = new Mock<IRepository<Customer>>();
            _mockProductRepository = new Mock<IRepository<Product>>();
            _mockOrderRepository = new Mock<IRepository<Order>>();

            // Set up the UnitOfWork to return mocked repositories
            _mockUnitOfWork.Setup(u => u.Customers).Returns(_mockCustomerRepository.Object);
            _mockUnitOfWork.Setup(u => u.Products).Returns(_mockProductRepository.Object);
            _mockUnitOfWork.Setup(u => u.Orders).Returns(_mockOrderRepository.Object);

            _orderService = new OrderService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetOrderByIdAsync_ReturnsOrder()
        {
            // Arrange
            var order = new Order { OrderId = 1, CustomerId = 1 };
            _mockUnitOfWork.Setup(u => u.Orders.GetByIdAsync(1)).ReturnsAsync(order);

            // Act
            var result = await _orderService.GetOrderByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.OrderId);
        }

        [Fact]
        public async Task PlaceOrderAsync_CallsAddAsyncAndCompleteAsync_WhenCustomerAndProductsExist()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1, Name = "John Doe" };
            var product = new Product { ProductId = 1, ProductName = "Product 1" };
            _mockUnitOfWork.Setup(u => u.Customers.GetByIdAsync(1)).ReturnsAsync(customer);
            _mockUnitOfWork.Setup(u => u.Products.GetByIdAsync(1)).ReturnsAsync(product);

            // Act
            await _orderService.PlaceOrderAsync(1, new List<int> { 1 });

            // Assert
            _mockUnitOfWork.Verify(u => u.Orders.AddAsync(It.IsAny<Order>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task PlaceOrderAsync_ThrowsException_WhenCustomerDoesNotExist()
        {
            // Arrange
            _mockUnitOfWork.Setup(u => u.Customers.GetByIdAsync(1)).ReturnsAsync((Customer)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _orderService.PlaceOrderAsync(1, new List<int> { 1 }));
        }

        [Fact]
        public async Task PatchOrderAsync_CallsApplyPatchAndCompleteAsync()
        {
            // Arrange
            var order = new Order { OrderId = 1 };
            var patchDoc = new JsonPatchDocument<Order>();
            patchDoc.Replace(o => o.OrderDate, System.DateTime.UtcNow);

            _mockOrderRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(order);

            // Act
            await _orderService.PatchOrderAsync(1, patchDoc);

            // Assert
            _mockOrderRepository.Verify(r => r.ApplyPatch(order, patchDoc), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteOrderAsync_CallsDeleteAndCompleteAsync_WhenOrderExists()
        {
            // Arrange
            var order = new Order { OrderId = 1 };
            _mockUnitOfWork.Setup(u => u.Orders.GetByIdAsync(1)).ReturnsAsync(order);

            // Act
            await _orderService.DeleteOrderAsync(1);

            // Assert
            _mockUnitOfWork.Verify(u => u.Orders.Delete(order), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }
    }
}
