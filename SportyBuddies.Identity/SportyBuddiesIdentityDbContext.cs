using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportyBuddies.Identity.Models;

namespace SportyBuddies.Identity;

public class SportyBuddiesIdentityDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public SportyBuddiesIdentityDbContext(DbContextOptions<SportyBuddiesIdentityDbContext> options) : base(options)
    {
    }
}