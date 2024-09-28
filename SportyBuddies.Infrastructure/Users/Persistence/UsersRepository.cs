using Microsoft.EntityFrameworkCore;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Users;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Infrastructure.Users.Persistence;

public class UsersRepository : GenericRepository<User>, IUsersRepository
{
    public UsersRepository(SportyBuddiesDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<User?> GetUserByIdWithSportsAsync(Guid userId)
    {
        return await _dbContext.Users
            .Include(u => u.Sports)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<IEnumerable<User>> GetAllUsersWithSportsAsync()
    {
        return await _dbContext.Users
            .Include(u => u.Sports)
            .ToListAsync();
    }
}