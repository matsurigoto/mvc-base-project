using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BaseProject.Admin.ViewModels.Account
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "RoleName")]
        public string Name { get; set; }
    }

    public class EditUserViewModel
    {
        public string Id { get; set; }

        /// <summary>
        /// 取得或設定使用者名稱
        /// </summary>
        /// <value>
        /// 使用者名稱
        /// </value>
        [Display(Name = "姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 取得或設定單位
        /// </summary>
        /// <value>
        /// 單位
        /// </value>
        [Display(Name = "單位")]
        public string Unit { get; set; }

        /// <summary>
        /// 取得或設定此帳號是否啟用
        /// </summary>
        /// <value>
        /// <c>true</c> 帳號啟用; 不然則是, <c>false</c>.
        /// </value>
        [Display(Name = "帳號是否啟用")]
        public bool IsEnabled { get; set; }

        [StringLength(100, ErrorMessage = "{0} 至少是 {2} 碼長度", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "確認密碼")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "確認密碼和密碼不符")]
        public string ConfirmPassword { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "群組")]
        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}