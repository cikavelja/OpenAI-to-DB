using Microsoft.EntityFrameworkCore;
using Sample.API.Infrastructure.Data;
using System.Dynamic;

namespace Sample.API.Persistence.Repositories
{
    public class RawSqlRepository<T> : IRawSqlRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public RawSqlRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<dynamic>> ExecuteRawSqlAsync(string sql, params object[] parameters)
        {
            var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                foreach (var param in parameters)
                {
                    // Assuming parameters are named parameters, adjust accordingly
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = $"@p{Array.IndexOf(parameters, param)}";
                    parameter.Value = param;
                    command.Parameters.Add(parameter);
                }

                var result = new List<dynamic>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var row = new ExpandoObject() as IDictionary<string, Object>;
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row.Add(reader.GetName(i), reader[i]);
                        }
                        result.Add(row);
                    }
                }
                return result;
            }
        }


    }
}
