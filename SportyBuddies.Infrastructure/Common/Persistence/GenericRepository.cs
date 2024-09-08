using Microsoft.EntityFrameworkCore;
using SportyBuddies.Application.Common.Interfaces;

namespace SportyBuddies.Infrastructure.Common.Persistence;

public class GenericRepository<T>:IGenericRepository<T> where T:class
{
    protected readonly SportyBuddiesDbContext _dbContext;

    public GenericRepository(SportyBuddiesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dbContext.Set<T>().Update(entity);
    }

    public void Remove(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }
}