using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PagedList;

namespace Core.Base
{
    /// <summary>
    /// 搜索頁面的ViewModel需要繼承這一個Base。
    /// 方便處理Paging和搜索條件相關。
    /// 這個方法就兩個Property，用作於表示搜索的form和搜索結果的result。
    /// </summary>
    /// <typeparam name="TSearchForm">搜索的form ViewModel type。必須是繼承<see cref="MvcInfrastructure.Common.Base.SearchFormViewModelBase"/></typeparam>
    /// <typeparam name="TPageResult">搜索的結果ViewModel type</typeparam>
    public class SearchViewModelBase<TSearchForm, TPageResult> : CoreViewModelBase, Core.Common.Base.ISearchViewModelBase<TSearchForm, TPageResult>
        where TSearchForm : Core.Common.Base.ISearchFormViewModelBase, new()
    {
        private TSearchForm searchForm;

        /// <summary>
        /// 取得或設定搜索的Form。如果是null，會實例一個。
        /// </summary>
        /// <value>
        /// 搜索的Form
        /// </value>
        public TSearchForm SearchForm
        {
            get
            {
                if (this.searchForm == null)
                {
                    this.searchForm = new TSearchForm();
                }

                return this.searchForm;
            }

            set { this.searchForm = value; }
        }

        /// <summary>
        /// 取得或設定搜索結果的ViewModel。
        /// </summary>
        /// <value>
        /// 搜索結果的ViewModel。用<see cref="PagedList.IPagedList"/>包住，方便做分頁
        /// </value>
        public IPagedList<TPageResult> Result { get; set; }

        /// <summary>
        /// 取得或設定有被勾選的Id - 以,做區隔
        /// </summary>
        /// <value>
        /// 被勾選的Id - 以,做區隔
        /// </value>
        [DataType("CheckAllCheckbox")]
        public string SelectedId { get; set; }

        /// <summary>
        /// 取得或設定以清單的方式呈現勾選的Id
        /// </summary>
        /// <value>
        /// 以清單的方式呈現勾選的Id
        /// </value>
        [DataType("ItemCheckbox")]
        public List<string> SeletedIdInList
        {
            get
            {
                if (string.IsNullOrEmpty(SelectedId))
                {
                    return new List<string>();
                }
                else
                {
                    return SelectedId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                }
            }
        }
    }

    /// <summary>
    /// 搜索 Form 的 ViewModel base。定義搜索必須要有的相關欄位。
    /// </summary>
    public abstract class SearchFormViewModelBase : CoreViewModelBase, Core.Common.Base.ISearchFormViewModelBase
    {
        /// <summary>
        /// 目前頁數的值
        /// </summary>
        private int page;

        /// <summary>
        ///  取得或設定目前頁數。最小值是1。
        /// </summary>
        /// <value>
        /// 目前頁數
        /// </value>
        public virtual int Page
        {
            get
            {
                if (this.page < 1)
                {
                    this.page = 1;
                }

                return this.page;
            }

            set { this.page = value; }
        }

        /// <summary>
        /// 每頁筆數的值
        /// </summary>
        private int pageSize;

        /// <summary>
        /// 取得或設定每頁筆數。最小值是15。
        /// </summary>
        /// <value>
        /// 每頁筆數
        /// </value>
        public virtual int PageSize
        {
            get
            {
                if (this.pageSize < 1)
                {
                    this.pageSize = 15;
                }

                return this.pageSize;
            }

            set { this.pageSize = value; }
        }

        /// <summary>
        /// 欄位排序的值
        /// </summary>
        protected string orderByColumnName;

        /// <summary>
        /// 取得或設定要依照那個欄位做排序。
        /// </summary>
        /// <value>
        /// 依照那個欄位做排序.
        /// </value>
        public abstract string OrderByColumnName { get; set; }

        /// <summary>
        /// 取得或設定排序的方向。
        /// </summary>
        /// <value>
        /// <c>true</c> 表示用 ascending排序; otherwise, <c>false</c>.
        /// </value>
        public bool IsAscending { get; set; }
    }

    /// <summary>
    /// 搜索 Form 的 ViewModel base。有帶上形態，以第一個欄位做排序
    /// </summary>
    /// <typeparam name="T">Entity Framework裡面Table Entity</typeparam>
    public abstract class SearchFormViewModelBase<T> : SearchFormViewModelBase
    {
        /// <summary>
        /// 取得或設定要依照那個欄位做排序。
        /// </summary>
        /// <value>
        /// 依照那個欄位做排序.
        /// </value>
        public override string OrderByColumnName
        {
            get
            {
                if (string.IsNullOrEmpty(this.orderByColumnName))
                {
                    this.orderByColumnName = typeof(T).GetProperties().First().Name;
                }

                return this.orderByColumnName;
            }

            set
            {
                this.orderByColumnName = value;
            }
        }
    }
}
