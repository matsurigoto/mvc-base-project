namespace Core.Common.ValidationError
{
    /// <summary>
    /// 代表一個錯誤訊息
    /// </summary>
    public interface IBaseError
    {
        /// <summary>
        /// 代表有錯誤的Property名稱
        /// </summary>
        /// <value>
        /// 有錯誤的Property名稱
        /// </value>
        string PropertyName { get; }

        /// <summary>
        /// 代表錯誤的Property原因內容
        /// </summary>
        /// <value>
        /// 錯誤的Property原因內容
        /// </value>
        string PropertyExceptionMessage { get; }
    }
}
