using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Evoflare.API.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly EvoflareDbContext _context;

        public Repository(EvoflareDbContext context)
        {
            _context = context;
        }

        public void DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
        }

        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            var result = await Task.Run(() => _context.Set<TEntity>());
            return result;
        }

        public async Task<TEntity> GetAsync(int id)
        {
            var result = await _context.Set<TEntity>().FindAsync(id);
            return result;
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            IQueryable<TEntity> set = _context.Set<TEntity>();

            foreach (var includeExpression in includeExpressions)
            {
                set = set.Include(includeExpression);
            }
            return set;
        }
    }
}
