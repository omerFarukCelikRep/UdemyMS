using BlogApp.Core.DataAccess.Interfaces.Models;
using System.Linq.Expressions;
using UdemyMS.Common.Core.Entities;

namespace BlogApp.Core.DataAccess.Interfaces.Repositories;
public interface IAsyncPaginateRepository<TEntity, TId> : IAsyncRepository
    where TEntity : BaseEntity<TId>
    where TId : struct
{
    Task<IPaginate<TEntity>> GetAllAsPaginateAsync(int index = 0, int size = 10, bool tracking = true, CancellationToken cancellationToken = default);
    Task<IPaginate<TEntity>> GetAllAsPaginateAsync(Expression<Func<TEntity, bool>> expression, int index = 0, int size = 10, bool tracking = true, CancellationToken cancellationToken = default);
}