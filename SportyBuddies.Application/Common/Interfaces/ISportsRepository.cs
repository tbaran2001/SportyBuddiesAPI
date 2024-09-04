using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.Common.Interfaces;

public interface ISportsRepository:IGenericRepository<Sport>
{
    Task<bool> SportExistsAsync(Guid id);
}