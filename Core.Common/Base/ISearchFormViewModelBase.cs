namespace Core.Common.Base
{
    /// <summary>
    /// 搜索 Form 的 ViewModel interface。定義搜索必須要有的相關欄位。
    /// </summary>
    public interface ISearchFormViewModelBase
    {
        /// <summary>
        /// 取得或設定此搜索邏輯是要用Ascending還是Descending
        /// </summary>
        /// <value>
        /// <c>true</c> 搜索邏輯是要用Ascending; 要不然, <c>false</c>.
        /// </value>
        bool IsAscending { get; set; }

        /// <summary>
        /// 取得或設定要依照那個排序的欄位
        /// </summary>
        /// <value>
        /// 要依照那個排序的欄位
        /// </value>
        string OrderByColumnName { get; set; }

        /// <summary>
        /// 取得或設定目前頁數
        /// </summary>
        /// <value>
        /// 目前頁數
        /// </value>
        int Page { get; set; }

        /// <summary>
        /// 取得或設定每頁幾筆
        /// </summary>
        /// <value>
        /// 每頁幾筆
        /// </value>
        int PageSize { get; set; }
    }
}
