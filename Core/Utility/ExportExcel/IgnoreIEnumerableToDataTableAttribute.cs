using System;

namespace Core.Utility.ExportExcel
{
    /// <summary>
    /// 表示IEnumerable轉DataTable用作匯出的時候，避免欄位被轉
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreIEnumerableToDataTableAttribute : Attribute
    {
    }
}
