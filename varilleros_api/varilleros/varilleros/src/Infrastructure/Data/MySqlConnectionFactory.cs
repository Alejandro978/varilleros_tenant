namespace Varilleros.src.Infrastructure.Data;

using System.Data;
using MySqlConnector;

public sealed class MySqlConnectionFactory(string connectionString) : IMasterDbConnectionFactory
{
    public IDbConnection CreateConnection() => new MySqlConnection(connectionString);
}
