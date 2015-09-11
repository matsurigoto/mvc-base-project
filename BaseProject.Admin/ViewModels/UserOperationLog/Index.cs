using Core.Common.Mapper;
using System.Collections.Generic;
using Core.Utility.Extension;
using Core.Base;
using System.ComponentModel.DataAnnotations;
using Core.SelectListFill;
using Core.Utility.ExportExcel;

namespace BaseProject.Admin.ViewModels.UserOperationLog
{
    public class Index : SearchViewModelBase<SearchFormViewModel, SearchResult>
    {
        public override SelectListViewModel[] NeedFillSelectList
        {
            get
            {
                List<SelectListViewModel> temp = new List<SelectListViewModel>();

                temp.Add(CoreSelectListViewModelHelper
                    .GetSelectListViewModel<Index, BaseProject.Model.AspNetUsers>
                    (x => x.SearchForm.UserId, x => x.Name, x => x.Id, () => SearchForm.UserId));

                return temp.ToArray();
            }
        }
    }

    public class SearchResult : IHaveCustomMapping
    {
        public int Id { get; set; }

        [Display(Name = "使用時間")]
        public System.DateTime VisitDateTime { get; set; }

        [Display(Name = "使用者姓名")]
        public string UserName { get; set; }

        [Display(Name = "功能名稱")]
        public string ResourceName { get; set; }

        [IgnoreIEnumerableToDataTableAttribute]
        public Core.Common.Security.ResourceOperationEnum Operation { get; set; }

        [Display(Name = "動作")]
        public string OperationDisplayName
        {
            get
            {
                return Operation.DisplayName();
            }
        }

        [Display(Name = "IP位置")]
        public string IP { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<BaseProject.Model.UserOperationLog, SearchResult>().ForMember(x => x.UserName, opt => opt.MapFrom(src => src.AspNetUsers.Name));
        }
    }

    public class SearchFormViewModel : SearchFormViewModelBase<Model.UserOperationLog>
    {
        [Display(Name = "使用者")]
        public string UserId { get; set; }
    }
}
