using System;
//using System.Data.Entity;

namespace Core.Common.Repository
{
    /// <summary>
    /// 實作Unit Of Work的interface。
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 儲存所有異動。
        /// </summary>
        void Save();

        /// <summary>
        /// 取得目前的Contex。
        /// </summary>
        //TODO: 加入資料庫連線的時候需要移除註解
        //DbContext DbContext { get; }

        /// <summary>
        /// 取得某一個Entity的Repository。
        /// 如果沒有取過，會initialise一個
        /// 如果有就取得之前initialise的那個。
        /// </summary>
        /// <typeparam name="T">此Context裡面的Entity Type</typeparam>
        /// <returns>Entity的Repository</returns>
        IRepository<T> Repository<T>() where T : class;
    }
}
