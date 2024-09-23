using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Sample.API.Services.Interfaces;
using System.Text;
using System.Threading.Tasks;

namespace Sample.API.Services
{
    public class DbRepresentationService : IDbRepresentationService
    {
        private readonly string _connectionString;
        public DbRepresentationService(IConfiguration configuration)
        {
            // Retrieve the connection string
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<string> GenerateSchemaScriptAsync()
        {
            return await Task.Run(() =>
            {
                var builder = new SqlConnectionStringBuilder(_connectionString);

                // Extract individual components
                string serverName = builder.DataSource;  // Server name
                string databaseName = builder.InitialCatalog;  // Database name
                string userId = builder.UserID;        // User name
                string password = builder.Password;      // Password

                // Create a ServerConnection object
                ServerConnection serverConnection;

                // Check if userId and password are empty for Windows Authentication
                if (string.IsNullOrEmpty(userId) && string.IsNullOrEmpty(password))
                {
                    // Use Windows Authentication
                    serverConnection = new ServerConnection(serverName);
                }
                else
                {
                    // Use SQL Server Authentication
                    serverConnection = new ServerConnection(serverName, userId, password);
                }

                // Create a Server object
                Server server = new Server(serverConnection);

                // Reference the database you want to generate the script from
                Database database = server.Databases[databaseName];

                // Define the ScriptingOptions to customize the schema generation
                ScriptingOptions options = new ScriptingOptions
                {
                    ScriptSchema = true, // Script schema only
                    ScriptData = false, // Set to true if you also want to include data
                    Indexes = true, // Include indexes
                    Triggers = true, // Include triggers
                    DriAll = true, // Include primary keys, foreign keys, constraints, etc.
                    IncludeHeaders = true
                };

                // Use a StringBuilder to accumulate the script
                StringBuilder scriptBuilder = new StringBuilder();

                // Generate the script for each table in the database
                foreach (Microsoft.SqlServer.Management.Smo.Table table in database.Tables)
                {
                    if (!table.IsSystemObject)
                    {
                        // Script each table schema
                        var script = table.Script(options);
                        foreach (string line in script)
                        {
                            scriptBuilder.AppendLine(line);
                        }
                    }
                }

                // Return the full script as a string
                return scriptBuilder.ToString();
            });
        }
    }
}
