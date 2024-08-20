using SportyBuddies.Application.Common.Interfaces.Persistence;
using SportyBuddies.Domain.Entities;

namespace SportyBuddies.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private static readonly List<User> _users = new();

    public User? GetUserByEmail(string email)
    {
        return _users.FirstOrDefault(u => u.Email == email);
    }

    public void Add(User user)
    {
        _users.Add(user);
    }
}