using System;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Core.Utility.ExportExcel
{
    /// <summary>
    /// 代表匯出Excel的ActionResult Base
    /// </summary>
    public abstract class ExportExcelActionResult : ActionResult
    {
        /// <summary>
        /// 取得或設定Excel的Sheet名稱
        /// </summary>
        /// <value>
        /// Excel的Sheet名稱
        /// </value>
        public string WorkSheetName { get; set; }

        /// <summary>
        /// 取得或設定Excel檔案名稱
        /// </summary>
        /// <value>
        /// Excel檔案名稱
        /// </value>
        public string FileName { get; set; }

        /// <summary>
        /// 取得或設定要匯出資料
        /// </summary>
        /// <value>
        /// 要匯出資料
        /// </value>
        public DataTable ExportData { get; set; }

        /// <summary>
        /// 實例化 <see cref="ExportExcelActionResult"/> class.
        /// </summary>
        /// <param name="inExportData">要匯出的資料</param>
        /// <param name="inWorkSheetName">Excell Worksheet的名稱</param>
        /// <param name="inFileName">匯出檔案的名字</param>
        public ExportExcelActionResult(DataTable inExportData, string inWorkSheetName = "",
                                        string inFileName = "")
        {
            ExportData = inExportData;
            WorkSheetName = inWorkSheetName;
            FileName = inFileName;
        }

        /// <summary>
        /// Enables processing of the result of an action method by a custom type that inherits from the <see cref="T:System.Web.Mvc.ActionResult" /> class.
        /// </summary>
        /// <param name="context">The context in which the result is executed. The context information includes the controller, HTTP content, request context, and route data.</param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (string.IsNullOrWhiteSpace(this.WorkSheetName))
            {
                this.WorkSheetName = "Sheet1";
            }

            if (string.IsNullOrWhiteSpace(this.FileName))
            {
                this.FileName = string.Concat(
                    "ExcelExport_",
                    DateTime.Now.ToString("yyyyMMddHHmmss"),
                    ".xls");
            }

            GenerateOutput(context);
        }

        /// <summary>
        /// 產生匯出的Excel資料
        /// </summary>
        /// <param name="context">The context.</param>
        private void GenerateOutput(ControllerContext context)
        {
            try
            {
                if (this.ExportData != null)
                {
                    BootstrapHttpContextSetting(context);

                    FillOutputStream(context);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 準備好Http Response的一些內容和值
        /// </summary>
        /// <param name="context">The context.</param>
        protected virtual void BootstrapHttpContextSetting(ControllerContext context)
        {
            context.HttpContext.Response.Clear();

            // 編碼
            context.HttpContext.Response.ContentEncoding = Encoding.UTF8;

            // 設定網頁ContentType
            context.HttpContext.Response.ContentType =
                "application/vnd.ms-excel";

            // 匯出檔名
            var browser = context.HttpContext.Request.Browser.Browser;
            var exportFileName = browser.Equals("Firefox", StringComparison.OrdinalIgnoreCase)
                ? this.FileName
                : HttpUtility.UrlEncode(this.FileName, Encoding.UTF8);

            context.HttpContext.Response.AddHeader(
                "Content-Disposition",
                string.Format("attachment;filename={0}", exportFileName));
        }

        /// <summary>
        /// 實際處理匯出邏輯的方法
        /// </summary>
        /// <param name="context">The context.</param>
        protected abstract void FillOutputStream(ControllerContext context);
    }
}