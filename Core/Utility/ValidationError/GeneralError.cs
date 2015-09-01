using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.ValidationError;

namespace Core.Utility.ValidationError
{
    /// <summary>
    /// 代表一個一般的錯誤訊息。只有錯誤訊息而PropertyName是空值。
    /// </summary>
    public class GeneralError : IBaseError
    {
        /// <summary>
        /// 代表有錯誤的Property名稱
        /// </summary>
        /// <value>
        /// 有錯誤的Property名稱
        /// </value>
        public string PropertyName
        {
            get
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 代表錯誤的Property原因內容
        /// </summary>
        /// <value>
        /// 錯誤的Property原因內容
        /// </value>
        public string PropertyExceptionMessage { get; set; }

        /// <summary>
        /// 使用錯誤訊息描述初始化 <see cref="GeneralError"/> 類別
        /// </summary>
        /// <param name="errorMessage">錯誤訊息描述</param>
        public GeneralError(string errorMessage)
        {
            this.PropertyExceptionMessage = errorMessage;
        }
    }
}
