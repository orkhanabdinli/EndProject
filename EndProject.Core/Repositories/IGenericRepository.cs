using EndProject.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EndProject.Core.Repositories;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity, new()
{
    DbSet<TEntity> Table { get; }
    Task InsertAsync(TEntity entity);
    Task<int> CommitAsync();
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null, params string[] includes);
    Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> expression = null, params string[] includes);
    Task<TEntity> GetByIdAsync(int id);
    Task<bool> IsExist(Expression<Func<TEntity, bool>> expression);
    void Delete(TEntity entity);
}
