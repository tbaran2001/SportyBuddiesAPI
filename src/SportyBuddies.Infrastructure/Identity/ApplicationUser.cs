using Microsoft.AspNetCore.Identity;
using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public string Name { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; }
}