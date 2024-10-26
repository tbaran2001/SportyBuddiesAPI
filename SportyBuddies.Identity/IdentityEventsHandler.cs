using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users;
using SportyBuddies.Identity.Models;
using SportyBuddies.Infrastructure.Common.Persistence;

namespace SportyBuddies.Identity;

public class IdentityEventsHandler(SportyBuddiesDbContext context)
{
    public async Task OnUserCreatedAsync(ApplicationUser user)
    {
        var userEntity = User.Create(user.Id);
        await context.Users.AddAsync(userEntity);
        await context.SaveChangesAsync();
    }
}