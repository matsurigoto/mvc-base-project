using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility.ValidationError.Exception
{
    /// <summary>
    /// 當EntityFramework驗證出錯的時候，會丟出這一個Custom的Exception
    /// </summary>
    public class DatabaseValidationErrors : ValidationErrors
    {
        /// <summary>
        /// 使用錯誤訊息，初始化 <see cref="DatabaseValidationErrors"/> 類別的執行個體
        /// </summary>
        /// <param name="errors">錯誤訊息</param>
        public DatabaseValidationErrors(IEnumerable<DbEntityValidationResult> errors)
            : base()
        {
            foreach (var err in errors.SelectMany(dbEntityValidationResult => dbEntityValidationResult.ValidationErrors))
            {
                Errors.Add(new PropertyError(err.PropertyName, err.ErrorMessage));
            }
        }
    }
}
