using Microsoft.AspNetCore.JsonPatch;
using Sample.API.Infrastructure;
using Sample.API.Models;
using Sample.API.Persistence.Repositories;
using Sample.API.Services.Interfaces;
using System.Linq.Expressions;

namespace Sample.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Retrieve products with optional filtering and pagination
        public async Task<PaginatedResult<Product>> GetProductsAsync(Expression<Func<Product, bool>> filter = null, int page = 1, int pageSize = 10)
        {
            return await _unitOfWork.Products.FilterAsync(filter, page, pageSize, orderBy: p => p.OrderBy(prod => prod.ProductName));
        }

        // Retrieve a single product by ID
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _unitOfWork.Products.GetByIdAsync(productId);
        }

        // Create a new product
        public async Task AddProductAsync(Product product)
        {
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CompleteAsync();
        }

        // Update an existing product
        public async Task UpdateProductAsync(Product product)
        {
            _unitOfWork.Products.Update(product);
            await _unitOfWork.CompleteAsync();
        }
        
        // Patch an existing product
        public async Task PatchProductAsync(int productId, JsonPatchDocument<Product> patchDoc)
        {
            // Fetch the order by ID
            var product = await _unitOfWork.Products.GetByIdAsync(productId);

            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {productId} not found.");
            }

            // Apply the patch
            _unitOfWork.Products.ApplyPatch(product, patchDoc);

            // Save changes via Unit of Work
            await _unitOfWork.CompleteAsync();
        }

        // Delete a product
        public async Task DeleteProductAsync(int productId)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productId);
            if (product != null)
            {
                _unitOfWork.Products.Delete(product);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
