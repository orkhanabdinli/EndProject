using EndProject.Core.Entities;
using EndProject.Core.Repositories;
using EndProject.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EndProject.Data.Repositories;

public class AppUserRepository : IAppUserRepository
{
    private readonly AppDbContext _context;
    public AppUserRepository(AppDbContext context)
    {
        _context = context;
    }
    public DbSet<AppUser> Table => _context.Set<AppUser>();

    public async Task InsertAsync(AppUser user)
    {
        await Table.AddAsync(user);
    }

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task<List<AppUser>> GetAllAsync(Expression<Func<AppUser, bool>> expression = null, params string[] includes)
    {
        var query = Table.AsQueryable();
        if (includes is not null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        return expression is not null
            ? await query.Where(expression).ToListAsync()
            : await query.ToListAsync();
    }

    public async Task<AppUser> GetByIdAsync(string id)
    {
        return await Table.FindAsync(id);
    }

    public async Task<AppUser> GetSingleAsync(Expression<Func<AppUser, bool>> expression = null, params string[] includes)
    {
        var query = Table.AsQueryable();
        if (includes is not null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        return expression is not null
            ? await query.Where(expression).FirstOrDefaultAsync()
            : await query.FirstOrDefaultAsync();
    }

    public async Task<bool> IsExist(Expression<Func<AppUser, bool>> expression)
    {
        return await Table.AnyAsync(expression);
    }

    public void Delete(AppUser user)
    {
        Table.Remove(user);
    }
}
