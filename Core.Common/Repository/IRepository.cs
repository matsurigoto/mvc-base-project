using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Common.Repository
{
    /// <summary>
    /// 代表一個Repository的interface。
    /// </summary>
    /// <typeparam name="T">任意model的class</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// 新增一筆資料。
        /// </summary>
        /// <param name="entity">要新增到的Entity</param>
        void Create(T entity);

        /// <summary>
        /// 取得第一筆符合條件的內容。如果符合條件有多筆，也只取得第一筆。
        /// </summary>
        /// <param name="predicate">要取得的Where條件。</param>
        /// <returns>取得第一筆符合條件的內容。</returns>
        T Read(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 取得Entity全部筆數的IQueryable。
        /// </summary>
        /// <returns>Entity全部筆數的IQueryable。</returns>
        IQueryable<T> Reads();

        /// <summary>
        /// 更新一筆資料的內容。
        /// </summary>
        /// <param name="entity">要更新的內容</param>
        void Update(T entity);

        /// <summary>
        /// 更新一筆資料的內容。只更新有指定的Property。
        /// </summary>
        /// <param name="entity">要更新的內容。</param>
        /// <param name="updateProperties">需要更新的欄位。</param>
        void Update(T entity, params Expression<Func<T, object>>[] updateProperties);

        /// <summary>
        /// 刪除一筆資料內容。
        /// </summary>
        /// <param name="entity">要被刪除的Entity。</param>
        void Delete(T entity);

        /// <summary>
        /// 清空所有內容。
        /// </summary>
        void DeleteAll();

        /// <summary>
        /// 儲存異動。
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// 取得RepositoryQuery的形態。
        /// </summary>
        /// <returns>RepositoryQuery形態</returns>
        IRepositoryQuery<T> RepositoryQuery();

        /// <summary>
        /// 取得指定條件的資料。
        /// </summary>
        /// <param name="filter">資料的where條件。</param>
        /// <param name="orderBy">資料的排序方式。</param>
        /// <param name="includeProperties">資料需要join的table。</param>
        /// <param name="page">如果要做分頁，目前在第幾頁。</param>
        /// <param name="pageSize">如果要做分頁，一頁多少的筆數。</param>
        /// <returns>符合條件的資料。</returns>
        IEnumerable<T> Query(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>,
            IOrderedQueryable<T>> orderBy = null,
        IList<Expression<Func<T, object>>>
            includeProperties = null,
        int? page = null,
        int? pageSize = null);
    }
}
