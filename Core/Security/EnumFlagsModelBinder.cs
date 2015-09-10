using System;
using System.Web.Mvc;

namespace Core.Security
{
    /// <summary>
    /// EnumFlagType的Binding處理
    /// </summary>
    public class EnumFlagsModelBinder : DefaultModelBinder
    {
        /// <summary>
        /// 這個ModelBinding是配對Core.Security.HtmlHelper.CheckBoxesForEnumFlagsFor的產生
        /// Binds the model by using the specified controller context and binding context.
        /// </summary>
        /// <param name="controllerContext">The context within which the controller operates. The context information includes the controller, HTTP content, request context, and route data.</param>
        /// <param name="bindingContext">The context within which the model is bound. The context includes information such as the model object, model name, model type, property filter, and value provider.</param>
        /// <example>
        /// 記得在Global.asax加上
        /// ModelBinders.Binders.Add(typeof(ResourceOperationEnum), new EnumFlagsModelBinder());
        /// </example>
        /// <returns>
        /// The bound object.
        /// </returns>
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // Fetch value to bind.
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (value != null)
            {
                // Get type of value.
                Type valueType = bindingContext.ModelType;

                var rawValues = value.RawValue as string[];
                if (rawValues != null)
                {
                    // Create instance of result object.
                    var result = (Enum)Activator.CreateInstance(valueType);

                    try
                    {
                        // Parse.
                        result = (Enum)Enum.Parse(valueType, string.Join(",", rawValues));
                        return result;
                    }
                    catch
                    {
                        return base.BindModel(controllerContext, bindingContext);
                    }
                }
            }

            return base.BindModel(controllerContext, bindingContext);
        }
    }
}
