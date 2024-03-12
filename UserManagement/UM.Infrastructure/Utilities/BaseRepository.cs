using Common.Domain;
using Common.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UM.Infrastructure.Persistent.EFCore;

namespace UM.Infrastructure.Utilities;

public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    protected readonly UserManagementContext Context;
    public BaseRepository(UserManagementContext context) => Context = context;


    #region Async Methods
    public virtual async Task<TEntity?> GetAsync(TKey id)
    {
        return await Context.Set<TEntity>().FirstOrDefaultAsync(t => t.Id.Equals(id)); ;
    }

    public virtual async Task<TEntity?> GetAsTrackingAsync(TKey id)
    {
        return await Context.Set<TEntity>().AsTracking().FirstOrDefaultAsync(t => t.Id.Equals(id));

    }
    public virtual async Task AddAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
    }

    public virtual async Task AddRangeAsync(ICollection<TEntity> entities)
    {
        await Context.Set<TEntity>().AddRangeAsync(entities);
    }

    public virtual async Task<int> SaveAsync()
    {
        return await Context.SaveChangesAsync();
    }
    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await Context.Set<TEntity>().AnyAsync(expression);
    }
    #endregion

    #region Sync Methods

    public virtual void Add(TEntity entity)
    {
        Context.Set<TEntity>().Add(entity);
    }
   
    public virtual void Update(TEntity entity)
    {
        Context.Update(entity);
    }
   
    public virtual bool Exists(Expression<Func<TEntity, bool>> expression)
    {
        return Context.Set<TEntity>().Any(expression);
    }

    public virtual TEntity? Get(TKey id)
    {
        return Context.Set<TEntity>().FirstOrDefault(t => t.Id.Equals(id)); ;
    }

   

    public void Save()
    {
        Context.SaveChanges();
    }

    #endregion
}