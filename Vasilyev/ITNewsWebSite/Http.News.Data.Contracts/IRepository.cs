using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Http.News.Data.Contracts
{
    /// <summary>
    /// The Repository interface.
    /// </summary>
    public interface IRepository<T>
    {
        /// <summary>
        /// Gets entity by key.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="keyValue">The key value.</param>
        /// <returns></returns>
        T GetByKey<T>(object keyValue) where T : class;

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <returns></returns>
        IQueryable<T> GetQuery<T>() where T : class;

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        IQueryable<T> GetQuery<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        IQueryable<T> GetQuery<T>(ISpecification<T> criteria) where T : class;

        /// <summary>
        /// Gets one entity based on matching criteria
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        T Single<T>(Expression<Func<T, bool>> criteria) where T : class;

        /// <summary>
        /// Gets single entity using specification
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        T Single<T>(ISpecification<T> criteria) where T : class;

        /// <summary>
        /// Firsts the specified predicate.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        T First<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Gets first entity with specification.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        T First<T>(ISpecification<T> criteria) where T : class;

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        void Add<T>(T entity) where T : class;

        /// <summary>
        /// Attaches the specified entity.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        void Attach<T>(T entity) where T : class;

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        void Delete<T>(T entity) where T : class;

        /// <summary>
        /// Deletes one or many entities matching the specified criteria
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        void Delete<T>(Expression<Func<T, bool>> criteria) where T : class;

        /// <summary>
        /// Deletes entities which satify specificatiion
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        void Delete<T>(ISpecification<T> criteria) where T : class;

        /// <summary>
        /// Updates changes of the existing entity. 
        /// The caller must later call SaveChanges() on the repository explicitly to save the entity to database
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        void Update<T>(T entity) where T : class;

        /// <summary>
        /// Finds entities based on provided criteria.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        IEnumerable<T> Find<T>(ISpecification<T> criteria) where T : class;

        /// <summary>
        /// Finds entities based on provided criteria.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        IEnumerable<T> Find<T>(Expression<Func<T, bool>> criteria) where T : class;

        /// <summary>
        /// Finds one entity based on provided criteria.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        T FindOne<T>(ISpecification<T> criteria) where T : class;

        /// <summary>
        /// Finds one entity based on provided criteria.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        T FindOne<T>(Expression<Func<T, bool>> criteria) where T : class;

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAll<T>() where T : class;

        /// <summary>
        /// Gets the specified order by.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <typeparam name="TOrderBy">The type of the order by.</typeparam>
        /// <param name="orderBy">The order by.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns></returns>
        IEnumerable<T> Get<T, TOrderBy>(Expression<Func<T, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending) where T : class;

        /// <summary>
        /// Gets the specified criteria.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <typeparam name="TOrderBy">The type of the order by.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns></returns>
        IEnumerable<T> Get<T, TOrderBy>(Expression<Func<T, bool>> criteria, Expression<Func<T, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending) where T : class;

        /// <summary>
        /// Gets entities which satifies a specification.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <typeparam name="TOrderBy">The type of the order by.</typeparam>
        /// <param name="specification">The specification.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns></returns>
        IEnumerable<T> Get<T, TOrderBy>(ISpecification<T> specification, Expression<Func<T, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending) where T : class;

        /// <summary>
        /// Counts the specified entities.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <returns></returns>
        int Count<T>() where T : class;

        /// <summary>
        /// Counts entities with the specified criteria.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        int Count<T>(Expression<Func<T, bool>> criteria) where T : class;

        /// <summary>
        /// Counts entities satifying specification.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        int Count<T>(ISpecification<T> criteria) where T : class;

        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        /// <value>The unit of work.</value>
        IUnitOfWork UnitOfWork { get; }
    }
}
