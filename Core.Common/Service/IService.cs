using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Common.ValidationError;

namespace Core.Common.Service
{
    /// <summary>
    /// Service服務層內容的Interface
    /// </summary>
    /// <typeparam name="T">主要要儲存的Entity Type</typeparam>
    public interface IService<T>
        where T : class
    {
        /// <summary>
        /// 取得目前的驗證錯誤清單集合
        /// </summary>
        /// <value>
        /// 驗證錯誤清單集合
        /// </value>
        IValidationDictionary ValidationDictionary { get; }

        /// <summary>
        /// 初始化IValidationDictionary
        /// </summary>
        /// <param name="inValidationDictionary">要用來儲存錯誤訊息的object</param>
        void InitialiseIValidationDictionary(IValidationDictionary inValidationDictionary);

        /// <summary>
        /// 處理在Index ViewModel所需要做的事情
        /// </summary>
        /// <typeparam name="TSearchForm">搜索form的形態</typeparam>
        /// <typeparam name="TPageResult">搜索結果的形態</typeparam>
        /// <param name="searchViewModel">搜索相關的viewmodel</param>
        /// <param name="wherePredicate">當不用預設的where處理邏輯的時候，傳入的自訂where條件</param>
        /// <param name="includes">需要Include進來的其他Entity</param>
        void ProcessIndexViewModel<TSearchForm, TPageResult>(Core.Common.Base.ISearchViewModelBase<TSearchForm, TPageResult> searchViewModel, Expression<Func<T, bool>> wherePredicate = null,
            params Expression<Func<T, object>>[] includes)
            where TSearchForm : Core.Common.Base.ISearchFormViewModelBase, new();

        /// <summary>
        /// 取得某一個條件下面的某一筆Entity
        /// </summary>
        /// <param name="wherePredicate">過濾邏輯</param>
        /// <param name="includes">需要Include的Entity</param>
        /// <returns>取得Entity或者是null</returns>
        T GetSpecificDetail(Expression<Func<T, bool>> wherePredicate,
            params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// 取得某一個條件下面的某一筆Entity並且轉成對應的ViewModel
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel的形態</typeparam>
        /// <param name="wherePredicate">過濾邏輯</param>
        /// <param name="includes">需要Include的Entity</param>
        /// <returns>取得轉換過的ViewModel或者是null</returns>
        TViewModel GetSpecificDetailToViewModel<TViewModel>(Expression<Func<T, bool>> wherePredicate,
            params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// 取得某一個條件下面的某一筆Entity並且確認符合前台顯示的條件（IsEnable，上下架時間）。
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel的形態</typeparam>
        /// <param name="wherePredicate">過濾邏輯</param>
        /// <param name="includes">需要Include的Entity</param>
        /// <returns>取得轉換過的ViewModel或者是null</returns>
        TViewModel GetSpecificDetailToViewModelForFront<TViewModel>(System.Linq.Expressions.Expression<Func<T, bool>> wherePredicate,
            params System.Linq.Expressions.Expression<Func<T, object>>[] includes);

        /// <summary>
        /// 依照某一個ViewModel的值，產生對應的Entity並且新增到資料庫
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel的形態</typeparam>
        /// <param name="viewModel">ViewModel的Reference</param>
        /// <returns>是否儲存成功</returns>
        bool CreateViewModelToDatabase<TViewModel>(TViewModel viewModel);

        /// <summary>
        /// 依照某一個ViewModel的值，產生對應的Entity並且新增到資料庫:客製路徑
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel的形態</typeparam>
        /// <param name="viewModel">ViewModel的Reference</param>
        /// <param name="path">path</param>
        /// <returns>是否儲存成功</returns>
        bool CreateViewModelToDatabase<TViewModel>(TViewModel viewModel, string path);

        /// <summary>
        /// 依照IList ViewModel的值，產生對應的Entity並且新增到資料庫
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel的形態</typeparam>
        /// <param name="viewModelList">要被儲存到DB裡面的ViewModel清單</param>
        /// <returns>是否儲存成功</returns>
        bool CreateListViewModelToDatabase<TViewModel>(IList<TViewModel> viewModelList);

        /// <summary>
        /// 依照某一個ViewModel的值，更新對應的Entity
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel的形態</typeparam>
        /// <param name="viewModel">ViewModel的值</param>
        /// <param name="wherePredicate">過濾條件 - 要被更新的那一筆過濾調價</param>
        /// <returns>是否刪除成功</returns>
        bool UpdateViewModelToDatabase<TViewModel>(TViewModel viewModel, Expression<Func<T, bool>> wherePredicate);

        /// <summary>
        /// 依照某一個ViewModel的值，更新對應的Entity:客製路徑
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel的形態</typeparam>
        /// <param name="viewModel">ViewModel的值</param>
        /// <param name="wherePredicate">過濾條件 - 要被更新的那一筆過濾調價</param>
        /// <param name="path">path</param>
        /// <returns>是否刪除成功</returns>
        bool UpdateViewModelToDatabase<TViewModel>(TViewModel viewModel, Expression<Func<T, bool>> wherePredicate, string path);

        /// <summary>
        /// 刪除某一筆Entity
        /// </summary>
        /// <param name="wherePredicate">過濾出要被刪除的Entity條件</param>
        /// <returns>是否刪除成功</returns>
        bool Delete(Expression<Func<T, bool>> wherePredicate);

        /// <summary>
        /// 刪除某一筆Entity，包含ManyToMany的relation的實際另一端Entity
        /// </summary>
        /// <typeparam name="TManyToMany">Many to many另外一端的形態</typeparam>
        /// <param name="wherePredicate">取得某一筆要被刪除的過濾條件</param>
        /// <param name="includes">Include進來的其他Entity</param>
        /// <returns>是否刪除成功</returns>
        bool DeleteWithManyToMany<TManyToMany>(Expression<Func<T, bool>> wherePredicate,
            params Expression<Func<T, object>>[] includes)
            where TManyToMany : class;

        /// <summary>
        /// 刪除某一筆Entity，包含ManyToMany的relation的實際另一端Entity
        /// </summary>
        /// <typeparam name="TManyToMany">Many to many另外一端的形態</typeparam>
        /// <typeparam name="TManyToMany2">Many to many2另外一端的形態</typeparam>
        /// <param name="wherePredicate">取得某一筆要被刪除的過濾條件</param>
        /// <param name="includes">Include進來的其他Entity</param>
        /// <returns>
        /// 是否刪除成功
        /// </returns>
        bool DeleteWithManyToMany<TManyToMany, TManyToMany2>(Expression<Func<T, bool>> wherePredicate,
            params Expression<Func<T, object>>[] includes)
            where TManyToMany : class
            where TManyToMany2 : class;

        /// <summary>
        /// 用IndexViewModel產生出要匯出的內容
        /// </summary>
        /// <typeparam name="TSearchForm">搜索form的形態</typeparam>
        /// <typeparam name="TPageResult">搜索結果的形態</typeparam>
        /// <param name="searchViewModel">搜索相關的viewmodel</param>
        /// <param name="wherePredicate">傳入要匯出的資料條件</param>
        /// <param name="includes">需要Include進來的其他Entity</param>
        void ProcessExportViewModel<TSearchForm, TPageResult>(Common.Base.ISearchViewModelBase<TSearchForm, TPageResult> searchViewModel,
            System.Linq.Expressions.Expression<Func<T, bool>> wherePredicate,
            params System.Linq.Expressions.Expression<Func<T, object>>[] includes)
            where TSearchForm : Common.Base.ISearchFormViewModelBase, new();
    }
}
