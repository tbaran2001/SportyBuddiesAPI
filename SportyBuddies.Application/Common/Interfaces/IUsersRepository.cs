using SportyBuddies.Domain.UserAggregate;
using SportyBuddies.Domain.UserAggregate.ValueObjects;

namespace SportyBuddies.Application.Common.Interfaces;

public interface IUsersRepository
{
    Task<User?> GetUserByIdAsync(UserId userId);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task AddUserAsync(User user);
    void RemoveUser(User user);
    Task<User?> GetUserByIdWithSportsAsync(UserId userId);
    Task<IEnumerable<User>> GetAllUsersWithSportsAsync();
}