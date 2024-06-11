using Microsoft.EntityFrameworkCore;
using SportyBuddiesAPI.Entities;

namespace SportyBuddiesAPI.DbContexts;

public class SportyBuddiesContext:DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Sport> Sports { get; set; }
    public DbSet<UserSport> UserSports { get; set; }


    public SportyBuddiesContext(DbContextOptions<SportyBuddiesContext> options):base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserSport>()
            .HasKey(us => new {us.UserId, us.SportId});
        
        modelBuilder.Entity<UserSport>()
            .HasOne(us => us.User)
            .WithMany(u => u.UserSports)
            .HasForeignKey(us => us.UserId);
        
        modelBuilder.Entity<UserSport>()
            .HasOne(us => us.Sport)
            .WithMany(s => s.UserSports)
            .HasForeignKey(us => us.SportId);
            
        
        modelBuilder.Entity<User>().HasData(
            new User {Id = 1, Name = "User1"},
            new User {Id = 2, Name = "User2"},
            new User {Id = 3, Name = "User3"}
        );
        
        modelBuilder.Entity<Sport>().HasData(
            new Sport {Id = 1, Name = "Sport1"},
            new Sport {Id = 2, Name = "Sport2"},
            new Sport {Id = 3, Name = "Sport3"}
        );
        
        modelBuilder.Entity<UserSport>().HasData(
            new UserSport {UserId = 1, SportId = 1},
            new UserSport {UserId = 1, SportId = 2},
            new UserSport {UserId = 2, SportId = 2},
            new UserSport {UserId = 2, SportId = 3},
            new UserSport {UserId = 3, SportId = 1},
            new UserSport {UserId = 3, SportId = 3}
        );
        
        base.OnModelCreating(modelBuilder);
    }
}