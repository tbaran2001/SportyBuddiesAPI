using SportyBuddies.Domain.Common;

namespace SportyBuddies.Domain.Profiles.Events;

public record ProfileSportRemovedEvent(Guid ProfileId,Guid SportId):IDomainEvent;