namespace Varilleros.src.Infrastructure.Data;

using System.Data;
using MySqlConnector;
using MultiTenancy;

public sealed class TenantDbConnectionFactory(
    TenantContextHolder tenantCtxHolder) : ITenantDbConnectionFactory
{
    public IDbConnection CreateConnection()
    {
        var ctx = tenantCtxHolder.Get();
        return new MySqlConnection(ctx.DbConnStr);
    }
}
