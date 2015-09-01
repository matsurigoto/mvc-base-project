using System.Web.Routing;
using AutoMapper.Internal;
using Core.Base;
using Core.Utility;
using Core.Utility.Extension;

namespace Core.Utility.Paging
{
    /// <summary>
    /// SearchForm在產生Paging的時候，一些helper方法
    /// </summary>
    public static class PagingHelper
    {
        /// <summary>
        /// 取得或設定SearchModel的property前置詞
        /// </summary>
        /// <value>
        /// SearchModel的property前置詞
        /// </value>
        public static string PropertyNamePrefix
        {
            get
            {
                return "SearchForm.";
            }
        }

        /// <summary>
        /// 依照Search Form來產生 Route Value Dictionary
        /// </summary>
        /// <param name="model">Search Form的ViewModel</param>
        /// <param name="page">那一頁</param>
        /// <param name="pageSize">頁數</param>
        /// <returns>
        /// 返回產生的RouteValueDictionary
        /// </returns>
        public static RouteValueDictionary GenRVDBaseOnSearchFormModel(SearchFormViewModelBase model, int? page = null, int? pageSize = null)
        {
            RouteValueDictionary rvd = new RouteValueDictionary();

            rvd.Add(PropertyNamePrefix + "Page", page ?? model.Page);
            rvd.Add(PropertyNamePrefix + "PageSize", pageSize ?? model.PageSize);

            // TODO: 這邊的取得property 邏輯和 Core.Paging.Extension.DynamicQueryExtension.DynamicWhere 裡面一樣，有時間要抽出來共用

            return GenRVDForSearchModel(model, rvd);
        }

        /// <summary>
        /// 依照SearchModel的值，產生出RouteValueDictionary
        /// </summary>
        /// <param name="model">SearchModel的Instance</param>
        /// <param name="rvd">需要增加到RouteValueDictionary的額外值</param>
        /// <returns>返回產生的RouteValueDictionary</returns>
        public static RouteValueDictionary GenRVDForSearchModel(object model, RouteValueDictionary rvd = null)
        {
            if (rvd == null)
            {
                rvd = new RouteValueDictionary();
            }

            var properties = ReflectionHelper.GetPropertiesOfCurrentType(model.GetType());

            for (int i = 0; i < properties.Length; i++)
            {
                var value = properties[i].GetValue(model);

                if (string.IsNullOrEmpty(value.NonNullString()) == false)
                {
                    rvd.Add(PropertyNamePrefix + properties[i].Name, value);
                }
            }

            return rvd;
        }
    }
}
