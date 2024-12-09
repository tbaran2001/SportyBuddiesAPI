using SportyBuddies.Domain.Users;

namespace SportyBuddies.Infrastructure.Identity;

public class IdentityEventsHandler(SportyBuddiesDbContext context)
{
    public async Task OnUserCreatedAsync(ApplicationUser user)
    {
        var userEntity = User.Create(user.Id, user.Name, user.DateOfBirth, user.Gender);
        await context.Users.AddAsync(userEntity);
        await context.SaveChangesAsync();
    }
}