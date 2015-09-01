using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility
{
    /// <summary>
    /// 讀取設定檔案的Helper
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// 取得DO儲存路徑
        /// </summary>
        /// <value>
        /// DO儲存路徑
        /// </value>
        public static string DoPath
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["Core.Utility.Config.DoPath"];
            }
        }

        /// <summary>
        /// 取得DO用來顯示的相對路徑
        /// </summary>
        /// <value>
        /// DO用來顯示的相對路徑
        /// </value>
        public static string DoPathForDisplay
        {
            get
            {
                return @"~\UploadPath";
            }
        }

        /// <summary>
        /// 取得屬於是image形態的問題描述
        /// </summary>
        /// <value>
        /// 屬於是image形態的問題描述
        /// </value>
        public static IList<string> ImageDoFileType
        {
            get
            {
                return new List<string>() { "jpg", "png" };
            }
        }

        /// <summary>
        /// 取得屬於文件形態的Do文字
        /// </summary>
        /// <value>
        /// 屬於文件形態的Do文字
        /// </value>
        public static IList<string> DocDoFileType
        {
            get
            {
                return new List<string>() { "pdf", "doc", "docx", "xsl", "xslx" };
            }
        }
    }
}
