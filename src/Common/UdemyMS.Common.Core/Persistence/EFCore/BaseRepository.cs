using BlogApp.Core.DataAccess.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using UdemyMS.Common.Core.Entities;

namespace UdemyMS.Common.Core.Persistence.EFCore;
public class BaseRepository<TEntity, TId> :
    IAsyncRepository,
    IAsyncInsertableRepository<TEntity, TId>
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


    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}