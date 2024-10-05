using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Common.Interfaces;

public interface IUsersRepository
{
    Task<User?> GetUserByIdAsync(Guid userId);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task AddUserAsync(User user);
    void RemoveUser(User user);
    Task<User?> GetUserByIdWithSportsAsync(Guid userId);
    Task<IEnumerable<User>> GetAllUsersWithSportsAsync();
}