using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users;
using SportyBuddies.Identity.Models;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Identity;

public class IdentityEventsHandler
{
    private readonly SportyBuddiesDbContext _context;

    public IdentityEventsHandler(SportyBuddiesDbContext context)
    {
        _context = context;
    }

    public async Task OnUserCreatedAsync(ApplicationUser user)
    {
        var userEntity = new User(user.UserName, null, DateTime.Now, new List<Sport>(), null,
            new List<UserPhoto>(), 0, user.Id);
        await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync();
    }
}