using InventorySystemBusiness.EntityContaxt;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemBusiness.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// Get all entity objects of the generic type from database
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> All();
        IEnumerable<TEntity> AllProxy();
        Task<IEnumerable<TEntity>> AllAsync();

        /// <summary>
        /// Get entities based on filter predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> AllIncludeAsync
           (params Expression<Func<TEntity, object>>[] includeProperties);
        /// <summary>
        /// Get the entity object by a generic primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity FindByKey<TKey>(TKey id);
        Task<TEntity> FindByKeyAsync<TKey>(TKey id);
        /// <summary>
        /// Get first or default with expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity FindFirstBy(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FindFirstByAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Generic repository method to insert entity in database and create a message in queue for auditing
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userId">This is the userId of the user performing the task</param>
        void Insert(TEntity entity);
        Task InsertManyAsync(TEntity[] entity);

        void InsertManyAsync2(TEntity[] entity);
        Task InsertAsync(TEntity entity);
        /// <summary>
        /// Generic repository method to update entity in database and create a message in queue for auditing
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userId">This is the userId of the user performing the task</param>
        void Update(TEntity entity);

        /// <summary>
        /// Generic async repository method to update entity in database and create a message in queue for auditing
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userId">This is the userId of the user performing the task</param>
        /// <returns></returns>
        Task<int> UpdateAsync(TEntity entity);

        /// <summary>
        /// Generic repository method to delete entity in database and create a message in queue for auditing
        /// </summary>
        /// <param name="id"></param>        
        void Delete<T>(T id);
        // void DeleteAsync<T>(T id);
        //   void DeleteAsync(TEntity entity);
        CoreDbContext GetContext();
        Task DeleteAsync(TEntity entity);
    }
}
