using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper.QueryableExtensions;
using Core.Utility.Paging.Extension;
using PagedList;

namespace Core.Utility.Paging
{
    /// <summary>
    /// 方便把Search Form Viewmodel的OrderBy和Where條件apply上去
    /// </summary>
    public class SearchViewModelProcess
    {
        /// <summary>
        /// 把Search Form Viewmodel的OrderBy和Where條件apply上去。
        /// 會把最終內容儲存到SearchViewModelBase.Result裡面。
        /// </summary>
        /// <typeparam name="T">EF 的Entity</typeparam>
        /// <typeparam name="TSearchForm">Search Form ViewModel的Type</typeparam>
        /// <typeparam name="TPageResult">Search結果的VieModel type</typeparam>
        /// <param name="data">原始的IQueryable</param>
        /// <param name="searchForm">Search Form ViewModel</param>
        public static void ApplySearchForm<T, TSearchForm, TPageResult>(IQueryable<T> data, Common.Base.ISearchViewModelBase<TSearchForm, TPageResult> searchForm)
             where TSearchForm : Core.Common.Base.ISearchFormViewModelBase, new()
        {
            data = data.DynamicWhere(searchForm.SearchForm);

            ApplyOrderByAndToPageResult<T, TSearchForm, TPageResult>(data, searchForm);
        }

        /// <summary>
        /// TODO: 此方法需要重構
        /// 把Search Form Viewmodel的OrderBy和Where條件apply上去。
        /// 會把最終內容儲存到SearchViewModelBase.Result裡面。
        /// </summary>
        /// <typeparam name="T">EF 的Entity</typeparam>
        /// <typeparam name="TSearchForm">Search Form ViewModel的Type</typeparam>
        /// <typeparam name="TPageResult">Search結果的VieModel type</typeparam>
        /// <param name="data">原始的IQueryable</param>
        /// <param name="searchForm">SearchForm ViewModel</param>
        /// <param name="where">where 條件</param>
        public static void ApplySearchForm<T, TSearchForm, TPageResult>(IQueryable<T> data,
            Common.Base.ISearchViewModelBase<TSearchForm, TPageResult> searchForm,
            Expression<Func<T, bool>> where)
             where TSearchForm : Core.Common.Base.ISearchFormViewModelBase, new()
        {
            data = data.Where(where);

            ApplyOrderByAndToPageResult<T, TSearchForm, TPageResult>(data, searchForm);
        }

        /// <summary>
        /// 會過濾前台顯示需要的三個條件 - IsEnable和上下架的日期
        /// 會把最終內容儲存到SearchViewModelBase.Result裡面。
        /// </summary>
        /// <typeparam name="T">EF 的Entity</typeparam>
        /// <typeparam name="TSearchForm">Search Form ViewModel的Type</typeparam>
        /// <typeparam name="TPageResult">Search結果的VieModel type</typeparam>
        /// <param name="data">原始的IQueryable</param>
        /// <param name="searchForm">Search Form ViewModel</param>
        public static void ApplySearchFormForFront<T, TSearchForm, TPageResult>(IQueryable<T> data, Common.Base.ISearchViewModelBase<TSearchForm, TPageResult> searchForm)
             where TSearchForm : Core.Common.Base.ISearchFormViewModelBase, new()
        {
            data = data.WhereForFrontDisplay();

            ApplySearchForm<T, TSearchForm, TPageResult>(data, searchForm);
        }

        /// <summary>
        /// Apply Orderby並且把最後結果塞到SearchViewModelBase.Result裡面。
        /// </summary>
        /// <typeparam name="T">EF 的Entity</typeparam>
        /// <typeparam name="TSearchForm">Search Form ViewModel的Type</typeparam>
        /// <typeparam name="TPageResult">Search結果的VieModel type</typeparam>
        /// <param name="data">原始的IQueryable</param>
        /// <param name="searchForm">Search Form ViewModel</param>
        private static void ApplyOrderByAndToPageResult<T, TSearchForm, TPageResult>(IQueryable<T> data,
            Common.Base.ISearchViewModelBase<TSearchForm, TPageResult> searchForm) where TSearchForm : Common.Base.ISearchFormViewModelBase, new()
        {
            data = data.DynamicOrderBy(searchForm.SearchForm);
            searchForm.Result = data.Project().To<TPageResult>().ToPagedList(searchForm.SearchForm.Page, searchForm.SearchForm.PageSize);
        }
    }
}
