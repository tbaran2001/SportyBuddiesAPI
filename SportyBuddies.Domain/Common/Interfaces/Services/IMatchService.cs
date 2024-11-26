namespace SportyBuddies.Domain.Common.Interfaces.Services;

public interface IMatchService
{
    Task FindMatchesToAddAsync(Guid userId);
    Task FindMatchesToRemoveAsync(Guid userId);
}