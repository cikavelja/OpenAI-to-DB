using Microsoft.AspNetCore.JsonPatch;
using Moq;
using Sample.API.Models;
using Sample.API.Persistence.Repositories;
using Sample.API.Services;

namespace Sample.Test
{

    public class CustomerServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IRepository<Customer>> _mockCustomerRepository;
        private readonly CustomerService _customerService;

        public CustomerServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockCustomerRepository = new Mock<IRepository<Customer>>();

            // Set up the Customers property to return the mock repository
            _mockUnitOfWork.Setup(u => u.Customers).Returns(_mockCustomerRepository.Object);

            _customerService = new CustomerService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task AddCustomerAsync_CallsAddAsyncAndCompleteAsync()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1, Name = "John Doe" };

            // Act
            await _customerService.AddCustomerAsync(customer);

            // Assert
            _mockUnitOfWork.Verify(u => u.Customers.AddAsync(customer), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateCustomerAsync_CallsUpdateAndCompleteAsync()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1, Name = "John Doe" };

            // Act
            await _customerService.UpdateCustomerAsync(customer);

            // Assert
            _mockUnitOfWork.Verify(u => u.Customers.Update(customer), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteCustomerAsync_CallsDeleteAndCompleteAsync_WhenCustomerExists()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1, Name = "John Doe" };
            _mockUnitOfWork.Setup(u => u.Customers.GetByIdAsync(1)).ReturnsAsync(customer);

            // Act
            await _customerService.DeleteCustomerAsync(1);

            // Assert
            _mockUnitOfWork.Verify(u => u.Customers.Delete(customer), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task PatchCustomerAsync_CallsApplyPatchAndCompleteAsync()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1, Name = "John Doe" };
            var patchDoc = new JsonPatchDocument<Customer>();
            patchDoc.Replace(c => c.Name, "Updated Name");

            _mockCustomerRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(customer);

            // Act
            await _customerService.PatchCustomerAsync(1, patchDoc);

            // Assert
            _mockCustomerRepository.Verify(r => r.ApplyPatch(customer, patchDoc), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteCustomerAsync_DoesNotCallDelete_WhenCustomerDoesNotExist()
        {
            // Arrange
            _mockUnitOfWork.Setup(u => u.Customers.GetByIdAsync(1)).ReturnsAsync((Customer)null);

            // Act
            await _customerService.DeleteCustomerAsync(1);

            // Assert
            _mockUnitOfWork.Verify(u => u.Customers.Delete(It.IsAny<Customer>()), Times.Never);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Never);
        }
    }
}
