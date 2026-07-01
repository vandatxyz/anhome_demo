using AnHomes.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AnHomes.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AnHomes.Infrastructure.Data.AnHomesDbContext _context;
    protected readonly Microsoft.EntityFrameworkCore.DbSet<T> _dbSet;

    public Repository(AnHomes.Infrastructure.Data.AnHomesDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _dbSet.FindAsync(new object[] { id }, ct);

    public virtual async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default)
        => await _dbSet.ToListAsync(ct);

    public virtual async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
        => await _dbSet.Where(predicate).ToListAsync(ct);

    public virtual async Task<T> AddAsync(T entity, CancellationToken ct = default)
    {
        await _dbSet.AddAsync(entity, ct);
        return entity;
    }

    public virtual async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default)
    {
        var list = entities.ToList();
        await _dbSet.AddRangeAsync(list, ct);
        return list;
    }

    public virtual void Update(T entity) => _dbSet.Update(entity);
    public virtual void Delete(T entity) => _dbSet.Remove(entity);
    public virtual void DeleteRange(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);

    public virtual async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken ct = default)
        => predicate is null ? await _dbSet.CountAsync(ct) : await _dbSet.CountAsync(predicate, ct);

    public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
        => await _dbSet.AnyAsync(predicate, ct);
}
