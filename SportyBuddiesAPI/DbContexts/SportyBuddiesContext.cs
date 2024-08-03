using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using SportyBuddiesAPI.Entities;

namespace SportyBuddiesAPI.DbContexts;

public class SportyBuddiesContext:IdentityDbContext<User>
{
    public DbSet<User> Users { get; set; }
    public DbSet<Sport> Sports { get; set; }
    public DbSet<Match> Matches { get; set; }

    public SportyBuddiesContext(DbContextOptions<SportyBuddiesContext> options):base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        List<IdentityRole> roles = new List<IdentityRole>
        {
            new IdentityRole {Name = "Admin", NormalizedName = "ADMIN"},
            new IdentityRole {Name = "User", NormalizedName = "USER"}
        };
        
        modelBuilder.Entity<IdentityRole>().HasData(roles);
        
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

    }

}