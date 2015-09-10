using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.Admin.ViewModels.Account
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
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

        /// <summary>
        /// 取得或設定建立使用者
        /// </summary>
        /// <value>
        /// 建立使用者
        /// </value>
        public string CreateBy { get; set; }

        /// <summary>
        /// 取得或設定建立時間
        /// </summary>
        /// <value>
        /// 建立時間
        /// </value>
        public System.DateTime CreateDate { get; set; }

        /// <summary>
        /// 取得或設定最後修改人
        /// </summary>
        /// <value>
        /// 最後修改人
        /// </value>
        public string LastModifyBy { get; set; }

        /// <summary>
        /// 取得或設定最後修改時間
        /// </summary>
        /// <value>
        /// 最後修改時間
        /// </value>
        public System.Nullable<System.DateTime> LastModifyDate { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}