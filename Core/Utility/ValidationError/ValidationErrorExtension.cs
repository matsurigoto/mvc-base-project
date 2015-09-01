using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.ValidationError;

namespace Core.Utility.ValidationError
{
    /// <summary>
    /// Validation有錯誤的時候，一些helper的extension方法
    /// </summary>
    public static class ValidationErrorExtension
    {
        /// <summary>
        /// 把錯誤訊息記錄到IValidationDictionary的物件裡面
        /// </summary>
        /// <param name="validationDictionary">儲存目前Validation結果的Dictionary</param>
        /// <param name="propertyErrors">要記錄到ValidationDictionary裡面的錯誤訊息</param>
        public static void AddValidationErrors(this IValidationDictionary validationDictionary, IValidationErrors propertyErrors)
        {
            foreach (var databaseValidationError in propertyErrors.Errors)
            {
                validationDictionary.AddError(databaseValidationError.PropertyName, databaseValidationError.PropertyExceptionMessage);
            }
        }
    }
}
