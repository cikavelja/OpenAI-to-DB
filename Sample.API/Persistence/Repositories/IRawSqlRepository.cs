namespace Sample.API.Persistence.Repositories
{
    public interface IRawSqlRepository<T>
    {
        Task<object> ExecuteRawSqlAsync(string sql, params object[] parameters);
    }
}