using System.Linq.Expressions;
using EmployeeSystem.Application.Exceptions;
using EmployeeSystem.Application.Interfaces.Persistence;
using EmployeeSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSystem.Infrastructure.Persistence.DataAccess;

public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private protected readonly SystemContext _context;
    private protected readonly DbSet<TEntity> _set;

    public BaseRepository(SystemContext context)
    {
        _context = context;
        _set = context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter)
    {
        if (filter is null)
        {
            return await _set.ToListAsync();
        }

        return await _set.Where(filter)
                         .ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _set.FindAsync(id);
    }

    public void Add(TEntity entity)
    {
        _set.Add(entity);
    }

    public void Update(TEntity entity)
    {
        _set.Update(entity);
    }

    public async Task DeleteByIdAsync(int id)
    {
        var entityToDelete = await _set.FindAsync(id);

        if (entityToDelete is null)
        {
            throw new NotFoundException("Entity with specified id was not found.");
        }

        _set.Remove(entityToDelete);
    }
}