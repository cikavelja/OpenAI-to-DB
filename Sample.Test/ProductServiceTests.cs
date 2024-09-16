using Microsoft.AspNetCore.JsonPatch;
using Moq;
using Sample.API.Models;
using Sample.API.Persistence.Repositories;
using Sample.API.Services;

namespace Sample.Test
{

    public class ProductServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IRepository<Product>> _mockProductRepository;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockProductRepository = new Mock<IRepository<Product>>();

            // Set up the UnitOfWork to return the mock product repository
            _mockUnitOfWork.Setup(u => u.Products).Returns(_mockProductRepository.Object);

            _productService = new ProductService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnsProduct()
        {
            // Arrange
            var product = new Product { ProductId = 1, ProductName = "Product 1" };
            _mockUnitOfWork.Setup(u => u.Products.GetByIdAsync(1)).ReturnsAsync(product);

            // Act
            var result = await _productService.GetProductByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.ProductId);
        }

        [Fact]
        public async Task AddProductAsync_CallsAddAsyncAndCompleteAsync()
        {
            // Arrange
            var product = new Product { ProductId = 1, ProductName = "Product 1" };

            // Act
            await _productService.AddProductAsync(product);

            // Assert
            _mockUnitOfWork.Verify(u => u.Products.AddAsync(product), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateProductAsync_CallsUpdateAndCompleteAsync()
        {
            // Arrange
            var product = new Product { ProductId = 1, ProductName = "Product 1" };

            // Act
            await _productService.UpdateProductAsync(product);

            // Assert
            _mockUnitOfWork.Verify(u => u.Products.Update(product), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteProductAsync_CallsDeleteAndCompleteAsync_WhenProductExists()
        {
            // Arrange
            var product = new Product { ProductId = 1, ProductName = "Product 1" };
            _mockUnitOfWork.Setup(u => u.Products.GetByIdAsync(1)).ReturnsAsync(product);

            // Act
            await _productService.DeleteProductAsync(1);

            // Assert
            _mockUnitOfWork.Verify(u => u.Products.Delete(product), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task PatchProductAsync_CallsApplyPatchAndCompleteAsync()
        {
            // Arrange
            var product = new Product { ProductId = 1, ProductName = "Product 1" };
            var patchDoc = new JsonPatchDocument<Product>();
            patchDoc.Replace(p => p.ProductName, "Updated Product");

            _mockProductRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(product);

            // Act
            await _productService.PatchProductAsync(1, patchDoc);

            // Assert
            _mockProductRepository.Verify(r => r.ApplyPatch(product, patchDoc), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteProductAsync_DoesNotCallDelete_WhenProductDoesNotExist()
        {
            // Arrange
            _mockUnitOfWork.Setup(u => u.Products.GetByIdAsync(1)).ReturnsAsync((Product)null);

            // Act
            await _productService.DeleteProductAsync(1);

            // Assert
            _mockUnitOfWork.Verify(u => u.Products.Delete(It.IsAny<Product>()), Times.Never);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Never);
        }
    }
}
