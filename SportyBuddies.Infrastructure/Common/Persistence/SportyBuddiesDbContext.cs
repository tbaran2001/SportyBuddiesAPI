using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Matches;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Infrastructure.Common.Persistence;

public class SportyBuddiesDbContext : DbContext, IUnitOfWork
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SportyBuddiesDbContext(DbContextOptions<SportyBuddiesDbContext> options,
        IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public DbSet<Sport> Sports { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Match> Matches { get; set; }

    public async Task CommitChangesAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity.PopDomainEvents())
            .SelectMany(domainEvents => domainEvents)
            .ToList();

        AddDomainEventsToOfflineProcessingQueue(domainEvents);

        await base.SaveChangesAsync();
    }

    private void AddDomainEventsToOfflineProcessingQueue(List<IDomainEvent> domainEvents)
    {
        var domainEventsQueue =
            _httpContextAccessor.HttpContext!.Items.TryGetValue("DomainEventsQueue", out var value)
            && value is Queue<IDomainEvent> existingDomainEvents
                ? existingDomainEvents
                : new Queue<IDomainEvent>();

        domainEvents.ForEach(domainEventsQueue.Enqueue);

        _httpContextAccessor.HttpContext!.Items["DomainEventsQueue"] = domainEventsQueue;
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