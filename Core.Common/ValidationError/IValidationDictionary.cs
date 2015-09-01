namespace Core.Common.ValidationError
{
    /// <summary>
    /// 定義記錄錯誤訊息的Dictionary所需要有的方法和property
    /// </summary>
    public interface IValidationDictionary
    {
        /// <summary>
        /// 記錄一筆錯誤訊息
        /// </summary>
        /// <param name="key">錯誤訊息的key。通常是property的名稱</param>
        /// <param name="errorMessage">錯誤訊息的描述</param>
        void AddError(string key, string errorMessage);

        /// <summary>
        /// 取得目前的情況，Validation有沒有通過
        /// </summary>
        /// <value>
        ///   <c>true</c> 表示有通過; 要不然就是, <c>false</c>.
        /// </value>
        bool IsValid { get; }
    }
}
