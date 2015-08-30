using System.Collections.Generic;
using System.Web.Mvc;

namespace Core.Alerts
{
    /// <summary>
    /// Alert相關的Helper方法，方便呼叫使用
    /// </summary>
    public static class AlertExtension
    {
        /// <summary>
        /// Alert存到TempData的Key值
        /// </summary>
        private static readonly string Alerts = "_Alerts";

        /// <summary>
        /// 取得目前所擁有的Alert清單。
        /// </summary>
        /// <param name="tempData">TempData</param>
        /// <returns>目前所還沒有顯示過的Alert清單</returns>
        public static List<AlertViewModel> GetAlerts(this TempDataDictionary tempData)
        {
            if (!tempData.ContainsKey(Alerts))
            {
                tempData[Alerts] = new List<AlertViewModel>();
            }

            return (List<AlertViewModel>)tempData[Alerts];
        }

        /// <summary>
        /// 增加一筆要顯示的Alert
        /// </summary>
        /// <param name="tempData">TempData</param>
        /// <param name="messageClass">這筆Alert的Class值</param>
        /// <param name="message">這筆Alert的訊息</param>
        public static void AddAlert(this TempDataDictionary tempData, string messageClass, string message)
        {
            var alerts = tempData.GetAlerts();
            alerts.Add(new AlertViewModel(messageClass, message));
        }

        /// <summary>
        /// 返回的ActionResult加上訊息屬於success Class的Helper
        /// </summary>
        /// <param name="result">ActionResult</param>
        /// <param name="message">要顯示的訊息</param>
        /// <returns>有增加Alert訊息的ActionResult</returns>
        public static ActionResult WithSuccess(this ActionResult result, string message)
        {
            return new AlertDecoratorActionResult(result, "alert-success", message);
        }

        /// <summary>
        /// 返回的ActionResult加上訊息屬於info Class的Helper
        /// </summary>
        /// <param name="result">ActionResult</param>
        /// <param name="message">要顯示的訊息</param>
        /// <returns>有增加Alert訊息的ActionResult</returns>
        public static ActionResult WithInfo(this ActionResult result, string message)
        {
            return new AlertDecoratorActionResult(result, "alert-info", message);
        }

        /// <summary>
        /// 返回的ActionResult加上訊息屬於warning Class的Helper
        /// </summary>
        /// <param name="result">ActionResult</param>
        /// <param name="message">要顯示的訊息</param>
        /// <returns>有增加Alert訊息的ActionResult</returns>
        public static ActionResult WithWarning(this ActionResult result, string message)
        {
            return new AlertDecoratorActionResult(result, "alert-warning", message);
        }

        /// <summary>
        /// 返回的ActionResult加上訊息屬於error Class的Helper
        /// </summary>
        /// <param name="result">ActionResult</param>
        /// <param name="message">要顯示的訊息</param>
        /// <returns>有增加Alert訊息的ActionResult</returns>
        public static ActionResult WithError(this ActionResult result, string message)
        {
            return new AlertDecoratorActionResult(result, "alert-danger", message);
        }
    }
}
