using SportyBuddies.Domain.Common;

namespace SportyBuddies.Domain.Users.Events;

public record UserSportRemovedEvent(Guid UserId,Guid SportId):IDomainEvent;