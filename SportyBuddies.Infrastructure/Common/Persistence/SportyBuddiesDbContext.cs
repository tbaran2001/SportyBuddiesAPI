using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.MatchAggregate;
using SportyBuddies.Domain.SportAggregate;
using SportyBuddies.Domain.UserAggregate;
using SportyBuddies.Domain.UserAggregate.ValueObjects;

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

    public async Task CommitChangesAsync()
    {
        /*var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity.PopDomainEvents())
            .SelectMany(domainEvents => domainEvents)
            .ToList();

        if (IsUserWaitingOnline())
            AddDomainEventsToOfflineProcessingQueue(domainEvents);
        else
            await PublishDomainEvents(publisher, domainEvents);*/

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
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SportyBuddiesDbContext).Assembly);

        modelBuilder.Ignore<UserId>();

        base.OnModelCreating(modelBuilder);
    }
}