namespace SportyBuddies.Domain.Common;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}