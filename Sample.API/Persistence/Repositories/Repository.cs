using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Sample.API.Infrastructure;
using Sample.API.Infrastructure.Data;
using System.Linq.Expressions;

namespace Sample.API.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<PaginatedResult<T>> FilterAsync(
            Expression<Func<T, bool>> filter = null,
            int page = 1,
            int pageSize = 20,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();

            // Apply filtering
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Apply eager loading (Include related entities)
            if (include != null)
            {
                query = include(query);
            }

            // Get total record count asynchronously
            int totalRecords = await query.CountAsync();

            // Apply ordering
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            // Apply pagination
            var data = await query.Skip((page - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();

            // Return the paginated result
            return new PaginatedResult<T>
            {
                Data = data,
                TotalRecords = totalRecords,
                PageSize = pageSize,
                CurrentPage = page
            };
        }
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public void ApplyPatch(T entity, JsonPatchDocument<T> patchDoc)
        {
            if (entity == null || patchDoc == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity or patch document cannot be null.");
            }

            // Apply the patch
            patchDoc.ApplyTo(entity);
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

    }
}
