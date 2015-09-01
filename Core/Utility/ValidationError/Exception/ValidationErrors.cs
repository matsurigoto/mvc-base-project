using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.ValidationError;

namespace Core.Utility.ValidationError.Exception
{
    /// <summary>
    /// 代表有儲存錯誤訊息清單的自訂Exception Base
    /// </summary>
    public class ValidationErrors : System.Exception, IValidationErrors
    {
        /// <summary>
        /// 代表錯誤訊息的IList清單
        /// </summary>
        /// <value>
        /// 錯誤訊息的IList清單
        /// </value>
        public IList<IBaseError> Errors { get; set; }

        /// <summary>
        /// 初始化 <see cref="ValidationErrors"/> 類別
        /// </summary>
        public ValidationErrors()
        {
            this.Errors = new List<IBaseError>();
        }

        /// <summary>
        /// 使用錯誤訊息初始化 <see cref="ValidationErrors"/> 類別
        /// </summary>
        /// <param name="error">The error.</param>
        public ValidationErrors(IBaseError error)
            : this()
        {
            this.Errors.Add(error);
        }
    }
}
