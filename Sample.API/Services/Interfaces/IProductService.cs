using Microsoft.AspNetCore.JsonPatch;
using Sample.API.Infrastructure;
using Sample.API.Models;
using System.Linq.Expressions;

namespace Sample.API.Services.Interfaces
{
    public interface IProductService
    {
        Task AddProductAsync(Product product);
        Task DeleteProductAsync(int productId);
        Task<Product> GetProductByIdAsync(int productId);
        Task<PaginatedResult<Product>> GetProductsAsync(Expression<Func<Product, bool>> filter = null, int page = 1, int pageSize = 10);
        Task UpdateProductAsync(Product product);
        Task PatchProductAsync(int productId, JsonPatchDocument<Product> patchDoc);
    }
}