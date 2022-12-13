using System.Linq.Expressions;
using EmployeeSystem.Domain.Entities;

namespace EmployeeSystem.Application.Interfaces.Persistence;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter);

    Task<TEntity?> GetByIdAsync(int id);

    void Add(TEntity entity);

    void Update(TEntity entity);

    Task DeleteByIdAsync(int id);
}