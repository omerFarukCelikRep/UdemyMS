﻿using System.Linq.Expressions;
using UdemyMS.Common.Core.Entities;

namespace BlogApp.Core.DataAccess.Interfaces.Repositories;
public interface IAsyncQueryableRepository<TEntity, TId> : IAsyncRepository
    where TEntity : BaseEntity<TId>
    where TId : struct
{
    Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = true, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true, CancellationToken cancellationToken = default);
}