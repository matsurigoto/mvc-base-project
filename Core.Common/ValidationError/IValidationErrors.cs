using System.Collections.Generic;

namespace Core.Common.ValidationError
{
    /// <summary>
    /// 代表有儲存錯誤訊息清單的Interface
    /// </summary>
    public interface IValidationErrors
    {
        /// <summary>
        /// 代表錯誤訊息的IList清單
        /// </summary>
        /// <value>
        /// 錯誤訊息的IList清單
        /// </value>
        IList<IBaseError> Errors { get; set; }
    }
}
