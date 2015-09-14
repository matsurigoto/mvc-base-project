using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Core.Utility.Extension;

namespace Core.Security
{
    /// <summary>
    /// Security相關的HtmlHelper
    /// </summary>
    public static class HtmlHelper
    {
        /// <summary>
        /// 產生Flag Enum的Checkbox
        /// </summary>
        /// <typeparam name="TModel">Enum值的Model</typeparam>
        /// <typeparam name="TEnum">Flag Enum形態</typeparam>
        /// <param name="htmlHelper">Html helper.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="isDisabled">if set to <c>true</c> [is disabled].</param>
        /// <param name="htmlAttribues">The HTML attribues.</param>
        /// <returns>
        /// 依照enum值產生對應的Checkbox
        /// </returns>
        /// <exception cref="System.ArgumentException">This helper can only be used with enums. Type used was:  + enumModelType.FullName.ToString() + .</exception>
        /// <exception cref="ArgumentException">This helper can only be used with enums. Type used was:  + enumModelType.FullName.ToString() + .</exception>
        public static IHtmlString CheckBoxesForEnumFlagsFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TEnum>> expression, bool isDisabled = false, object htmlAttribues = null)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            Type enumModelType = metadata.ModelType;

            // Check to make sure this is an enum.
            if (!enumModelType.IsEnum)
            {
                throw new ArgumentException("This helper can only be used with enums. Type used was: " + enumModelType.FullName.ToString() + ".");
            }

            // Create string for Element.
            var sb = new StringBuilder();
            foreach (Enum item in Enum.GetValues(enumModelType))
            {
                if (Convert.ToInt32(item) != 0)
                {
                    var ti = htmlHelper.ViewData.TemplateInfo;
                    var id = ti.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression) + item);
                    var name = ti.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));
                    var label = new TagBuilder("label");
                    label.Attributes["for"] = id;

                    // Add checkbox.
                    var checkbox = new TagBuilder("input");
                    checkbox.Attributes["id"] = id;
                    checkbox.Attributes["name"] = name;
                    checkbox.Attributes["type"] = "checkbox";
                    checkbox.Attributes["value"] = item.ToString();

                    if (isDisabled)
                    {
                        checkbox.Attributes["disabled"] = "disabled";
                    }

                    if (htmlAttribues != null)
                    {
                        var attributes = System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttribues) as IDictionary<string, object>;

                        checkbox.MergeAttributes(attributes);
                    }

                    var model = metadata.Model as Enum;

                    if ((model != null) && model.HasFlag(item))
                    {
                        checkbox.Attributes["checked"] = "checked";
                    }

                    sb.AppendLine(checkbox.ToString());

                    label.SetInnerText(item.DisplayName());

                    sb.AppendLine(label.ToString());

                    // Add line break.
                    // sb.AppendLine("<br />");
                }
            }

            return new HtmlString(sb.ToString());
        }
    }
}
