using System.Linq.Expressions;

namespace Common.Domain.Repository;

public interface IBaseRepository<T, TKey> where T : BaseEntity<TKey>
{
    #region Async Methods
    Task<T?> GetAsync(TKey id);

    Task<T?> GetTracking(TKey id);

    Task AddAsync(T entity);

    Task AddRange(ICollection<T> entities);

    Task UpdateAsync(T entity);

    Task<int> SaveAsync();

    Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);

    #endregion

    #region Sync Methods

    void Add(T entity);

    void Update(T entity);

    void Save();

    bool Exists(Expression<Func<T, bool>> expression);

    T? Get(Guid id);

    #endregion
}
