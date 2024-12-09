using System.Data;
using Microsoft.Data.SqlClient;
using Npgsql;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Infrastructure;

internal sealed class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        var connection = new SqlConnection(_connectionString);
        connection.Open();

        return connection;
    }
}