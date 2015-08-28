namespace Core.Common.Task
{
    /// <summary>
    /// 在每一個Request執行完成之後，要執行的內容
    /// </summary>
    public interface IRunAfterEachRequest
    {
        /// <summary>
        /// 要執行的邏輯。
        /// </summary>
        void Execute();
    }
}
