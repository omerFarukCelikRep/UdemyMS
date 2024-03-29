﻿using BlogApp.Core.DataAccess.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using UdemyMS.Common.Core.Entities;

namespace UdemyMS.Common.Core.Persistence.EFCore;
public class BaseRepository<TEntity, TId> :
    IAsyncRepository,
    IAsyncInsertableRepository<TEntity, TId>,
    IAsyncDeleteableRepository<TEntity, TId>,
    IAsyncUpdateableRepository<TEntity, TId>,
    IAsyncQueryableRepository<TEntity, TId>,
    IAsyncFindableRepository<TEntity, TId>,
    IAsyncOrderableRepository<TEntity, TId>
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

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? expression = null, CancellationToken cancellationToken = default)
    {
        return expression is null
            ? _table.AnyAsync(cancellationToken)
            : _table.AnyAsync(expression, cancellationToken);
    }

    public Task BulkDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return _table.Where(predicate)
                     .ExecuteDeleteAsync(cancellationToken);
    }

    public Task BulkUpdateAsync(Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls, CancellationToken cancellationToken = default)
    {
        return _table.ExecuteUpdateAsync(setPropertyCalls, cancellationToken);
    }

    public Task BulkUpdateAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls, CancellationToken cancellationToken = default)
    {
        return _table.Where(predicate)
                     .ExecuteUpdateAsync(setPropertyCalls, cancellationToken);
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

    public Task<IQueryable<TEntity>> GetAllAsync(bool tracking = true, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
            return Task.FromCanceled<IQueryable<TEntity>>(cancellationToken);

        var query = tracking
            ? _table
            : _table.AsNoTracking();

        return Task.FromResult(query);
    }

    public async Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true, CancellationToken cancellationToken = default)
    {
        var query = await GetAllAsync(tracking, cancellationToken);

        return query.Where(expression);
    }

    public async Task<IQueryable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, bool tracking = true, CancellationToken cancellationToken = default)
    {
        var query = await GetAllAsync(tracking, cancellationToken);
        return orderDesc
            ? query.OrderByDescending(orderby)
            : query.OrderBy(orderby);
    }

    public async Task<IQueryable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, int takeCount = 0, bool tracking = true, CancellationToken cancellationToken = default)
    {
        var query = await GetAllAsync(orderby, orderDesc, tracking, cancellationToken);
        return query.Take(takeCount);
    }

    public async Task<IQueryable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, bool tracking = true, CancellationToken cancellationToken = default)
    {
        var query = await GetAllAsync(expression, tracking, cancellationToken);
        return orderDesc
            ? query.OrderByDescending(orderby)
            : query.OrderBy(orderby);
    }

    public async Task<IQueryable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, int takeCount = 0, bool tracking = true, CancellationToken cancellationToken = default)
    {
        var query = await GetAllAsync(expression, orderby, orderDesc, tracking, cancellationToken);
        return query.Take(takeCount);
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true, CancellationToken cancellationToken = default)
    {
        var query = await GetAllAsync(tracking, cancellationToken);
        return await query.FirstOrDefaultAsync(expression, cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(TId id, bool tracking = true, CancellationToken cancellationToken = default)
    {
        var query = await GetAllAsync(tracking, cancellationToken);
        return await query.FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
            return Task.FromCanceled<TEntity>(cancellationToken);

        var entry = _table.Update(entity);
        return Task.FromResult(entry.Entity);
    }

    public Task UpdateRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
            return Task.FromCanceled(cancellationToken);

        _table.UpdateRange(entities);
        return Task.CompletedTask;
    }
}