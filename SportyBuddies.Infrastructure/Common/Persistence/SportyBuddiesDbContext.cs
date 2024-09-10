using Microsoft.EntityFrameworkCore;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Matches;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Infrastructure.Common.Persistence;

public class SportyBuddiesDbContext : DbContext, IUnitOfWork
{
    public SportyBuddiesDbContext(DbContextOptions<SportyBuddiesDbContext> options) : base(options)
    {
    }

    public DbSet<Sport> Sports { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Match> Matches { get; set; }

    public async Task CommitChangesAsync()
    {
        await base.SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Sports)
            .WithMany(s => s.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserSport",
                j => j
                    .HasOne<Sport>()
                    .WithMany()
                    .HasForeignKey("SportId"),
                j => j
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
            );

        base.OnModelCreating(modelBuilder);
    }
}