using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Evoflare.API.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetAsync(int id);
        Task<IQueryable<TEntity>> GetAllAsync();
        Task InsertAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        void DeleteAsync(TEntity entity);
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeExpressions);
    }
}
