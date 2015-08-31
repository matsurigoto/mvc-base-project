namespace Core.Alerts
{
    /// <summary>
    /// 用來代表一筆需要顯示的Alert資訊ViewModel
    /// </summary>
    public class AlertViewModel
    {
        /// <summary>
        /// 儲存這個Alert資訊的Class - 用來區分呈現的顏色
        /// </summary>
        /// <value>
        /// Alert資訊的Class值
        /// </value>
        public string AlertClass { get; set; }

        /// <summary>
        /// Alert資訊的內容值
        /// </summary>
        /// <value>
        /// Alert資訊的值
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertViewModel"/> class.
        /// </summary>
        /// <param name="alertClass">Alert資訊的Class值</param>
        /// <param name="message">Alert資訊的內容</param>
        public AlertViewModel(string alertClass, string message)
        {
            AlertClass = alertClass;
            Message = message;
        }
    }
}
