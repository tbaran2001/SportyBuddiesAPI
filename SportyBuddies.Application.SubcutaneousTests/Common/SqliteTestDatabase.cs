using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Application.SubcutaneousTests.Common;

public class SqliteTestDatabase : IDisposable
{
    private SqliteTestDatabase(string connectionString)
    {
        Connection = new SqliteConnection(connectionString);
    }

    public SqliteConnection Connection { get; }

    public void Dispose()
    {
        Connection.Close();
    }

    public static SqliteTestDatabase CreateAndInitialize()
    {
        var testDatabase = new SqliteTestDatabase("DataSource=:memory:");

        testDatabase.InitializeDatabase();

        return testDatabase;
    }

    public void InitializeDatabase()
    {
        Connection.Open();
        var options = new DbContextOptionsBuilder<SportyBuddiesDbContext>()
            .UseSqlite(Connection)
            .Options;

        var context = new SportyBuddiesDbContext(options, null!, null!);

        context.Database.EnsureCreated();
    }

    public void ResetDatabase()
    {
        Connection.Close();

        InitializeDatabase();
    }
}