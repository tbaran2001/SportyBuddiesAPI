namespace SportyBuddies.Domain.Services;

public interface IMatchService
{
    Task FindMatchesToAddAsync(Guid userId);
    Task FindMatchesToRemoveAsync(Guid userId);
}