using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Infrastructure.Identity;

public class IdentityEventsHandler(SportyBuddiesDbContext context)
{
    public async Task OnUserCreatedAsync(ApplicationUser user)
    {
        var userEntity = Profile.Create(user.Id, user.Name, user.DateOfBirth, user.Gender);
        await context.Profiles.AddAsync(userEntity);
        await context.SaveChangesAsync();
    }
}