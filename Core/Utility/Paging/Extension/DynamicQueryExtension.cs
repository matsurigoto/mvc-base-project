using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;
using AutoMapper.Internal;
using Core.Base;
using Core.Common.Base;
using Core.Common.Service;
using Core.Utility;
using Core.Utility.Extension;

namespace Core.Utility.Paging.Extension
{
    /// <summary>
    /// 把<see cref="MvcInfrastructure.Common.Base.SearchFormViewModelBase"/> 裡面的值，透過Dynamic Linq
    /// 把where條件和orderBy都apply上去。
    /// </summary>
    public static class DynamicQueryExtension
    {
        /// <summary>
        /// 依照Search Form ViewModel的值來設定OrderBy欄位
        /// </summary>
        /// <typeparam name="T">通常是EF的Entity</typeparam>
        /// <param name="data">要被處理的資料</param>
        /// <param name="searchForm">Search Form Viewmodel的值</param>
        /// <returns>有增加OrderBy的IQueryable</returns>
        public static IQueryable<T> DynamicOrderBy<T>(this IQueryable<T> data, ISearchFormViewModelBase searchForm)
        {
            return (IQueryable<T>)DynamicOrderBy((IQueryable)data, searchForm);
        }

        /// <summary>
        /// 依照Search Form ViewModel的值來設定OrderBy欄位
        /// </summary>
        /// <param name="data">要被處理的資料</param>
        /// <param name="searchForm">Search Form Viewmodel的值</param>
        /// <returns>有增加OrderBy的IQueryable</returns>
        public static IQueryable DynamicOrderBy(this IQueryable data, ISearchFormViewModelBase searchForm)
        {
            if (searchForm.IsAscending)
            {
                return data.OrderBy(searchForm.OrderByColumnName);
            }
            else
            {
                return data.OrderBy(searchForm.OrderByColumnName + " descending");
            }
        }

        /// <summary>
        /// 依照Search Form ViewModel的值來設定Where的內容。
        /// </summary>
        /// <typeparam name="T">通常是EF的Entity</typeparam>
        /// <param name="data">要被處理的資料</param>
        /// <param name="searchForm">Search Form Viewmodel的值</param>
        /// <returns>有增加OrderBy的IQueryable</returns>
        public static IQueryable<T> DynamicWhere<T>(this IQueryable<T> data, ISearchFormViewModelBase searchForm)
        {
            var properties = ReflectionHelper.GetPropertiesOfCurrentType(searchForm.GetType());

            string whereCalus = string.Empty;
            string andOperator = string.Empty;
            string operatorOr = string.Empty;

            List<object> propertiesValue = new List<object>();

            for (int i = 0; i < properties.Length; i++)
            {
                var value = properties[i].GetValue(searchForm);

                if (string.IsNullOrEmpty(value.NonNullString()) == false)
                {
                    // 如果形態是string，就用Like方式查詢
                    if (properties[i].PropertyType == typeof(string))
                    {
                        whereCalus = string.Format("{0}{1} {2}.Contains(@{3})", whereCalus, andOperator,
                                                properties[i].Name, propertiesValue.Count);
                    }

                    // 如果形態是int，就用等於查詢
                    if (properties[i].PropertyType == typeof(int))
                    {
                        whereCalus = string.Format("{0}{1} {2} = @{3}", whereCalus, andOperator,
                                                properties[i].Name, propertiesValue.Count);
                    }

                    if (properties[i].PropertyType == typeof(string[]))
                    {
                        string propName = "SpringsID";

                        var stringArray = value as string[];

                        for (int cc = 0; cc < stringArray.Length; cc++)
                        {
                            whereCalus = string.Format("{0}{1} {2} = @{3}", whereCalus, operatorOr,
                                                    propName, propertiesValue.Count);
                            operatorOr = " or";
                            propertiesValue.Add(Convert.ToInt32(stringArray[cc]));
                        }
                    }
                    else
                    {
                        andOperator = " and";

                        propertiesValue.Add(value);
                    }
                }
            }

            if (string.IsNullOrEmpty(whereCalus) == false)
            {
                data = data.Where(whereCalus, propertiesValue.ToArray());
            }

            return data;
        }

        /// <summary>
        /// 專門給在前台顯示的時候必須有的Filter條件。
        /// 會判斷是否啟用(IsEnable)和上下架的時間
        /// </summary>
        /// <typeparam name="T">通常是EF的Entity</typeparam>
        /// <param name="data">要被處理的資料</param>
        /// 
        /// <returns>有被加上where條件的data</returns>
        public static IQueryable<T> WhereForFrontDisplay<T>(this IQueryable<T> data)
        {
            // x => x.IsEnable == true && ((x.PostDateStart == null || x.PostDateStart < DateTime.Now) && (x.PostDateEnd == null || x.PostDateEnd > DateTime.Now))
            data = data.Where("IsEnable = true And (PostDateStart = null OR PostDateStart < @0) And (PostDateEnd = null || PostDateEnd > @0)", DateTime.Now);

            return data;
        }
    }
}
