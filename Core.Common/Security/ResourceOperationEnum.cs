using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Security
{
    /// <summary>
    /// Resource Operation的層級
    /// </summary>
    [Flags]
    public enum ResourceOperationEnum
    {
        /// <summary>
        /// 代表不需要任何權限
        /// </summary>
        None = 0,

        /// <summary>
        /// 代表新增權限
        /// </summary>
        [Display(Name = "新增")]
        Create = 1,

        /// <summary>
        /// 代表讀取的權限
        /// </summary>
        [Display(Name = "讀取")]
        Read = 2,

        /// <summary>
        /// 代表更新的權限
        /// </summary>
        [Display(Name = "修改")]
        Update = 4,

        /// <summary>
        /// 代表刪除的權限
        /// </summary>
        [Display(Name = "刪除")]
        Delete = 8
    }
}
