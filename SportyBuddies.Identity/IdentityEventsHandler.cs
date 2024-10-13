using SportyBuddies.Domain.UserAggregate;
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
        var userEntity = User.CreateWithId(user.Id, user.UserName, user.UserName, DateTime.Now);
        await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync();
    }
}