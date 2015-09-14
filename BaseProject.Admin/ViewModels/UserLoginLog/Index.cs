using System.ComponentModel.DataAnnotations;
using Core.Base;
using Core.Common.Mapper;

namespace BaseProject.Admin.ViewModels.UserLoginLog
{
    public class Index : SearchViewModelBase<SearchFormViewModel, SearchResult>
    {
    }

    public class SearchResult : IMapFrom<BaseProject.Model.UserLoginLog>
    {
        public int Id { get; set; }

        [Display(Name = "登入帳號")]
        public string UserName { get; set; }

        [Display(Name = "嘗試登入時間")]
        public System.DateTime AttempTime { get; set; }

        [Display(Name = "IP")]
        public string IP { get; set; }

        public Core.Common.Security.EnumLoginStatus Status { get; set; }

        [Display(Name = "狀態")]
        public string StatusDisplayString { get; set; }
    }

    public class SearchFormViewModel : SearchFormViewModelBase<BaseProject.Model.UserOperationLog>
    {
        [Display(Name = "登入帳號")]
        public string UserName { get; set; }
    }
}
