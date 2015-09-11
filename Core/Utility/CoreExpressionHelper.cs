using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Utility
{
    /// <summary>
    /// 和Lambada 相關的ExpressionHelper
    /// </summary>
    public static class CoreExpressionHelper
    {
        /// <summary>
        /// 取得某一個Lambada Expression的值。
        /// 和Mvc內建的不同在於遇到NullAble的形態也會取得他的Text
        /// </summary>
        /// <typeparam name="TModel">Model的形態</typeparam>
        /// <param name="expression">要取得的Expression</param>
        /// <returns>取得Expression裡面的欄位值</returns>
        public static string GetExpressionText<TModel>(Expression<Func<TModel, object>> expression)
        {
            var expr = (LambdaExpression)expression;
            if (expr.Body.NodeType == ExpressionType.Convert)
            {
                var ue = expression.Body as UnaryExpression;
                return String.Join(".", GetProperties(ue.Operand).Select(p => p.Name));
            }

            return System.Web.Mvc.ExpressionHelper.GetExpressionText(expr);
        }

        /// <summary>
        /// 取得某個Expression裡面所有的Property
        /// </summary>
        /// <param name="expression">要取得的Expression</param>
        /// <returns>取得所有的PropertyInfo</returns>
        private static IEnumerable<PropertyInfo> GetProperties(Expression expression)
        {
            var memberExpression = expression as MemberExpression;
            if (memberExpression == null)
            {
                yield break;
            }

            var property = memberExpression.Member as PropertyInfo;

            foreach (var propertyInfo in GetProperties(memberExpression.Expression))
            {
                yield return propertyInfo;
            }

            yield return property;
        }
    }
}
