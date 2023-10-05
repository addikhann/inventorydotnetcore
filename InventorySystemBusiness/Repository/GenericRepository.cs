using InventorySystemBusiness.EntityContaxt;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;

namespace InventorySystemBusiness.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, new()
    {
        internal CoreDbContext _context;
        internal DbSet<TEntity> _dbSet;

        public GenericRepository(CoreDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public CoreDbContext GetContext()
        {
            return _context;
        }
        public IEnumerable<TEntity> AllProxy()
        {
            return _dbSet.ToList();
        }

        public IEnumerable<TEntity> All()
        {
            return _dbSet.ToList();
        }
        public async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public IEnumerable<TEntity> AllInclude
            (params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return GetAllIncluding(includeProperties).ToList();
        }

        public async Task<IEnumerable<TEntity>> AllIncludeAsync
           (params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await GetAllIncluding(includeProperties).ToListAsync();
        }
        public IEnumerable<TEntity> FindByInclude
        (Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = GetAllIncluding(includeProperties);
            IEnumerable<TEntity> results = query.Where(predicate).ToList();
            return results;
        }

        public async Task<IEnumerable<TEntity>> FindByIncludeAsync
     (Expression<Func<TEntity, bool>> predicate,
         params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = GetAllIncluding(includeProperties);
            IEnumerable<TEntity> results = await query.Where(predicate).ToListAsync();
            return results;
        }

        private IQueryable<TEntity> GetAllIncluding
            (params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = _dbSet;

            return includeProperties.Aggregate
                (queryable, (current, includeProperty) => current.Include(includeProperty));
        }


        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            IEnumerable<TEntity> results = _dbSet
                .Where(predicate).ToList();
            return results;
        }
        public async Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            IEnumerable<TEntity> results = await _dbSet
                .Where(predicate).ToListAsync();
            return results;
        }
        public TEntity FindFirstBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet
                .SingleOrDefault(predicate);
        }

        public async Task<TEntity> FindFirstByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet
                .SingleOrDefaultAsync(predicate).ConfigureAwait(false);
        }
        public TEntity FindByKey<TKey>(TKey id)
        {
            Expression<Func<TEntity, bool>> lambda = Helper.BuildLambdaForFindByKey<TEntity, TKey>(id);

            return _dbSet.SingleOrDefault(lambda);
        }
        public async Task<TEntity> FindByKeyAsync<TKey>(TKey id)
        {
            Expression<Func<TEntity, bool>> lambda = Helper.BuildLambdaForFindByKey<TEntity, TKey>(id);

            return await _dbSet.SingleOrDefaultAsync(lambda).ConfigureAwait(false); ;
        }

        public void Insert(TEntity entity)
        {
            try
            {
                _dbSet.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task InsertManyAsync(TEntity[] entity)
        {
            try
            {
                await _dbSet.AddRangeAsync(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void InsertManyAsync2(TEntity[] entity)
        {
            try
            {
                _dbSet.AddRange(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task InsertAsync(TEntity entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            _context.SaveChanges();
            //detach after saving changes so that it can be saved again in the same session
            _context.Entry(entity).State = EntityState.Detached;

        }


        public async Task<int> UpdateAsync(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            int objectsChanged = await _context.SaveChangesAsync();
            //detach after saving changes so that it can be saved again in the same session
            _context.Entry(entity).State = EntityState.Detached;

            return objectsChanged;
        }

        private void Delete(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();

        }

        private void DeleteAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChangesAsync();

        }

        public void Delete<T>(T id)
        {
            var entity = FindByKey(id);
            _dbSet.Attach(entity);
            Delete(entity);
        }

        public void DeleteAsync<T>(T id)
        {
            var entity = FindByKey(id);
            _dbSet.Attach(entity);
            DeleteAsync(entity);
        }

        async Task IGenericRepository<TEntity>.DeleteAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }

    internal class Helper
    {
        public static Expression<Func<TEntity, bool>> BuildLambdaForFindByKey<TEntity, T>(T id)
        {
            var item = Expression.Parameter(typeof(TEntity), "entity");
            var prop = Expression.Property(item, "Id");
            var value = Expression.Constant(id);
            var equal = Expression.Equal(prop, value);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equal, item);
            return lambda;
        }

    }
}
