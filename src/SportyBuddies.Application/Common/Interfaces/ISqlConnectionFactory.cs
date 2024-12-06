using System.Data;

namespace SportyBuddies.Application.Common.Interfaces;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}