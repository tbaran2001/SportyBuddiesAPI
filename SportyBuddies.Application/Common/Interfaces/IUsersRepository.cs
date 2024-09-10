using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Common.Interfaces;

public interface IUsersRepository:IGenericRepository<User>
{
    Task<User?> GetUserByIdWithSportsAsync(Guid userId);
    Task<IEnumerable<User>> GetAllUsersWithSportsAsync();
}