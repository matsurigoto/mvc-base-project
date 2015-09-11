using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.SelectListFill
{
    /// <summary>
    /// 代表一個要被產生的SelectList
    /// </summary>
    public class SelectListViewModel
    {
        /// <summary>
        /// 取得或設定此SelectList要和那個ViewModel Property對應
        /// </summary>
        /// <value>
        /// 此SelectList要和那個ViewModel Property對應
        /// </value>
        public string SelectListId { get; set; }

        /// <summary>
        /// 取得或設定資料來源
        /// </summary>
        /// <value>
        /// 資料來源
        /// </value>
        public string Source { get; set; }

        /// <summary>
        /// 取得或設定SelectList值的欄位來源
        /// </summary>
        /// <value>
        /// SelectList值的欄位來源
        /// </value>
        public string DataValueField { get; set; }

        /// <summary>
        /// 取得或設定SelectList顯示的欄位來源
        /// </summary>
        /// <value>
        /// SelectList顯示的欄位來源
        /// </value>
        public string DataTextField { get; set; }

        /// <summary>
        /// 取得或設定SelectList被選取的值
        /// </summary>
        /// <value>
        /// SelectList被選取的值
        /// </value>
        public object SelectedValue { get; set; }

        /// <summary>
        /// SelectList要從那裡被產生出來
        /// </summary>
        private string codeWhere;

        /// <summary>
        /// 取得或設定SelectList要從那裡被產生出來
        /// </summary>
        /// <value>
        /// SelectList要從那裡被產生出來
        /// </value>
        public string CodeWhere
        {
            get
            {
                if (string.IsNullOrEmpty(codeWhere))
                {
                    // 因為有可能SelectListId的來源是SearchForm裡面，因此會造成名字是SearchForm.xxx, 但是其實我只需要
                    // 後面xxx的部分。
                    var selectListIdString = SelectListId;
                    var subStringIndex = selectListIdString.LastIndexOf(".") + 1;
                    if (selectListIdString.Length > subStringIndex)
                    {
                        codeWhere = selectListIdString.Substring(subStringIndex);
                    }
                }

                return codeWhere;
            }

            set { codeWhere = value; }
        }
    }
}
