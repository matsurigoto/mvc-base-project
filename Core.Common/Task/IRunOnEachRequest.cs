using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Task
{
    /// <summary>
    /// 在每一個Request執行之前，要執行的內容
    /// </summary>
    public interface IRunOnEachRequest
    {
        /// <summary>
        /// 要執行的邏輯
        /// </summary>
        void Execute();
    }
}
