using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Evoflare.API.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Fields

        private readonly EvoflareDbContext _context;
        private DbSet<TEntity> _entities;

        #endregion

        #region Constructors

        public Repository(EvoflareDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public virtual TEntity GetById(object id)
        {
            return Entities.Find(id);
        }

        /// <summary>
        /// Get entity by identifier async
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await Entities.FindAsync(id);
        }

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Insert(TEntity entity)
        {
            Entities.Add(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Insert entity async
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual async Task InsertAsync(TEntity entity)
        {
            await Entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual TEntity Update(TEntity entity)
        {
            Entities.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        /// <summary>
        /// Update entity async
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            Entities.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Update(IEnumerable<TEntity> entities)
        {
            Entities.UpdateRange(entities);
            _context.SaveChanges();
        }

        /// <summary>
        /// Update entities async
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual async Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            Entities.UpdateRange(entities);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[] includes)
        {
            var queryableResultWithIncludes = includes
                .Aggregate(_context.Set<TEntity>().AsQueryable(),
                    (current, include) => current.Include(include));
            return await queryableResultWithIncludes.FirstOrDefaultAsync(criteria);
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Delete(TEntity entity)
        {
            Entities.Remove(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Delete entity async
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual async Task DeleteAsync(TEntity entity)
        {
            Entities.Remove(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            Entities.RemoveRange(entities);
            _context.SaveChanges();

        }

        /// <summary>
        /// Delete entities async
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual async Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            Entities.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public virtual IEnumerable<TEntity> GetEnumerable()
        {
            return Entities.AsEnumerable();
        }

        public virtual IAsyncEnumerable<TEntity> GetEnumerableAsync()
        {
            return Entities.ToAsyncEnumerable();
        }


        public virtual List<TEntity> GetList()
        {
            return Entities.ToList();
        }

        public virtual async Task<List<TEntity>> GetListAsync()
        {
            return await Entities.ToListAsync();
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> criteria)
        {
            return Entities.Where(criteria).ToList();
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> criteria)
        {
            return await Entities.Where(criteria).ToListAsync();
        }

        public List<TEntity> GetList(params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            var set = includeExpressions.Aggregate(Entities.AsQueryable(), (current, include) => current.Include(include));
            return set.ToList();
        }

        public async Task<List<TEntity>> GetListAsync(params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            var set = includeExpressions.Aggregate(Entities.AsQueryable(), (current, include) => current.Include(include));
            return await set.ToListAsync();
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            var set = includeExpressions.Aggregate(Entities.AsQueryable(), (current, include) => current.Include(include));
            return set.Where(criteria).ToList();
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            var set = includeExpressions.Aggregate(Entities.AsQueryable(), (current, include) => current.Include(include));
            return await set.Where(criteria).ToListAsync();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<TEntity> Table => Entities;

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        /// <summary>
        /// Gets an entity set
        /// </summary>
        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<TEntity>();

                return _entities;
            }
        }

        #endregion

    }
}
