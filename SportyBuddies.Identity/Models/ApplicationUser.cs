using Microsoft.AspNetCore.Identity;

namespace SportyBuddies.Identity.Models;

public class ApplicationUser : IdentityUser<Guid>
{
    public string Name { get; set; }
}