using Microsoft.EntityFrameworkCore;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Infrastructure.Common.Persistence;

public class SportyBuddiesDbContext : DbContext, IUnitOfWork
{
    public SportyBuddiesDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Sport> Sports { get; set; }

    public async Task CommitChangesAsync()
    {
        await base.SaveChangesAsync();
    }
}