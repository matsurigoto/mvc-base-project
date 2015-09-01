using System;

namespace Core.Utility.Extension
{
    /// <summary>
    /// Enum相關的Extension
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 取得Enum的顯示文字。會先嘗試從DisplayAttribute和DisplayNameAttribute讀取name的值。
        /// 如果沒有，就用Enum本身的值。
        /// </summary>
        /// <param name="value">要取得的Enum值</param>
        /// <returns>Enum的顯示文字</returns>
        public static string DisplayName(this Enum value)
        {
            string result = value.ToString();

            Type enumType = value.GetType();

            var enumValue = Enum.GetName(enumType, value);

            result = enumType.GetMember(enumValue)[0].DisplayName(result);

            return result;
        }
    }
}
