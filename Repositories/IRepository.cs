using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Evoflare.API.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {

        TEntity GetById(object id);
        Task<TEntity> GetByIdAsync(object id);
        void Insert(TEntity entity);
        Task InsertAsync(TEntity entity);
        TEntity Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        void Update(IEnumerable<TEntity> entities);
        Task UpdateAsync(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);
        Task DeleteAsync(IEnumerable<TEntity> entities);

        IEnumerable<TEntity> GetEnumerable();
        IAsyncEnumerable<TEntity> GetEnumerableAsync();

        List<TEntity> GetList();
        Task<List<TEntity>> GetListAsync();

        List<TEntity> GetList(Expression<Func<TEntity, bool>> criteria);
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> criteria);

        List<TEntity> GetList(params Expression<Func<TEntity, object>>[] includeExpressions);
        Task<List<TEntity>> GetListAsync(params Expression<Func<TEntity, object>>[] includeExpressions);

        List<TEntity> GetList(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[] includeExpressions);
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[] includeExpressions);
    }
}
