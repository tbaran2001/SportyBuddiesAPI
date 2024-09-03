using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.Common.Interfaces;

public interface ISportsRepository
{
    Task AddSportAsync(Sport sport);
    Task<Sport?> GetByIdAsync(Guid sportId);
    Task RemoveSport(Sport sport);
    Task<IEnumerable<Sport>> GetAllAsync();
}