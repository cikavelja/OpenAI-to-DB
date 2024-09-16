using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore.Query;
using Sample.API.Infrastructure;
using System.Linq.Expressions;

namespace Sample.API.Persistence.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);

        // Generic method to filter and query data with pagination, sorting, and eager loading.
        Task<PaginatedResult<T>> FilterAsync(
            Expression<Func<T, bool>> filter = null,
            int page = 1,
            int pageSize = 20,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        // Add a new entity
        Task AddAsync(T entity);

        // Update an existing entity
        void Update(T entity);

        // Patch an existing entity
        void ApplyPatch(T entity, JsonPatchDocument<T> patchDoc);

        // Delete an entity
        void Delete(T entity);
    }
}
