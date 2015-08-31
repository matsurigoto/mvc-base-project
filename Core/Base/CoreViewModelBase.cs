namespace Core.Base
{
    /// <summary>
    /// Core View Model 的 Base class。所有ViewModel將會繼承這一個。
    /// </summary>
    public abstract class CoreViewModelBase
    {
        /// <summary>
        /// 如果需要產生SelectList到ViewData裡面，那麼child class會複寫這個Property，輸入需要產生的SelectList資訊。
        /// </summary>
        /// <value>
        /// 
        /// </value>
        /*
        public virtual SelectListFill.SelectListViewModel[] NeedFillSelectList
        {
            get
            {
                return null;
            }
        }
        */
    }
}
