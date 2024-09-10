namespace SportyBuddies.Application.Common.Interfaces;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}