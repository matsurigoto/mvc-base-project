using System;

namespace Core.Common.Repository
{
    /// <summary>
    /// 把Repository轉換成用Fluent的方式來下Query的class interface
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IRepositoryQuery<TEntity>
    {
        /// <summary>
        /// 對資料做filte。輸入的將會用來做where條件。
        /// </summary>
        /// <param name="inFilter">where條件</param>
        /// <returns>返回RepositoryQuery，串接其他query項目。</returns>
        IRepositoryQuery<TEntity> Filter(System.Linq.Expressions.Expression<Func<TEntity, bool>> inFilter);

        /// <summary>
        /// 取得符合設定的資料。
        /// </summary>
        /// <returns>符合設定的資料。</returns>
        System.Collections.Generic.IEnumerable<TEntity> GetAll { get; }

        /// <summary>
        /// 如果有做分頁，這邊取得分頁的資料。
        /// </summary>
        /// <param name="page">目前在第幾頁。</param>
        /// <param name="pageSize">一頁有幾筆。</param>
        /// <returns>
        /// Item1：返回符合所有條件的目前分頁內容。
        /// Item2：返回總數有幾筆資料。
        /// </returns>
        Tuple<System.Collections.Generic.IEnumerable<TEntity>, int> GetByPage(int page, int pageSize);

        /// <summary>
        /// 要包含的其他Table。
        /// </summary>
        /// <param name="expression">包含的table</param>
        /// <returns>返回RepositoryQuery，串接其他query項目。</returns>
        IRepositoryQuery<TEntity> Include(System.Linq.Expressions.Expression<Func<TEntity, object>> expression);

        /// <summary>
        /// 對資料做排序。
        /// </summary>
        /// <param name="inOrderBy">排序的邏輯。OrderBy條件</param>
        /// <returns>返回RepositoryQuery，串接其他query項目。</returns>
        IRepositoryQuery<TEntity> OrderBy(Func<System.Linq.IQueryable<TEntity>, System.Linq.IOrderedQueryable<TEntity>> inOrderBy);
    }
}
