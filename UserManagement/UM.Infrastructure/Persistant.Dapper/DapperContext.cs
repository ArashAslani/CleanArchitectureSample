using Microsoft.Data.SqlClient;
using System.Data;

namespace UM.Infrastructure.Persistant.Dapper;

public class DapperContext
{
    private readonly string _connectionString;

    public DapperContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);

    public string UserTokens => "[user].Tokens";
}