using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Core.Common.Security
{
    /// <summary>
    /// 代表目前這個站台所有的Route對應的Resource和Operation。
    /// 此實作應該以DI用static的方式注入。
    /// </summary>
    public interface IResourceOperationCollection
    {
        /// <summary>
        /// 取得這個站台的Resource和Operation。
        /// </summary>
        /// <value>
        /// 這個站台的Resource和Operation。
        /// </value>
        List<ResourceOperation> ResourceOperation { get; }

        /// <summary>
        /// 取得或設定這個站台的Resource和Operation清單是否已經產生。
        /// </summary>
        /// <value>
        /// <c>true</c>這個站台的Resource和Operation清單已經產生。; 要不然, <c>false</c>.
        /// </value>
        bool IsResourceOperationPopulated { get; set; }

        /// <summary>
        /// 產生並且設定這個站台的Resource和Operation清單
        /// </summary>
        void PopulateResourceOperationCollection();

        /// <summary>
        /// 依照對應的Area、Controller和Action，取得對應的ResourceOperation
        /// </summary>
        /// <param name="areaName">Area名稱</param>
        /// <param name="controllerName">Controller名稱</param>
        /// <param name="actionName">Action名稱</param>
        /// <returns>這個Route對應的ResourceOperation</returns>
        ResourceOperation GetResourceOperation(string areaName, string controllerName, string actionName);

        /// <summary>
        /// 依照Request context，取得對應的ResourceOperation
        /// </summary>
        /// <param name="context">Request Context</param>
        /// <returns>這個Route對應的ResourceOperation</returns>
        ResourceOperation GetResourceOperation(HttpContextBase context);
    }
}
