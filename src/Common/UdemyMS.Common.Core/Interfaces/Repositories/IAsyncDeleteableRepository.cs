using System.Linq.Expressions;
using UdemyMS.Common.Core.Entities;

namespace BlogApp.Core.DataAccess.Interfaces.Repositories;
public interface IAsyncDeleteableRepository<TEntity, TId> : IAsyncRepository
    where TEntity : BaseEntity<TId>
    where TId : struct
{
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TId id, CancellationToken cancellationToken = default);
    Task BulkDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
}