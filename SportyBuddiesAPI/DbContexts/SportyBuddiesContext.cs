using Microsoft.EntityFrameworkCore;
using SportyBuddiesAPI.Entities;

namespace SportyBuddiesAPI.DbContexts;

public class SportyBuddiesContext:DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Sport> Sports { get; set; }

    public SportyBuddiesContext(DbContextOptions<SportyBuddiesContext> options):base(options)
    {
        
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