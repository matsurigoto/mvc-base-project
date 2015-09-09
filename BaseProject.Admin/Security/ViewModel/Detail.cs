using Core.Common.Security;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.Admin.Security.ViewModel
{
    /// <summary>
    /// 詳細頁的ViewModel
    /// </summary>
    public class Detail
    {
        /// <summary>
        /// 取得或設定功能權限
        /// </summary>
        /// <value>
        /// 功能權限
        /// </value>
        [Display(Name = "功能權限")]
        public List<ResourceOperation> ResourceOperation { get; set; }

        /// <summary>
        /// 取得或設定群組名稱
        /// </summary>
        /// <value>
        /// 群組名稱
        /// </value>
        [Display(Name = "群組名稱")]
        public string RoleName { get; set; }

        /// <summary>
        /// 取得或設定群組Id
        /// </summary>
        /// <value>
        /// 群組Id
        /// </value>
        public string RoleId { get; set; }

        /// <summary>
        /// 取得或設定群組所有使用者
        /// </summary>
        /// <value>
        /// 群組所有使用者
        /// </value>
        public Dictionary<string, string> Users { get; set; }
    }
}
