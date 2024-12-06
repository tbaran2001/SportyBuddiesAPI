using SportyBuddies.Domain.Common;

namespace SportyBuddies.Domain.Users.Events;

public record UserSportAddedEvent(Guid UserId,Guid SportId):IDomainEvent;