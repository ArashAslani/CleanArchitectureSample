using System.Linq.Expressions;

namespace Common.Domain.Repository;

public interface IBaseRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    #region Async Methods
    Task<TEntity?> GetAsync(TKey id);

    Task<TEntity?> GetAsTrackingAsync(TKey id);

    Task AddAsync(TEntity entity);

    Task AddRangeAsync(ICollection<TEntity> entities);

    Task<int> SaveAsync();

    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);

    #endregion

    #region Sync Methods

    void Add(TEntity entity);

    void Update(TEntity entity);

    void Save();

    bool Exists(Expression<Func<TEntity, bool>> expression);

    TEntity? Get(TKey id);

    #endregion
}
