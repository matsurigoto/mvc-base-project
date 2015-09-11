using System;
using System.Linq.Expressions;
using Core.SelectListFill;

namespace BaseProject.SelectListFill
{
    /// <summary>
    /// 方便建立用來產生DropDownList的SelectListViewModel - 針對已知的Table去寫，簡化呼叫的參數
    /// </summary>
    public static class SelectListViewModelHelper
    {
        /// <summary>
        /// 用Expression的方式來建立 <see cref="Core.SelectListFill.SelectListViewModel"/>。資料來源是Table Code
        /// </summary>
        /// <typeparam name="TModel">需要DropDownList的Model Type</typeparam>
        /// <param name="selectListId">DropDownList的值會存在的地方</param>
        /// <param name="selectedValue">要預設被選取的值</param>
        /// <returns></returns>
        public static SelectListViewModel GetSelectListViewModelForCode<TModel>
            (Expression<Func<TModel, object>> selectListId, Func<object> selectedValue)
        {
            return Core.SelectListFill.CoreSelectListViewModelHelper.GetSelectListViewModel<TModel, BaseProject.Model.Code>(selectListId,
                 y => y.Display, y => y.Value, selectedValue);
        }

        /// <summary>
        /// 用Expression的方式來建立 <see cref="Core.SelectListFill.SelectListViewModel"/>。資料來源是Table CodeMain
        /// </summary>
        /// <typeparam name="TModel">需要DropDownList的Model Type</typeparam>
        /// <param name="selectListId">DropDownList的值會存在的地方</param>
        /// <param name="selectedValue">要預設被選取的值</param>
        /// <returns></returns>
        public static SelectListViewModel GetSelectListViewModelForCodeMain<TModel>
            (Expression<Func<TModel, object>> selectListId, Func<object> selectedValue)
        {
            return Core.SelectListFill.CoreSelectListViewModelHelper.GetSelectListViewModel<TModel, BaseProject.Model.CodeMain>(selectListId,
                 y => y.Name, y => y.ID, selectedValue);
        }

    }
}
