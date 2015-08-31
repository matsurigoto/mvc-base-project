namespace Core.Common.Base
{
    /// <summary>
    /// 搜索頁面的ViewModel需要實作的一個interface。
    /// 方便處理Paging和搜索條件相關。
    /// 這個方法就兩個Property，用作於表示搜索的form和搜索結果的result。
    /// </summary>
    /// <typeparam name="TSearchForm">搜索的form ViewModel type。必須是繼承<see cref="MvcInfrastructure.Common.Base.SearchFormViewModelBase"/></typeparam>
    /// <typeparam name="TPageResult">搜索的結果ViewModel type</typeparam>
    public interface ISearchViewModelBase<TSearchForm, TPageResult>
     where TSearchForm : Core.Common.Base.ISearchFormViewModelBase, new()
    {
        /// <summary>
        /// 取得或設定搜索結果
        /// </summary>
        /// <value>
        /// 搜索結果
        /// </value>
        PagedList.IPagedList<TPageResult> Result { get; set; }

        /// <summary>
        /// 取得或設定搜索的Form
        /// </summary>
        /// <value>
        /// 搜索的Form
        /// </value>
        TSearchForm SearchForm { get; set; }
    }
}
