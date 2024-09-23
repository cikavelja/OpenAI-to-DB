using Microsoft.EntityFrameworkCore;
using Sample.API.Infrastructure.Data;
using System.Text.Json;

namespace Sample.API.Persistence.Repositories
{
    public class RawSqlRepository<T> : IRawSqlRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly ILogger<RawSqlRepository<T>> _logger;

        public RawSqlRepository(AppDbContext context, ILogger<RawSqlRepository<T>> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<object> ExecuteRawSqlAsync(string sql, params object[] parameters)
        {
            _logger.LogInformation("Executing SQL: {Sql} with parameters: {Parameters}", sql, parameters);

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

                var result = new List<string>();
                var jsonResponse = "";
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var row = new Dictionary<string, string>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row.Add(reader.GetName(i), reader[i].ToString());
                            jsonResponse += reader[i].ToString();
                        }

                    }
                }

                return jsonResponse;
            }
        }

    }
}
