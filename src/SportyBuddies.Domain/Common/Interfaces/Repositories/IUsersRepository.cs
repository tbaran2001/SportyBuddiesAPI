using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.Common.Interfaces.Repositories;

public interface IUsersRepository
{
    Task<User?> GetUserByIdAsync(Guid userId);
    Task<User?> GetUserByIdWithSportsAsync(Guid userId);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task AddUserAsync(User user);
    void RemoveUser(User user);
    Task<IEnumerable<User>> GetPotentialMatchesAsync(Guid userId,IEnumerable<Guid> userSports);
}