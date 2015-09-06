using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Core.Security.Filter;
using Core.Utility.ExportExcel;
using Core.Utility.ExportExcel.Extension;
using Microsoft.Web.Mvc;

namespace Core.Base
{
    /// <summary>
    /// Application 層級的BaseController
    /// </summary>
    [AuthoriseFilter]
    public class CoreBaseController : Controller
    {
        /// <summary>
        /// Redirects to action.
        /// </summary>
        /// <typeparam name="TController">The type of the controller.</typeparam>
        /// <param name="action">The action.</param>
        /// <returns>ActionResult</returns>
        protected ActionResult RedirectToAction<TController>(Expression<Action<TController>> action)
            where TController : Controller
        {
            return ControllerExtensions.RedirectToAction(this, action);
        }

        /// <summary>
        /// 把資料匯出成為Excel
        /// </summary>
        /// <param name="data">要匯出的資料</param>
        /// <param name="sheetName">Excel裡面WorkSheet的名稱</param>
        /// <param name="fileName">Excel的檔案名稱</param>
        /// <returns>ActionResult</returns>
        protected virtual ActionResult ExportExcel(IEnumerable data, string sheetName = "", string fileName = "")
        {
            return ExportExcel(data.ToDataTable(), sheetName, fileName);
        }

        /// <summary>
        /// 把資料匯出成為Excel
        /// </summary>
        /// <param name="data">要匯出的資料</param>
        /// <param name="sheetName">Excel裡面WorkSheet的名稱</param>
        /// <param name="fileName">Excel的檔案名稱</param>
        /// <returns>ActionResult</returns>
        protected virtual ActionResult ExportExcel(DataTable data, string sheetName = "", string fileName = "")
        {
            return new ExportExcelUsingNPOIActionResult(data, sheetName, fileName);
        }
    }
}
