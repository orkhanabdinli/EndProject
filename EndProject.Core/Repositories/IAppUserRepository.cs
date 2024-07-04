using EndProject.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EndProject.Core.Repositories;

public interface IAppUserRepository
{
    DbSet<AppUser> Table { get; }
    Task InsertAsync(AppUser user);
    Task<int> CommitAsync();
    Task<List<AppUser>> GetAllAsync(Expression<Func<AppUser, bool>> expression = null, params string[] includes);
    Task<AppUser> GetSingleAsync(Expression<Func<AppUser, bool>> expression = null, params string[] includes);
    Task<AppUser> GetByIdAsync(string id);
    Task<bool> IsExist(Expression<Func<AppUser, bool>> expression);
    void Delete(AppUser user);
}
