using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Core.Common.ValidationError;

namespace Core.Utility.ValidationError
{
    /// <summary>
    /// Mvc裡面用ModelState作為儲存ValidationError的Dictionary
    /// </summary>
    public class ModelStateWrapper : IValidationDictionary
    {
        private ModelStateDictionary _modelState;

        /// <summary>
        /// 使用Mvc.ModelStateDictionary初始化 <see cref="ModelStateWrapper"/> 類別.
        /// </summary>
        /// <param name="modelState">Model State</param>
        public ModelStateWrapper(ModelStateDictionary modelState)
        {
            _modelState = modelState;
        }

        #region IValidationDictionary Members

        /// <summary>
        /// 記錄一筆錯誤訊息
        /// </summary>
        /// <param name="key">錯誤訊息的key。通常是property的名稱</param>
        /// <param name="errorMessage">錯誤訊息的描述</param>
        public void AddError(string key, string errorMessage)
        {
            _modelState.AddModelError(key, errorMessage);
        }

        /// <summary>
        /// 取得目前的情況，Validation有沒有通過
        /// </summary>
        /// <value>
        ///   <c>true</c> 表示有通過; 要不然就是, <c>false</c>.
        /// </value>
        public bool IsValid
        {
            get { return _modelState.IsValid; }
        }

        #endregion
    }
}
