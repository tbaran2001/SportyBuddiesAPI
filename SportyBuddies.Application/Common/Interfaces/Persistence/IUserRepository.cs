using SportyBuddies.Domain.Entities;

namespace SportyBuddies.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void Add(User user);
}