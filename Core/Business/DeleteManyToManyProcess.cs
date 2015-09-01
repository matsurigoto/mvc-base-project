using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Business;
using Core.Utility;

namespace Core.Business
{
    /// <summary>
    /// 處理刪除相關
    /// </summary>
    public class DeleteManyToManyProcess : IDeleteManyToManyProcess
    {
        /// <summary>
        /// 從DB刪除目前的Entity，並且包含刪除裡面的ManyToMany的關係（不只是刪除junction table的記錄，實際的
        /// 對應資料也會刪除）
        /// </summary>
        /// <typeparam name="TEntity">目前這一筆Entity的type</typeparam>
        /// <typeparam name="TManyToMany">要被刪除的Many to many的type</typeparam>
        /// <param name="uow">DB的Unit of work</param>
        /// <param name="main">要被刪除的Entity</param>
        public void DeleteIncludeManyToManyRelationship<TEntity, TManyToMany>(Core.Common.Repository.IUnitOfWork uow,
            TEntity main)
            where TEntity : class
            where TManyToMany : class
        {
            DeleteOneTypeManyToManyProperty<TEntity, TManyToMany>(uow, main);

            // uow.Repository<TEntity>().Delete(main);
        }

        /// <summary>
        /// 只刪除某筆entity裡面many to many的實際table儲存資料。
        /// </summary>
        /// <typeparam name="TEntity">目前這一筆Entity的type</typeparam>
        /// <typeparam name="TManyToMany">要被刪除的Many to many的type</typeparam>
        /// <param name="uow">DB的Unit of work</param>
        /// <param name="main">要被刪除的Entity</param>
        private void DeleteOneTypeManyToManyProperty<TEntity, TManyToMany>(Core.Common.Repository.IUnitOfWork uow,
            TEntity main)
            where TEntity : class
            where TManyToMany : class
        {
             var properties = ReflectionHelper.GetPropertiesOfCurrentType(typeof(TEntity))
                .Where(x => typeof(ICollection<TManyToMany>).IsAssignableFrom(x.PropertyType));

            foreach (var property in properties)
            {
                var dos = (property.GetValue(main) as ICollection<TManyToMany>).ToList();

                if (dos != null && dos.Count > 0)
                {
                    foreach (var doValue in dos)
                    {
                         uow.Repository<TManyToMany>().Delete(doValue);
                    }
                }
            }
        }
    }
}
