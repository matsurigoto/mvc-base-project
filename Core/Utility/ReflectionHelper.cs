using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility
{
    /// <summary>
    /// Reflection相關的Helper方法
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        /// 取得某一個Type目前所有的Property。
        /// 會濾掉Parent的property和在Child裡面有複寫Parent的Property
        /// </summary>
        /// <param name="t">要取得的Type</param>
        /// <returns>某一個Type目前所有的Property - 不包含Parent的property和有override的property</returns>
        public static PropertyInfo[] GetPropertiesOfCurrentType(Type t)
        {
            var properties = t.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public)
                                .Where(x => (x.GetGetMethod().GetBaseDefinition() == x.GetGetMethod())).ToArray(); // where 條件是用來避免override的property （例如：OrderByColumnName）也被算進去。
            return properties;
        }

        /// <summary>
        /// 取得某一個Type目前所有的Property。
        /// 這邊會取得所有的Public Instance 和包含parent 的所有property
        /// </summary>
        /// <param name="t">要取得的Type</param>
        /// <returns>某一個Type目前所有的Property</returns>
        public static PropertyInfo[] GetPropertiesOfType(Type t)
        {
            var properties = t.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
            return properties;
        }
    }
}
