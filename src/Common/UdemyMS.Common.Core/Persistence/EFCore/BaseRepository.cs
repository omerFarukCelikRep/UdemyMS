using BlogApp.Core.DataAccess.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UdemyMS.Common.Core.Entities;

namespace UdemyMS.Common.Core.Persistence.EFCore;
public class BaseRepository<TEntity, TId> :
    IAsyncRepository,
    IAsyncInsertableRepository<TEntity, TId>,
    IAsyncDeleteableRepository<TEntity, TId>
    where TEntity : BaseEntity<TId>
    where TId : struct
{
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _table;
    public BaseRepository(DbContext context)
    {
        _context = context;
        _table = _context.Set<TEntity>();
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var entry = await _table.AddAsync(entity, cancellationToken);
        return entry.Entity;
    }

    public Task AddRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default)
    {
        return _table.AddRangeAsync(entities, cancellationToken);
    }

    public Task BulkDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return _table.Where(predicate)
                     .ExecuteDeleteAsync(cancellationToken);
    }

    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
            return Task.FromCanceled(cancellationToken);

        _table.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(TId id, CancellationToken cancellationToken = default)
    {
        var entity = await _table.FindAsync(new object?[] { id }, cancellationToken) ?? throw new Exception(); //TODO : DBException

        await DeleteAsync(entity, cancellationToken);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}