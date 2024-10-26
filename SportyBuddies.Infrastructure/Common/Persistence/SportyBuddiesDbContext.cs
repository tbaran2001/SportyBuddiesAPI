using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SportyBuddies.Domain.Buddies;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Matches;
using SportyBuddies.Domain.Messages;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Infrastructure.Common.Persistence;

public class SportyBuddiesDbContext(
    DbContextOptions<SportyBuddiesDbContext> options,
    IHttpContextAccessor httpContextAccessor,
    IPublisher publisher)
    : DbContext(options), IUnitOfWork
{
    public DbSet<Sport> Sports { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<Buddy> Buddies { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<UserPhoto> UserPhotos { get; set; }

    public async Task CommitChangesAsync()
    {
        await base.SaveChangesAsync();
    }

    private async Task PublishDomainEvents(IPublisher _publisher, List<IDomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents) await _publisher.Publish(domainEvent);
    }

    private bool IsUserWaitingOnline()
    {
        return httpContextAccessor.HttpContext is not null;
    }

    private void AddDomainEventsToOfflineProcessingQueue(List<IDomainEvent> domainEvents)
    {
        var domainEventsQueue =
            httpContextAccessor.HttpContext!.Items.TryGetValue("DomainEventsQueue", out var value)
            && value is Queue<IDomainEvent> existingDomainEvents
                ? existingDomainEvents
                : new Queue<IDomainEvent>();

        domainEvents.ForEach(domainEventsQueue.Enqueue);

        httpContextAccessor.HttpContext!.Items["DomainEventsQueue"] = domainEventsQueue;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Sports)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "UserSports",
                j => j
                    .HasOne<Sport>()
                    .WithMany()
                    .HasForeignKey("SportId"),
                j => j
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
            );

        modelBuilder.Entity<User>()
            .HasMany(u => u.Photos)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasOne(u => u.MainPhoto)
            .WithMany()
            .HasForeignKey(u => u.MainPhotoId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<UserPhoto>()
            .Property(up => up.Id)
            .ValueGeneratedNever();

        modelBuilder.Entity<User>().OwnsOne(u => u.Preferences);

        base.OnModelCreating(modelBuilder);
    }
}