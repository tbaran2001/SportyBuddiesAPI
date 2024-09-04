using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Users;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Infrastructure.Users.Persistence;

public class UsersRepository:GenericRepository<User>,IUsersRepository
{
    public UsersRepository(SportyBuddiesDbContext dbContext) : base(dbContext)
    {
    }
}