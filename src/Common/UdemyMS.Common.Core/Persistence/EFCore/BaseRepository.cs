using BlogApp.Core.DataAccess.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace UdemyMS.Common.Core.Persistence.EFCore;
public class BaseRepository<TEntity> : IAsyncRepository where TEntity : class
{
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _table;
    public BaseRepository(DbContext context)
    {
        _context = context;
        _table = _context.Set<TEntity>();
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
