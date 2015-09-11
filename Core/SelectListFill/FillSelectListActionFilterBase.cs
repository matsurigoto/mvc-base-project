using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Core.Base;
using Core.Common.Repository;

namespace Core.SelectListFill
{
    /// <summary>
    /// 把ViewModel裡面有設定要產生的SelectList產生出來並且寫到ViewData。
    /// 需要由此Class的Child來複寫產生SelectList的邏輯
    /// </summary>
    public abstract class FillSelectListActionFilterBase : ActionFilterAttribute
    {
        /// <summary>
        /// 產生SelectList的邏輯
        /// </summary>
        /// <param name="viewModel">提供要如何產出SelectList的資訊</param>
        /// <returns>依照ViewModel的資訊產出對應的SelectList</returns>
        public abstract SelectList GetSelectList(SelectListFill.SelectListViewModel viewModel);

        /// <summary>
        /// 把產出的SelectList注入到ViewData裡面
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var viewResult = filterContext.Result as ViewResult;

            if (viewResult != null && viewResult.Model is CoreViewModelBase)
            {
                var selectListViewModelArray = ((CoreViewModelBase)viewResult.Model).NeedFillSelectList;

                // 假設有設定ViewModel才要做產出的動作
                if (selectListViewModelArray != null && selectListViewModelArray.Count() > 0)
                {
                    foreach (var item in selectListViewModelArray)
                    {
                        // 假設目前ViewData裡面沒有這個SelectList才產生。因此，在別的地方產出的SelectList的權重比這一個
                        // filter還高。
                        if (viewResult.ViewData[item.SelectListId] as System.Web.Mvc.SelectList == null)
                        {
                            viewResult.ViewData[item.SelectListId] = this.GetSelectList(item);
                        }
                    }
                }
            }

            base.OnActionExecuted(filterContext);
        }
    }
}
