using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.ValidationError;

namespace Core.Utility.ValidationError
{
    /// <summary>
    /// 代表一個Property的錯誤訊息
    /// </summary>
    public class PropertyError : IBaseError
    {
        /// <summary>
        /// 代表有錯誤的Property名稱
        /// </summary>
        /// <value>
        /// 有錯誤的Property名稱
        /// </value>
        public string PropertyName { get; set; }

        /// <summary>
        /// 代表錯誤的Property原因內容
        /// </summary>
        /// <value>
        /// 錯誤的Property原因內容
        /// </value>
        public string PropertyExceptionMessage { get; set; }

        /// <summary>
        /// 使用錯誤的Property欄位和錯誤的訊息，初始化 <see cref="PropertyError"/> 類別
        /// </summary>
        /// <param name="propertyName">錯誤的Property欄位名稱</param>
        /// <param name="errorMessage">錯誤訊息</param>
        public PropertyError(string propertyName, string errorMessage)
        {
            this.PropertyName = propertyName;
            this.PropertyExceptionMessage = errorMessage;
        }
    }
}
