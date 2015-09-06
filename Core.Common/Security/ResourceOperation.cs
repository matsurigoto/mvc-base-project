using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Security;

namespace Core.Common.Security
{
    /// <summary>
    /// 代表站台某一個Resource的名字和Operation需要的權限
    /// </summary>
    public class ResourceOperation
    {
        /// <summary>
        /// 實例化一個 <see cref="ResourceOperation"/> 物件.
        /// </summary>
        public ResourceOperation()
        {
        }

        /// <summary>
        /// 實例化一個和傳進來一樣的 <see cref="ResourceOperation"/> 物件.
        /// </summary>
        /// <param name="resourceOperation">The resource operation.</param>
        public ResourceOperation(ResourceOperation resourceOperation)
            : this(resourceOperation.ResourceName, resourceOperation.Operation, resourceOperation.AreaName,
             resourceOperation.ControllerName, 
             resourceOperation.ActionName, 
             resourceOperation.DisplayName,
             resourceOperation.DisplayNameWithParent)
        {
        }

        /// <summary>
        /// 實例化一個 <see cref="ResourceOperation" /> 物件.
        /// </summary>
        /// <param name="resourceName">Resource名稱</param>
        /// <param name="resourceOperation">Resource可以做的動作</param>
        /// <param name="areaName">對應的Area名稱</param>
        /// <param name="controllerName">對應的Controller名稱</param>
        /// <param name="actionName">對應的Action名稱</param>
        /// <param name="displayName">Resource顯示的名稱</param>
        /// <param name="displayNameWithParent">Resource顯示的名稱 - 包括parent的部分</param>
        public ResourceOperation(string resourceName, 
            ResourceOperationEnum resourceOperation,
            string areaName, string controllerName, 
            string actionName, string displayName,
            string displayNameWithParent)
        {
            ResourceName = resourceName;
            Operation = resourceOperation;
            AreaName = areaName;
            ControllerName = controllerName;
            ActionName = actionName;
            DisplayName = displayName;
            DisplayNameWithParent = displayNameWithParent;
        }

        /// <summary>
        /// 取得或設定Resource的名稱
        /// </summary>
        /// <value>
        /// Resource的名稱
        /// </value>
        public string ResourceName { get; set; }

        /// <summary>
        /// 取得或設定Resource顯示的名稱
        /// </summary>
        /// <value>
        /// Resource顯示的名稱
        /// </value>
        public string DisplayName { get; set; }

        /// <summary>
        /// 取得或設定Resource顯示的名稱 - 包括parent的部分
        /// </summary>
        /// <value>
        /// Resource顯示的名稱 - 包括parent的部分
        /// </value>
        public string DisplayNameWithParent { get; set; }

        /// <summary>
        /// 取得或設定此Resource要執行需要的Operation層級
        /// </summary>
        /// <value>
        /// 此Resource要執行需要的Operation層級
        /// </value>
        public ResourceOperationEnum Operation { get; set; }

        /// <summary>
        /// 取得或設定對應的Area名稱
        /// </summary>
        /// <value>
        /// 對應的Area名稱
        /// </value>
        public string AreaName { get; set; }

        /// <summary>
        /// 取得或設定對應的Controller名稱
        /// </summary>
        /// <value>
        /// 對應的Controller名稱
        /// </value>
        public string ControllerName { get; set; }

        /// <summary>
        /// 取得或設定對應的Action名稱
        /// </summary>
        /// <value>
        /// 對應的Action名稱
        /// </value>
        public string ActionName { get; set; }
    }
}
