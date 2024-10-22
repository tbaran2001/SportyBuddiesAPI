using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Common.Interfaces;

public interface IUsersRepository
{
    Task<User?> GetUserByIdAsync(Guid userId);
    Task<User?> GetUserByIdWithSportsAsync(Guid userId);
    Task<User?> GetUserByIdWithPhotosAsync(Guid userId);
    Task<IEnumerable<User>> GetAllUsersAsync(bool includeSports);
    Task AddUserAsync(User user);
    void RemoveUser(User user);
    Task<IEnumerable<User>> GetAllUsersWithSportsAsync();
}