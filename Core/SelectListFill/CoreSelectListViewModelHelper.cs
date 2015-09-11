using System;
using System.Linq.Expressions;
using Core.Utility;

namespace Core.SelectListFill
{
    /// <summary>
    /// 方便建立用來產生DropDownList的SelectListViewModel
    /// </summary>
    public static class CoreSelectListViewModelHelper
    {
        /// <summary>
        /// 用Expression的方式來建立 <see cref="Core.SelectListFill.SelectListViewModel"/>
        /// </summary>
        /// <typeparam name="TModel">目標的Model形態。用來取得是那個Model Property需要DropDownList的值</typeparam>
        /// <typeparam name="TSourceModel">資料來源的Model形態。有Strong type取得對應的欄位</typeparam>
        /// <param name="selectListId">DropDownList值要對應到的Model Property</param>
        /// <param name="dataTextField">用作於顯示文字的欄位</param>
        /// <param name="dataValueField">用作於實際值的欄位</param>
        /// <param name="selectedValue">目前被選取的值</param>
        /// <returns><see cref="Core.SelectListFill.SelectListViewModel"/></returns>
        public static SelectListViewModel GetSelectListViewModel<TModel, TSourceModel>(Expression<Func<TModel, object>> selectListId,
            Expression<Func<TSourceModel, object>> dataTextField, Expression<Func<TSourceModel, object>> dataValueField,
            Func<object> selectedValue)
        {
            return new SelectListViewModel()
            {
                Source = typeof(TSourceModel).Name,
                SelectListId = CoreExpressionHelper.GetExpressionText<TModel>(selectListId),
                SelectedValue = selectedValue.Invoke(),
                DataTextField = CoreExpressionHelper.GetExpressionText<TSourceModel>(dataTextField),
                DataValueField = CoreExpressionHelper.GetExpressionText<TSourceModel>(dataValueField)
            };
        }
    }
}
