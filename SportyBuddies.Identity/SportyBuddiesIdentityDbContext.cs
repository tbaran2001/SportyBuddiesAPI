using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportyBuddies.Identity.Models;

namespace SportyBuddies.Identity;

public class SportyBuddiesIdentityDbContext(DbContextOptions<SportyBuddiesIdentityDbContext> options)
    : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options);