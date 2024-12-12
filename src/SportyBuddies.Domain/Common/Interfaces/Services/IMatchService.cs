namespace SportyBuddies.Domain.Common.Interfaces.Services;

public interface IMatchService
{
    Task FindMatchesToAddAsync(Guid profileId);
    Task FindMatchesToRemoveAsync(Guid profileId);
}