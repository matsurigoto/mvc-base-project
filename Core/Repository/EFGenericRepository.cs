using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Core.Common.Repository;

namespace Core.Repository
{
    /// <summary>
    /// 實作Entity Framework Generic Repository 的 Class。
    /// </summary>
    /// <typeparam name="TEntity">EF Model 裡面的Type</typeparam>
    public class EFGenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private DbContext Context { get; set; }

        /// <summary>
        /// 建構EF一個Entity的Repository，需傳入此Entity的Context。
        /// </summary>
        /// <param name="inContext">Entity所在的Context</param>
        public EFGenericRepository(DbContext inContext)
        {
            Context = inContext;
        }

        /// <summary>
        /// 新增一筆資料到資料庫。
        /// </summary>
        /// <param name="entity">要新增到資料的庫的Entity</param>
        public void Create(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// 取得第一筆符合條件的內容。如果符合條件有多筆，也只取得第一筆。
        /// </summary>
        /// <param name="predicate">要取得的Where條件。</param>
        /// <returns>取得第一筆符合條件的內容。</returns>
        public TEntity Read(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).FirstOrDefault();
        }

        /// <summary>
        /// 取得Entity全部筆數的IQueryable。
        /// </summary>
        /// <returns>Entity全部筆數的IQueryable。</returns>
        public IQueryable<TEntity> Reads()
        {
            return Context.Set<TEntity>().AsQueryable();
        }

        /// <summary>
        /// 更新一筆Entity內容。
        /// </summary>
        /// <param name="entity">要更新的內容</param>
        public void Update(TEntity entity)
        {
            Context.Entry<TEntity>(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// 更新一筆Entity的內容。只更新有指定的Property。
        /// </summary>
        /// <param name="entity">要更新的內容。</param>
        /// <param name="updateProperties">需要更新的欄位。</param>
        public void Update(TEntity entity, Expression<Func<TEntity, object>>[] updateProperties)
        {
            Context.Configuration.ValidateOnSaveEnabled = false;

            Context.Entry<TEntity>(entity).State = EntityState.Unchanged;

            if (updateProperties != null)
            {
                foreach (var property in updateProperties)
                {
                    Context.Entry<TEntity>(entity).Property(property).IsModified = true;
                }
            }
        }

        /// <summary>
        /// 刪除一筆資料內容。
        /// </summary>
        /// <param name="entity">要被刪除的Entity。</param>
        public void Delete(TEntity entity)
        {
            Context.Entry<TEntity>(entity).State = EntityState.Deleted;
        }

        /// <summary>
        /// 儲存異動。
        /// </summary>
        public void SaveChanges()
        {
            Context.SaveChanges();

            // 因為Update 單一model需要先關掉validation，因此重新打開
            if (Context.Configuration.ValidateOnSaveEnabled == false)
            {
                Context.Configuration.ValidateOnSaveEnabled = true;
            }
        }

        /// <summary>
        /// 清空Entity所有內容。
        /// </summary>
        public void DeleteAll()
        {
            ObjectContext objectContext = ((IObjectContextAdapter)Context).ObjectContext;

            string sql = objectContext.CreateObjectSet<TEntity>().ToTraceString();

            string matchWords = "FROM \"(?<schema>.*)\".\"(?<table>.*)\"\\s";

            Regex regex = new Regex(matchWords);
            Match match = regex.Match(sql);

            string finalSql = string.Format(CultureInfo.InvariantCulture, "truncate table \"{0}\".\"{1}\"", match.Groups["schema"].Value,
                match.Groups["table"].Value);

            Context.Database.ExecuteSqlCommand(finalSql);
        }

        /// <summary>
        /// 取得RepositoryQuery的形態。
        /// </summary>
        /// <returns>RepositoryQuery形態</returns>
        public IRepositoryQuery<TEntity> RepositoryQuery()
        {
            var repositoryGetFluentHelper =
            new RepositoryQuery<TEntity>(this);

            return repositoryGetFluentHelper;
        }

        /// <summary>
        /// 取得指定條件的資料。
        /// </summary>
        /// <param name="filter">資料的where條件。</param>
        /// <param name="orderBy">資料的排序方式。</param>
        /// <param name="includeProperties">資料需要join的table。</param>
        /// <param name="page">如果要做分頁，目前在第幾頁。</param>
        /// <param name="pageSize">如果要做分頁，一頁多少的筆數。</param>
        /// <returns>符合條件的資料。</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "忽略")]
        public IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, IList<Expression<Func<TEntity, object>>> includeProperties = null, int? page = null, int? pageSize = null)
        {
            IQueryable<TEntity> query = Reads();

            if (includeProperties != null)
            {
                foreach (var property in includeProperties)
                {
                    query = query.Include(property);
                }
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (page != null && pageSize != null)
            {
                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            return query.ToList();
        }
    }
}
