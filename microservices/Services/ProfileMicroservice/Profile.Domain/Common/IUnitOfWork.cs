namespace Profile.Domain.Common;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}