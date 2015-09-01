using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core.Utility.Extension;

namespace Core.Utility.ExportExcel.Extension
{
    /// <summary>
    /// 匯出和IEnumerable相關的Extension方法
    /// </summary>
    public static class IEnumerableExtension
    {
        /// <summary>
        /// 把IEnumerable的東西轉換成為DataTable
        /// </summary>
        /// <param name="list">要轉換的IEnumerable</param>
        /// <returns>轉換完成的DataTable</returns>
        public static DataTable ToDataTable(this IEnumerable list)
        {
            return list.ToDataTable(x => true);
        }

        /// <summary>
        /// 把IEnumerable的東西轉換成為DataTable
        /// </summary>
        /// <param name="list">要轉換的IEnumerable</param>
        /// <param name="isRender">判斷某一個欄位是否要包含在DataTable裡面</param>
        /// <returns>轉換完成的DataTable</returns>
        public static DataTable ToDataTable(this IEnumerable list, Predicate<PropertyInfo> isRender)
        {
            DataTable dt = new DataTable();
            bool isShcemaGenerated = false;
            IEnumerable<PropertyInfo> propInfo = null;

            foreach (var item in list)
            {
                if (!isShcemaGenerated)
                {
                    // 決定那些欄位要匯出
                    propInfo = item.GetType().GetProperties().Where(x => isRender(x)
                        && x.GetCustomAttribute(typeof(IgnoreIEnumerableToDataTableAttribute)) == null);

                    foreach (var p in propInfo)
                    {
                        var columnName = p.DisplayName(p.Name);

                        dt.Columns.Add(new DataColumn(columnName, p.PropertyType));
                    }

                    isShcemaGenerated = true;
                }

                var row = dt.NewRow();
                foreach (var p in propInfo)
                {
                    var columnName = p.DisplayName(p.Name);

                    row[columnName] = p.GetValue(item, null);
                }

                dt.Rows.Add(row);
            }

            dt.AcceptChanges();
            return dt;
        }
    }
}
