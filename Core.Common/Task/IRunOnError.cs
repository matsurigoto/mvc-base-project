namespace Core.Common.Task
{
    /// <summary>
    /// 當出現錯誤的時候，要執行的內容
    /// </summary>
    public interface IRunOnError
    {
        /// <summary>
        /// 要執行的邏輯
        /// </summary>
        void Execute();
    }
}
