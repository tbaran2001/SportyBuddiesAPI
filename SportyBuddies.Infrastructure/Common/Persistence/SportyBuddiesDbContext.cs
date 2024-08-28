using Microsoft.EntityFrameworkCore;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Infrastructure.Common.Persistence;

public class SportyBuddiesDbContext : DbContext,IUnitOfWork
{
    public DbSet<Sport> Sports { get; set; } = null!;

    public SportyBuddiesDbContext(DbContextOptions options) : base(options)
    {
    }

    public async Task CommitChangesAsync()
    {
        await base.SaveChangesAsync();
    }
}