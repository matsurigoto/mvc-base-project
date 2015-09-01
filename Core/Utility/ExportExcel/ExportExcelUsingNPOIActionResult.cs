using System.Data;
using System.IO;
using System.Web.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace Core.Utility.ExportExcel
{
    /// <summary>
    /// 用NPOI作為匯出邏輯的ActionResult
    /// </summary>
    public class ExportExcelUsingNPOIActionResult : ExportExcelActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExportExcelUsingNPOIActionResult"/> class.
        /// </summary>
        /// <param name="inExportData">要匯出的資料</param>
        /// <param name="inWorkSheetName">Excell Worksheet的名稱</param>
        /// <param name="inFileName">匯出檔案的名字</param>
        public ExportExcelUsingNPOIActionResult(DataTable inExportData,
                                        string inWorkSheetName = "",
                                        string inFileName = "")
            : base(inExportData, inWorkSheetName, inFileName)
        {
        }

        /// <summary>
        /// 實際處理匯出邏輯的方法
        /// </summary>
        /// <param name="context">The context.</param>
        protected override void FillOutputStream(ControllerContext context)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();

            ISheet sheet = workbook.CreateSheet();

            CreatWorksheetForSingleDataTable(ExportData, ref workbook, ref sheet);

            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.WriteTo(context.HttpContext.Response.OutputStream);
            }

            workbook = null;
        }

        /// <summary>
        /// 為資料建立成NPOI 的Workbook和Sheet
        /// </summary>
        /// <param name="sourceTable">要匯出的內容</param>
        /// <param name="workbook">The workbook.</param>
        /// <param name="sheet">The sheet.</param>
        /// <param name="headerRowIndex">標題欄的index起始位置</param>
        /// <param name="rowIndexStart">第一筆實際資料的起始位置</param>
        private void CreatWorksheetForSingleDataTable(DataTable sourceTable, ref HSSFWorkbook workbook,
            ref ISheet sheet, int headerRowIndex = 0, int rowIndexStart = 1)
        {
            IRow headerRow = sheet.CreateRow(headerRowIndex);

            // Set up the Header Coloumn cell style

            var headerCellStyle = workbook.CreateCellStyle();

            headerCellStyle.BorderTop = BorderStyle.Thin;

            headerCellStyle.BorderBottom = BorderStyle.Thin;

            headerCellStyle.Alignment = HorizontalAlignment.Center;

            // Set up the Header Coloumn cell font style

            var headerCellFontStyle = workbook.CreateFont();

            headerCellFontStyle.Boldweight = (short)FontBoldWeight.Bold;

            headerCellStyle.SetFont(headerCellFontStyle);

            // handling header.
            foreach (DataColumn column in sourceTable.Columns)
            {
                var headerCell = headerRow.CreateCell(column.Ordinal);
                headerCell.SetCellValue(column.ColumnName);
                headerCell.CellStyle = headerCellStyle;
            }

            // handling value.
            int rowIndex = rowIndexStart;

            foreach (DataRow row in sourceTable.Rows)
            {
                IRow dataRow = sheet.CreateRow(rowIndex);

                foreach (DataColumn column in sourceTable.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }

                rowIndex++;
            }
        }
    }
}
