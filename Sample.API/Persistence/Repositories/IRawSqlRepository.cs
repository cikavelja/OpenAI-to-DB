namespace Sample.API.Persistence.Repositories
{
    public interface IRawSqlRepository<T>
    {
        Task<IEnumerable<dynamic>> ExecuteRawSqlAsync(string sql, params object[] parameters);
    }
}