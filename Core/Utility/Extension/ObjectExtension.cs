using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Core.Utility.Extension
{
    /// <summary>
    /// string 通用行的方法
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// 取得非null的string值。
        /// 屬於Extension 方法。
        /// </summary>
        /// <param name="value">要被取得string值的物件</param>
        /// <returns>如果傳進來的物件是null，回傳空值。</returns>
        public static string NonNullString(this object value)
        {
            return (value ?? string.Empty).ToString();
        }

        /// <summary>
        /// 取得某一個欄位的顯示名稱。
        /// </summary>
        /// <param name="value">要顯示的欄位</param>
        /// <param name="defaultValue">如果沒有任何attribute要用的值</param>
        /// <returns>
        /// 顯示的值
        /// </returns>
        public static string DisplayName(this MemberInfo value, string defaultValue)
        {
            string result = defaultValue;

            var displayAttr = value.GetCustomAttributes(typeof(DisplayAttribute), false)
                            .FirstOrDefault() as DisplayAttribute;

            if (displayAttr != null)
            {
                result = displayAttr.Name;

                if (displayAttr.ResourceType != null)
                {
                    result = displayAttr.GetName();
                }
            }
            else
            {
                var displayNameAttr = value.GetCustomAttributes(typeof(DisplayNameAttribute), false)
                            .FirstOrDefault() as DisplayNameAttribute;

                if (displayNameAttr != null)
                {
                    result = displayNameAttr.DisplayName;
                }
            }

            return result;
        }
    }
}
