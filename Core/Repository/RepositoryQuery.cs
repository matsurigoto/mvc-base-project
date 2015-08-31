using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Common.Repository;

namespace Core.Repository
{
    /// <summary>
    /// 方便對Repository做Query的Class。讓query像是fluent的方式串接。
    /// </summary>
    /// <typeparam name="TEntity">同IRepository裡面的Type</typeparam>
    public sealed class RepositoryQuery<TEntity> : IRepositoryQuery<TEntity>
    {
        private readonly List<Expression<Func<TEntity, object>>>
            _includeProperties;

        private readonly IRepository<TEntity> _repository;

        private Expression<Func<TEntity, bool>> _filter;

        private Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> _orderByQuerable;

        private int? _page;

        private int? _pageSize;

        /// <summary>
        /// initialise Query
        /// </summary>
        /// <param name="repository">需要傳入要做query的IRepository</param>
        public RepositoryQuery(IRepository<TEntity> repository)
        {
            _repository = repository;
            _includeProperties =
                new List<Expression<Func<TEntity, object>>>();
        }

        /// <summary>
        /// 對資料做filte。輸入的將會用來做where條件。
        /// </summary>
        /// <param name="inFilter">where條件</param>
        /// <returns>返回RepositoryQuery，串接其他query項目。</returns>
        public IRepositoryQuery<TEntity> Filter(
            Expression<Func<TEntity, bool>> inFilter)
        {
            _filter = inFilter;
            return this;
        }

        /// <summary>
        /// 對資料做排序。
        /// </summary>
        /// <param name="inOrderBy">排序的邏輯。OrderBy條件</param>
        /// <returns>返回RepositoryQuery，串接其他query項目。</returns>
        public IRepositoryQuery<TEntity> OrderBy(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> inOrderBy)
        {
            _orderByQuerable = inOrderBy;
            return this;
        }

        /// <summary>
        /// 要包含的其他Table。
        /// </summary>
        /// <param name="expression">包含的table</param>
        /// <returns>返回RepositoryQuery，串接其他query項目。</returns>
        public IRepositoryQuery<TEntity> Include(
            Expression<Func<TEntity, object>> expression)
        {
            _includeProperties.Add(expression);
            return this;
        }

        /// <summary>
        /// 如果有做分頁，這邊取得分頁的資料。
        /// </summary>
        /// <param name="page">目前在第幾頁。</param>
        /// <param name="pageSize">一頁有幾筆。</param>
        /// <returns>
        /// Item1：返回符合所有條件的目前分頁內容。
        /// Item2：返回總數有幾筆資料。
        /// </returns>
        public Tuple<IEnumerable<TEntity>, int> GetByPage(int page, int pageSize)
        {
            _page = page;
            _pageSize = pageSize;
            int totalCount = _repository.Query(_filter).Count();

            return Tuple.Create(
                GetAll,
                totalCount);
        }

        /// <summary>
        /// 取得符合設定的資料。
        /// </summary>
        /// <returns>符合設定的資料。</returns>
        public IEnumerable<TEntity> GetAll
        {
            get
            {
                return _repository.Query(_filter,
                       _orderByQuerable, _includeProperties, _page, _pageSize);
            }
        }
    }
}
