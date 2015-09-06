using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Security
{
    /// <summary>
    /// 登入的幾種狀態
    /// </summary>
    public enum EnumLoginStatus
    {
        /// <summary>
        /// 登入成功
        /// </summary>
        [Display(Name = "登入成功")]
        Success,

        /// <summary>
        /// 登入失敗 - 帳號密碼錯誤
        /// </summary>
        [Display(Name = "登入失敗 - 帳號密碼錯誤")]
        WrongPassword,

        /// <summary>
        /// 登入失敗 - 帳號被停用
        /// </summary>
        [Display(Name = "登入失敗 - 帳號被停用")]
        NotEnabled
    }
}
