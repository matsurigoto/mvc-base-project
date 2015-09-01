using System.Web.Mvc;

namespace Core.Utility.Alert
{
    /// <summary>
    /// 一個Decorator Pattern的ActionResult，讓增加Alert訊息變的方便
    /// </summary>
    public class AlertDecoratorActionResult : ActionResult
    {
        /// <summary>
        /// 取得或設定實際的ActionResult
        /// </summary>
        /// <value>
        /// 實際的ActionResult值
        /// </value>
        public ActionResult InnerResult { get; set; }

        /// <summary>
        /// 取得或設定Alert的Class
        /// </summary>
        /// <value>
        /// Alert的Class
        /// </value>
        public string AlertClass { get; set; }

        /// <summary>
        /// 取得或設定Alert的訊息
        /// </summary>
        /// <value>
        /// Alert的訊息
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertDecoratorActionResult"/> class.
        /// </summary>
        /// <param name="inInnerResult">The in inner result.</param>
        /// <param name="inAlertClass">The in alert class.</param>
        /// <param name="inMessage">The in message.</param>
        public AlertDecoratorActionResult(ActionResult inInnerResult, string inAlertClass, string inMessage)
        {
            this.InnerResult = inInnerResult;
            this.AlertClass = inAlertClass;
            this.Message = inMessage;
        }

        /// <summary>
        /// Enables processing of the result of an action method by a custom type that inherits from the <see cref="T:System.Web.Mvc.ActionResult" /> class.
        /// </summary>
        /// <param name="context">The context in which the result is executed. The context information includes the controller, HTTP content, request context, and route data.</param>
        public override void ExecuteResult(ControllerContext context)
        {
            context.Controller.TempData.AddAlert(this.AlertClass, this.Message);
            this.InnerResult.ExecuteResult(context);
        }
    }
}
