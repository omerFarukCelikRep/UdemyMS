using System.Linq.Expressions;
using UdemyMS.Common.Core.Entities;

namespace BlogApp.Core.DataAccess.Interfaces.Repositories;
public interface IAsyncOrderableRepository<TEntity, TId> : IAsyncRepository
    where TEntity : BaseEntity<TId>
    where TId : struct
{
    Task<IQueryable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, bool tracking = true, CancellationToken cancellationToken = default);
    Task<IQueryable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, int takeCount = 0, bool tracking = true, CancellationToken cancellationToken = default);
    Task<IQueryable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, bool tracking = true, CancellationToken cancellationToken = default);
    Task<IQueryable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, int takeCount = 0, bool tracking = true, CancellationToken cancellationToken = default);
}