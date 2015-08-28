namespace Core.Common.Task
{
    /// <summary>
    /// 在網站啟動之後，要執行的內容
    /// </summary>
    public interface IRunAtStartup
    {
        /// <summary>
        /// 要執行的邏輯
        /// </summary>
        void Execute();
    }
}
