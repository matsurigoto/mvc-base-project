using Core.Common.Repository;

namespace Core.Common.Business
{
    /// <summary>
    /// 處理刪除相關
    /// </summary>
    public interface IDeleteManyToManyProcess : IBusiness
    {
        /// <summary>
        /// 刪除Entity裡面的ManyToMany的關係（不只是刪除junction table的記錄，實際的
        /// 對應資料也會刪除）
        /// </summary>
        /// <typeparam name="TEntity">目前這一筆Entity的type</typeparam>
        /// <typeparam name="TManyToMany">要被刪除的Many to many的type</typeparam>
        /// <param name="uow">DB的Unit of work</param>
        /// <param name="main">要被刪除的Entity</param>
        void DeleteIncludeManyToManyRelationship<TEntity, TManyToMany>(IUnitOfWork uow, TEntity main)
            where TEntity : class
            where TManyToMany : class;
    }
}