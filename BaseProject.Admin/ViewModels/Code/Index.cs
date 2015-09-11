using BaseProject.SelectListFill;
using Core.Base;
using Core.Common.Mapper;
using Core.SelectListFill;
using System.Collections.Generic;
using System.ComponentModel;

namespace BaseProject.Admin.ViewModels.Code
{
    public class Index : SearchViewModelBase<SearchFormViewModel, SearchResult>
    {
        public override Core.SelectListFill.SelectListViewModel[] NeedFillSelectList
        {
            get
            {
                List<SelectListViewModel> temp = new List<SelectListViewModel>();

                temp.Add(SelectListViewModelHelper.GetSelectListViewModelForCodeMain
                        <Index>(x => x.SearchForm.MainID, () => (SearchForm as SearchFormViewModel).MainID));

                return temp.ToArray();
            }
        }
    }

    public class SearchResult : IMapFrom<BaseProject.Model.Code>
    {
        public int SN { get; set; }
        [DisplayName("代碼值")]
        public string Value { get; set; }

        [DisplayName("代碼顯示值")]
        public string Display { get; set; }

        [DisplayName("順序")]
        public int OrderBy { get; set; }

        public virtual BaseProject.Admin.ViewModels.CodeMain.Detail CodeMain { get; set; }
    }

    public class SearchFormViewModel : SearchFormViewModelBase<BaseProject.Model.Code>
    {
        [DisplayName("代碼主檔名稱")]
        public string MainID { get; set; }
    }

}