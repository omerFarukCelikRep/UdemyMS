using UdemyMS.Common.Core.Entities;

namespace BlogApp.Core.DataAccess.Interfaces.Repositories;
public interface IDeleteableRepository<TEntity, TId>
    where TEntity : BaseEntity<TId>
    where TId : struct
{
    void Delete(TEntity entity);
    void Delete(TId id);
}